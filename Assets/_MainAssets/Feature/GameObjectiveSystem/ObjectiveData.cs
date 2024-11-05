using Core;
using UnityEngine;

namespace GameObjectiveSystem
{
    [CreateAssetMenu(fileName = "ObjectiveData", menuName = "RestaurantGame/ObjectiveData")]
    public class ObjectiveData : MyScriptableObject
    {
        public int revenueGoal;
        public float satisfactionGoal;
        public int serveTimeGoal;
        public int comboGoal;
    }
}