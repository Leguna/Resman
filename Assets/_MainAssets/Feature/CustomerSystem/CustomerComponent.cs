using System;
using System.Collections.Generic;
using System.Linq;
using CookSystem;
using DG.Tweening;
using OrderSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CustomerSystem
{
    public class CustomerComponent : MonoBehaviour
    {
        [SerializeField] private Order order;
        [SerializeField] private SpriteRenderer foodSprite;
        [SerializeField] private SpriteRenderer customerSprite;
        [SerializeField] private CustomerSatisfactionTimer satisfactionTimer;

        [SerializeField] private CustomerData customerData;

        [SerializeField] private List<FoodItemData> allowedFoodItemData = new();

        private Action<CustomerComponent> _onLeave;

        public void Init(CustomerData customerData, Action<CustomerComponent> onLeave)
        {
            this.customerData = customerData;
            _onLeave = onLeave;
            customerSprite.sprite = customerData.customerSprite;
            EnterQueue();
        }

        private void SetOrder(Order order)
        {
            this.order = order;
            satisfactionTimer.Init(customerData.patience, Leave);
        }


        private void RequestPay(float objPrice)
        {
            satisfactionTimer.Stop();
            Leave();
        }

        private void EnterQueue()
        {
            transform.position = new Vector3(-15, 0, 0);
            transform.DOLocalMoveX(0, 2f).OnComplete(() =>
            {
                GenerateRandomOrder();
                satisfactionTimer.Init(customerData.patience, Leave);
                UpdateUI();
            });
        }

        private void UpdateUI()
        {
            foodSprite.gameObject.SetActive(true);
            if (!(!order?.IsOrderComplete ?? false)) return;
            foodSprite.sprite = order.GetNextFoodItem().icon;
            foodSprite.transform.parent.gameObject.SetActive(true);
            foodSprite.gameObject.SetActive(true);
        }

        private void OnOrderComplete(List<FoodItemData> obj)
        {
            RequestPay(obj.Sum(foodItemData => foodItemData.price));
        }

        private void OnOrderFulfilled(FoodItemData obj)
        {
        }

        private void Leave()
        {
            CloseOrder();
            transform.DOLocalMoveX(15, 2f).OnComplete(() => { _onLeave?.Invoke(this); });
        }

        public bool TryReceive(FoodItemData item)
        {
            if (!allowedFoodItemData.Contains(item)) return false;
            if (order == null) return false;
            return order.TryReceive(item);
        }

        private void CloseOrder()
        {
            order = null;
            foodSprite.gameObject.SetActive(false);
            foodSprite.transform.parent.gameObject.SetActive(false);
            satisfactionTimer.Stop();
        }

        public void SetAllowedFoodItemData(List<FoodItemData> foodItemData)
        {
            foreach (var foodItem in foodItemData) allowedFoodItemData.Add(foodItem);
        }

        private void GenerateRandomOrder()
        {
            var randomOrder = new List<FoodItemData>
            {
                allowedFoodItemData[Random.Range(0, allowedFoodItemData.Count)]
            };

            SetOrder(new Order(randomOrder, OnOrderComplete, OnOrderFulfilled));
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}