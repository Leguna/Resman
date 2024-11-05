using Core;
using UnityEngine;

namespace CustomerSatisfactionSystem
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "RestaurantGame/CustomerData")]
    public class CustomerData : MyScriptableObject
    {
        public float patience;
        public float satisfactionThreshold;
    }
}