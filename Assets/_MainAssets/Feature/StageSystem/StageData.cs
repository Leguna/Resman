using System.Collections.Generic;
using System.Linq;
using Core;
using StageSystem.Objective;
using UnityEngine;

namespace StageSystem
{
    [CreateAssetMenu(fileName = "StageData", menuName = "StageSystem/StageData")]
    public class StageData : MyScriptableObject
    {
        public List<ObjectiveData> objectives = new();

        public int customerLimit;
        public float duration;

        public void SetObjectives(List<ObjectiveData> objectives)
        {
            this.objectives = objectives;
        }

        public List<ObjectiveType> GetObjectives() => objectives.Select(objective => objective.objectiveType).ToList();

        public void UpdateObjective(ObjectiveType objectiveType, float amount)
        {
            var objective = objectives.Find(obj => obj.objectiveType == objectiveType);
            objective.UpdateObjective(amount);
            Debug.Log($"Objective {objectiveType} updated with amount {amount}");
        }

        public string GetObjectiveDescription()
        {
            var objectiveDescriptions = objectives.Select(objective => objective.GetObjectiveDescription()).ToList();
            return string.Join("\n", objectiveDescriptions);
        }

        public bool AreAllObjectivesAchieved() => objectives.All(objective => objective.IsAchieved);
    }
}