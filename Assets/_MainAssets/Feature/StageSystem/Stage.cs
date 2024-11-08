using System;
using CookSystem;
using CustomerSystem;
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

        // TODO: Need Optimize
        [SerializeField] private StageTimer stageTimer;
        [SerializeField] private CustomerCounter customerCounter;
        [SerializeField] private ObjectiveManager objectiveManager;

        public void Init(StageData stageData)
        {
            stageState = StageState.Idle;
            _currentStageData = stageData;
            kitchenSystem.Init(OnPlateServe);
            kitchenSystem.HideIngredientSources();
            onStageStateChanged?.Invoke(stageState);
            switch (stageData.completionCondition)
            {
                case CompletionType.Timed:
                    stageTimer.Init(stageData.duration, onTimerEnd: StageFinished);
                    break;
                case CompletionType.Customer:
                    customerCounter.Init(stageData.customerLimit, onCustomerLimitReached: StageFinished);
                    break;
            }
        }

        private void OnPlateServe(FoodItemData foodItemData, FoodPlate foodPlate)
        {
            customerSpawner.OnPlateServe(foodItemData, foodPlate);
        }

        private void StageFinished()
        {
            stageState = StageState.Ended;
            onStageStateChanged?.Invoke(stageState);
            Pause();
        }

        public void Play()
        {
            stageState = StageState.Playing;
            stageTimer.StartTimer(_currentStageData.duration);
            kitchenSystem.ShowIngredientSources();
            customerCounter.ResetCustomerCount();
            onStageStateChanged?.Invoke(stageState);
            customerSpawner.Init();
        }

        public void Pause()
        {
            stageState = StageState.Paused;
            stageTimer.PauseTimer();
            onStageStateChanged?.Invoke(stageState);
            customerSpawner.Pause();
        }

        public void Resume()
        {
            stageState = StageState.Playing;
            stageTimer.ResumeTimer();
            onStageStateChanged?.Invoke(stageState);
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