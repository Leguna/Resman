using System;
using CookSystem;
using Currency;
using CustomerSystem;
using Events;
using StageSystem.Completion;
using StageSystem.Objective;
using UnityEngine;
using Utilities;

namespace StageSystem
{
    public class Stage : SingletonMonoBehaviour<Stage>
    {
        private StageData _currentStageData;
        public StageState stageState;
        public Action<StageState> onStageStateChanged = delegate { };
        [SerializeField] private KitchenSystem kitchenSystem;
        [SerializeField] private CustomerSpawner customerSpawner;
        [SerializeField] private ObjectiveManager objectiveManager;
        [SerializeField] private StageCompletionComponent stageCompletionComponent;

        private Gold _stageGold;
        public Action<Gold> onPaymentReceived = delegate { };

        public void Init(StageData stageData)
        {
            SetState(StageState.Idle);
            _stageGold = new Gold();
            _currentStageData = stageData;
            kitchenSystem.Init(OnPlateServe);
            kitchenSystem.HideIngredientSources();
            customerSpawner.Reset();
            objectiveManager.Hide();
        }

        private void OnPlateServe(FoodItemData foodItemData, FoodPlate foodPlate)
        {
            customerSpawner.OnPlateServe(foodItemData, foodPlate);
        }

        private void StageFinished()
        {
            onPaymentReceived?.Invoke(_stageGold);
            SetState(StageState.Ended);
            stageCompletionComponent.Hide();
            Reset();
        }

        private void SetState(StageState state)
        {
            stageState = state;
            onStageStateChanged?.Invoke(stageState);
        }

        public void Play()
        {
            objectiveManager.Init(_currentStageData.goalData);
            SetState(StageState.Playing);
            kitchenSystem.ShowIngredientSources();
            stageCompletionComponent.Init(_currentStageData, StageFinished);
            customerSpawner.Init(OnPaymentReceived, OnCustomerLeave, OnCustomerEnter);
        }


        private void OnCustomerLeave()
        {
            if (stageCompletionComponent.CheckIfCompleted())
            {
                StageFinished();
            }
        }

        private void OnCustomerEnter()
        {
            stageCompletionComponent.DecrementCustomerCount();
            if (stageCompletionComponent.CheckIfCompleted())
            {
                customerSpawner.CloseShop();
            }
        }

        private void OnPaymentReceived(int payment)
        {
            _stageGold.Add(payment);
            var paymentEvent = new AddObjectiveEvent
            {
                GoalType = GoalType.RevenueGoal,
                Amount = payment
            };
            objectiveManager.OnObjectiveEvent(paymentEvent);
        }

        public void Pause()
        {
            SetState(StageState.Paused);
            stageCompletionComponent.Pause();
            customerSpawner.Pause();
        }

        public void Resume()
        {
            SetState(StageState.Playing);
            stageCompletionComponent.Resume();
            customerSpawner.Resume();
        }

        public void Restart()
        {
            Reset();
            Play();
        }

        public void Reset() => Init(_currentStageData);
    }

    public enum StageState
    {
        Idle,
        Playing,
        Paused,
        Ended
    }
}