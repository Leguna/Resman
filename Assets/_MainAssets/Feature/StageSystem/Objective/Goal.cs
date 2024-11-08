using System;
using Events;
using UnityEngine;
using Utilities;

namespace StageSystem.Objective
{
    [Serializable]
    public abstract class Goal : MonoBehaviour
    {
        public GoalData goalData;
        public Action onGoalAchieved = delegate { };
        public abstract bool IsGoalAchieved();
        public abstract void UpdateProgress(AddObjectiveEvent addObjectiveEvent);

        public void TrackProgress()
        {
            if (IsGoalAchieved()) onGoalAchieved?.Invoke();
        }

        public void OnEnable()
        {
            EventManager.AddEventListener<AddObjectiveEvent>(OnAddObjectiveEvent);
        }

        public void OnDisable()
        {
            EventManager.RemoveEventListener<AddObjectiveEvent>(OnAddObjectiveEvent);
        }

        private void OnAddObjectiveEvent(AddObjectiveEvent data)
        {
            UpdateProgress(data);
        }
    }
}