using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "RestaurantGame/CustomerData")]
    public class CustomerData : ScriptableObject
    {
        public float patience;
        public float satisfactionThreshold;
        
    }
}