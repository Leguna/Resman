using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "ToppingItemData", menuName = "RestaurantGame/ToppingItemData")]
    public class ToppingItemData : ScriptableObject
    {
        public string toppingName;
        public Color bgColor = Color.white;
        public float price = 2;
    }
}