using System;
using System.Collections.Generic;
using StageSystem.Objective;
using UnityEngine;
using Utilities;

namespace StageSystem
{
    public class Stage : SingletonMonoBehaviour<Stage>
    {
        public StageData currentStageData;
        public StageState stageState;
        [SerializeField] private StageTimer stageTimer;

        private readonly Action<StageState> _onStageChanged = delegate { };

        public void Init(StageData stageData)
        {
            currentStageData = stageData;
            stageState = StageState.Idle;
            _onStageChanged?.Invoke(stageState);
            stageTimer.Init(stageData.duration);
            stageTimer.OnTimerEnded += () =>
            {
                stageState = StageState.Ended;
                _onStageChanged?.Invoke(stageState);
            };
        }

        public void ObjectiveUpdate(ObjectiveType objectiveType, float amount) =>
            currentStageData.UpdateObjective(objectiveType, amount);

        public void Play()
        {
            stageState = StageState.Playing;
            stageTimer.StartTimer(currentStageData.duration);
            _onStageChanged?.Invoke(stageState);
        }

        public void Pause()
        {
            stageState = StageState.Paused;
            stageTimer.PauseTimer();
            _onStageChanged?.Invoke(stageState);
        }

        public void Resume()
        {
            stageState = StageState.Playing;
            stageTimer.ResumeTimer();
            _onStageChanged?.Invoke(stageState);
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