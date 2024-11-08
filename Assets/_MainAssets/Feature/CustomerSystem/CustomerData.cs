using UnityEngine;

namespace CustomerSystem
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "RestaurantGame/CustomerData")]
    public class CustomerData : MyScriptableObject
    {
        public float patience = 10;
        public Sprite customerSprite;
    }
}