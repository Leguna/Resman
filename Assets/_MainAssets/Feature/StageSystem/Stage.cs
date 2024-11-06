using System;
using System.Collections.Generic;
using StageSystem.Completion;
using StageSystem.Objective;
using UnityEngine;
using Utilities;

namespace StageSystem
{
    public class Stage : SingletonMonoBehaviour<Stage>
    {
        public StageData currentStageData;
        public StageState stageState;
        public Action<StageState> onStageChanged = delegate { };

        [SerializeField] private StageTimer stageTimer;
        [SerializeField] private CustomerCounter customerCounter;
        [SerializeField] private ObjectiveManager objectiveManager;

        public void Init(StageData stageData)
        {
            currentStageData = stageData;
            stageState = StageState.Idle;
            onStageChanged?.Invoke(stageState);
            objectiveManager.Init(stageData.objectiveData);
            objectiveManager.SetListener(() => { print("Objective Achieved"); });
            switch (stageData.completionCondition)
            {
                case CompletionType.Timed:
                    stageTimer.Init(stageData.duration, onTimerEnd: StageFinished);
                    break;
                case CompletionType.Customer:
                    customerCounter.Init(stageData.customerLimit, onCustomerLimitReached: StageFinished);
                    break;
                default:
                    throw new Exception("Invalid completion condition");
            }
        }

        private void StageFinished()
        {
            stageState = StageState.Ended;
            onStageChanged?.Invoke(stageState);
        }

        public void CustomerCounterIncrement() => customerCounter.IncrementCustomerCount();

        public void ObjectiveUpdate(float amount) => objectiveManager.UpdateObjective(amount);

        public void Play()
        {
            stageState = StageState.Playing;
            stageTimer.StartTimer(currentStageData.duration);
            customerCounter.ResetCustomerCount();
            onStageChanged?.Invoke(stageState);
        }

        public void Pause()
        {
            stageState = StageState.Paused;
            stageTimer.PauseTimer();
            onStageChanged?.Invoke(stageState);
        }

        public void Resume()
        {
            stageState = StageState.Playing;
            stageTimer.ResumeTimer();
            onStageChanged?.Invoke(stageState);
        }

        public static StageData LoadStageData(string path)
        {
            var stageData = Resources.Load<StageData>(path);
            return stageData;
        }

        public static List<StageData> GetAllInFolder(string stageName)
        {
            var stageData = Resources.LoadAll<StageData>($"Level/{stageName}");
            return new List<StageData>(stageData);
        }
    }

    public enum StageState
    {
        Idle,
        Playing,
        Paused,
        Ended
    }
}