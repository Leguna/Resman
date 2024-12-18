using System.Collections.Generic;
using Events;
using Touch;
using UnityEngine;
using Utilities;

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

        private void Init(IngredientData data)
        {
            ingredient = data;
            icon.sprite = ingredient.rawIcon;
            Hide();
        }

        private void AddToAvailableUtensil()
        {
            foreach (var utensil in utensils)
            {
                if (!utensil.AddIngredient(ingredient)) continue;
                utensil.StartCook();
                EventManager.TriggerEvent(new PlayAudioEvent { audioName = "cookingSound" });
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