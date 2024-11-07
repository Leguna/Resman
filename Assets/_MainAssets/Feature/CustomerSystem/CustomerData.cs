using UnityEngine;

namespace CustomerSystem
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "RestaurantGame/CustomerData")]
    public class CustomerData : MyScriptableObject
    {
        public float patience;
        public float satisfactionThreshold;
    }
}