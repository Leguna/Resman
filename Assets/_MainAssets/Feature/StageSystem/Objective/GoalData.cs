using System;
using UnityEngine;

namespace StageSystem.Objective
{
    [Serializable]
    public class GoalData
    {
        [SerializeField] internal float targetAmount;
        [SerializeField] internal float currentAmount;

        public GoalType goalType = GoalType.RevenueGoal;
        public string GetObjectiveDescription() => $"Achieve {targetAmount} in {goalType}";
        public void UpdateObjective(float amount) => currentAmount += amount;

        public void SetObjective(float amount) => currentAmount = amount;

        public void ResetObjective() => currentAmount = 0;

        public float GetProgress() => currentAmount / targetAmount;

        public string GetProgressText()
        {
            switch (goalType)
            {
                case GoalType.RevenueGoal:
                    return $"Revenue: {currentAmount}/{targetAmount}";
                case GoalType.SatisfactionGoal:
                    return $"Satisfaction: {currentAmount}/{targetAmount}";
                case GoalType.ServeGoal:
                    return $"Serve: {currentAmount}/{targetAmount}";
                case GoalType.ChainComboGoal:
                    return $"Chain Combo: {currentAmount}/{targetAmount}";
                default:
                    return string.Empty;
            }
        }

        public ObjectiveResult GetResult()
        {
            if (currentAmount >= targetAmount)
            {
                return new ObjectiveResult
                {
                    title = "Objective Completed",
                    content = $"You have completed the {goalType} objective",
                };
            }

            return new ObjectiveResult
            {
                title = "Objective Failed",
                content = $"You have failed the {goalType} objective",
            };
        }
        
        public bool IsCompleted() => currentAmount >= targetAmount;
    }

    public struct ObjectiveResult
    {
        public string title;
        public string content;
    }

    [Serializable]
    public enum GoalType
    {
        RevenueGoal,
        SatisfactionGoal,
        ServeGoal,
        ChainComboGoal
    }
}