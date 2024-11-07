using System;
using Animation;
using CookSystem.Ingredient;
using Touch;
using UnityEngine;
using UnityEngine.UI;

namespace CookSystem
{
    public class Utensil : MonoBehaviour, IOnDoubleTap, ITouchable
    {
        [SerializeField] private Image progress;
        [SerializeField] private SpriteRenderer foodIcon;
        [SerializeField] private SpriteRenderer utensilIcon;
        [SerializeField] private UtensilData utensilData;

        private IngredientComponent _cookedIngredientComponent;

        private bool _isCooking;
        private float _cookTime;
        
        private AnimateScale _animateScale;

        private void Awake()
        {
            _animateScale = GetComponent<AnimateScale>();
            Init(utensilData);
        }

        private void Update()
        {
            if (!_isCooking) return;
            Cook();
        }

        private void Init(UtensilData data)
        {
            utensilData = data;
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
            _animateScale.PlayAnimation();
            return true;
        }

        private void OnCooked()
        {
            progress.color = Color.red;
            UpdateUI();
            _animateScale.PlayAnimation();
        }

        private void OnBurnt()
        {
            _isCooking = false;
            UpdateUI();
            _animateScale.PlayAnimation();
        }

        private void UpdateUI()
        {
            progress.fillAmount = _cookedIngredientComponent.GetProgress();
            foodIcon.sprite = _cookedIngredientComponent.GetIcon();
        }

        private void Cook()
        {
            _cookedIngredientComponent.Cook(utensilData.GetCookSpeed() * Time.deltaTime * GameManager.GameSpeed);
            progress.fillAmount = _cookedIngredientComponent.GetProgress();
        }


        public void StartCook()
        {
            progress.color = Color.green;
            _cookedIngredientComponent.Reset();
            _isCooking = true;
            _animateScale.PlayAnimation();
        }

        private void RemoveIngredient()
        {
            _isCooking = false;
            progress.gameObject.SetActive(false);
            foodIcon.gameObject.SetActive(false);
            Destroy(_cookedIngredientComponent);
            _cookedIngredientComponent = null;
            _animateScale.PlayAnimation();
        }

        private void PutOut()
        {
            if (_cookedIngredientComponent == null) return;
            if (_cookedIngredientComponent.state != IngredientState.Done) return;
            RemoveIngredient();
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