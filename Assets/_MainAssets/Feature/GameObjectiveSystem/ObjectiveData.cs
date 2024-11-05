using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "ObjectiveData", menuName = "RestaurantGame/ObjectiveData")]
    public class ObjectiveData : ScriptableObject
    {
        public string id;
        public int revenueGoal;
        public float satisfactionGoal;
        public int serveTimeGoal;
        public int comboGoal;
    }
}