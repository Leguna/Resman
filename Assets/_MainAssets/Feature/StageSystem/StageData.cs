using System;
using StageSystem.Objective;
using UnityEngine;

namespace StageSystem
{
    [CreateAssetMenu(fileName = "StageData", menuName = "StageSystem/StageData")]
    public class StageData : MyScriptableObject
    {
        public GoalData goalData;
        public CompletionType completionCondition;

        public int customerLimit = 5;
        public float duration = 60f;

        protected override void OnValidate()
        {
            try
            {
                base.OnValidate();
                switch (completionCondition)
                {
                    case CompletionType.Timed:
                        customerLimit = 0;
                        if (duration <= 0)
                            throw new Exception("Duration must be greater than 0");
                        break;
                    case CompletionType.Customer:
                        duration = 0;
                        if (customerLimit <= 0)
                            throw new Exception("Customer limit must be greater than 0");
                        break;
                    default:
                        throw new Exception("Invalid completion condition");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Validation Error in {name}: {e.Message}", this);
            }
        }
    }

    public enum CompletionType
    {
        Timed,
        Customer,
    }
}