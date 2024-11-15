using System;
using Animation;
using CookSystem.Ingredient;
using Touch;
using UnityEngine;
using UnityEngine.UI;
using UpgradeSystem;

namespace CookSystem
{
    [RequireComponent(typeof(AnimateScale))]
    public class Utensil : MonoBehaviour, IOnDoubleTap, ITouchable
    {
        [SerializeField] private Image progress;
        [SerializeField] private SpriteRenderer foodIcon;
        [SerializeField] private SpriteRenderer utensilIcon;
        [SerializeField] private UtensilData utensilData;

        [SerializeField] private AnimateScale animateScale;

        private IngredientComponent _cookedIngredientComponent;

        private bool _isCooking;
        private float _cookTime;


        private Action<FoodItemData, Utensil> _onServe;

        private void Update()
        {
            if (!_isCooking) return;
            Cook();
        }

        public void Init(Action<FoodItemData, Utensil> onServe)
        {
            _onServe = onServe;
            foodIcon.gameObject.SetActive(false);
            Reset();
        }

        private void Reset()
        {
            _isCooking = false;
            _cookedIngredientComponent = null;
            // TODO: Change color to image
            utensilIcon.color = utensilData.color;
            progress.fillAmount = 0;
            progress.gameObject.SetActive(false);
            foodIcon.gameObject.SetActive(false);
        }

        public bool AddIngredient(IngredientData ingredientComponent)
        {
            if (_cookedIngredientComponent != null) return false;
            _cookedIngredientComponent = TryGetComponent<IngredientComponent>(out var component)
                ? component
                : gameObject.AddComponent<IngredientComponent>();
            _cookedIngredientComponent.Init(ingredientComponent);
            _cookedIngredientComponent.onCooked += OnCooked;
            _cookedIngredientComponent.onBurnt += OnBurnt;
            UpdateUI();
            foodIcon.gameObject.SetActive(true);
            progress.gameObject.SetActive(true);
            animateScale.PlayAnimation();
            return true;
        }

        private void OnCooked()
        {
            progress.color = Color.red;
            UpdateUI();
            animateScale.PlayAnimation();
        }

        private void OnBurnt()
        {
            _isCooking = false;
            UpdateUI();
            animateScale.PlayAnimation();
        }

        private void UpdateUI()
        {
            progress.fillAmount = _cookedIngredientComponent.GetProgress();
            foodIcon.sprite = _cookedIngredientComponent.GetIcon();
        }

        private void Cook()
        {
            _cookedIngredientComponent.Cook(utensilData.GetCookSpeed() * GameManager.GameDeltaTime);
            progress.fillAmount = _cookedIngredientComponent.GetProgress();
        }


        public void StartCook()
        {
            progress.color = Color.green;
            _cookedIngredientComponent.Reset();
            _isCooking = true;
            animateScale.PlayAnimation();
        }

        public void RemoveIngredient()
        {
            _isCooking = false;
            progress.gameObject.SetActive(false);
            foodIcon.gameObject.SetActive(false);
            Destroy(_cookedIngredientComponent);
            _cookedIngredientComponent = null;
            animateScale.PlayAnimation();
        }

        private void PutOut()
        {
            if (_cookedIngredientComponent == null) return;
            if (_cookedIngredientComponent.state != IngredientState.Done) return;
            _onServe?.Invoke(_cookedIngredientComponent.GetFoodItemData(), this);
        }

        private void TrashBurnt()
        {
            if (_cookedIngredientComponent == null) return;
            if (_cookedIngredientComponent.state != IngredientState.Burnt) return;
            RemoveIngredient();
        }

        public void OnTouch() => PutOut();

        public void OnDoubleTap() => TrashBurnt();

    }
}