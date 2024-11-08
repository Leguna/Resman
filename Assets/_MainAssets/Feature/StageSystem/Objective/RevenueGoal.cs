using System;
using Events;

namespace StageSystem.Objective
{
    [Serializable]
    public class RevenueGoal : Goal
    {
        public float revenueGoal;
        public float currentRevenue;

        public override bool IsGoalAchieved()
        {
            return currentRevenue >= revenueGoal;
        }

        public string GetProgress()
        {
            return $"{currentRevenue}/{revenueGoal}";
        }

        public string GetProgressPercentage()
        {
            return $"{currentRevenue / revenueGoal}%";
        }

        public override void UpdateProgress(AddObjectiveEvent addObjectiveEvent)
        {
            currentRevenue += addObjectiveEvent.Amount;
            TrackProgress();
        }
    }
}