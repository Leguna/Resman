using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace CookSystem
{
    public class KitchenSystem : SingletonMonoBehaviour<KitchenSystem>
    {
        [SerializeField] private List<FoodPlate> foodPlates;
        [SerializeField] private List<Utensil> utensils;

        protected override void Awake()
        {
            foreach (var utensil in utensils) utensil.onServe += OnUtensilServe;
        }

        private void OnUtensilServe(FoodItemData foodItemData, Utensil utensil)
        {
            foreach (var foodPlate in foodPlates)
                if (foodPlate.Receive(foodItemData))
                {
                    utensil.RemoveIngredient();
                    return;
                }
        }
    }
}