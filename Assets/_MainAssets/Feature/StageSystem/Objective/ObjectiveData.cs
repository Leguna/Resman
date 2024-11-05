using System;
using UnityEngine;

namespace StageSystem.Objective
{
    [Serializable]
    public class ObjectiveData
    {
        public float targetAmount;
        [SerializeField] private float currentAmount;
        public ObjectiveType objectiveType;

        public bool IsAchieved => currentAmount >= targetAmount;
        public string GetObjectiveDescription() => $"Achieve {targetAmount} in {objectiveType}";

        public void UpdateObjective(float amount)
        {
            currentAmount += amount;
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