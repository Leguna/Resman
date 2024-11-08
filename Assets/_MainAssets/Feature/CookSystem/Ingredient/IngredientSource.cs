using System.Collections.Generic;
using Touch;
using UnityEngine;

namespace CookSystem.Ingredient
{
    public class IngredientSource : MonoBehaviour, ITouchable
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private List<Utensil> utensils;
        [SerializeField] private IngredientData ingredient;

        private void Awake()
        {
            Init(ingredient);
        }

        public void Init(IngredientData data)
        {
            ingredient = data;
            icon.sprite = ingredient.rawIcon;
            icon.gameObject.SetActive(true);
        }

        private void AddToAvailableUtensil()
        {
            foreach (var utensil in utensils)
            {
                if (!utensil.AddIngredient(ingredient)) continue;
                utensil.StartCook();
                break;
            }
        }

        public void OnTouch()
        {
            AddToAvailableUtensil();
        }
        
        public void Show()
        {
            icon.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            icon.gameObject.SetActive(false);
        }
    }
}