using System;
using CookSystem;
using UnityEngine;

namespace OrderSystem
{
    [Serializable]
    public class Order : MonoBehaviour
    {
        public FoodItemData foodItem;
        public float cookingTime;
    }
}