using System;
using UnityEngine;

namespace StageSystem.Objective
{
    [Serializable]
    public class ObjectiveData
    {
        [SerializeField] internal float targetAmount;
        [SerializeField] internal float currentAmount;
        public ObjectiveType objectiveType;
        public Action onObjectiveAchieved = delegate { };

        public bool IsAchieved => currentAmount >= targetAmount;
        public string GetObjectiveDescription() => $"Achieve {targetAmount} in {objectiveType}";

        public void SetListener(Action onAchieved)
        {
            onObjectiveAchieved = onAchieved;
        }

        public void UpdateObjective(float amount)
        {
            currentAmount += amount;
            if (!IsAchieved) return;
            onObjectiveAchieved?.Invoke();
            onObjectiveAchieved = delegate { };
        }

        public void ResetObjective()
        {
            currentAmount = 0;
        }

        public float GetProgress()
        {
            return currentAmount / targetAmount;
        }
    }

    [Serializable]
    public enum ObjectiveType
    {
        RevenueGoal,
        SatisfactionGoal,
        ServeGoal,
        ChainComboGoal
    }
}