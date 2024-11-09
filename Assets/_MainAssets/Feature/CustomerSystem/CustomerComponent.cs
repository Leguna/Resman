using System;
using System.Collections.Generic;
using CookSystem;
using CustomerSystem.OrderSystem;
using DG.Tweening;
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

        [SerializeField] private List<SpriteRenderer> toppingSprites;

        [SerializeField] private CustomerData customerData;

        [SerializeField] private List<FoodItemData> allowedFoodItemData = new();

        private Action<CustomerComponent, CustomerLeaveData> _onLeave;

        public void Init(CustomerData customerData, Action<CustomerComponent, CustomerLeaveData> onLeave)
        {
            this.customerData = customerData;
            _onLeave = onLeave;
            customerSprite.sprite = customerData.customerSprite;
            EnterQueue();
        }

        private void SetOrder(Order order)
        {
            this.order = order;
            satisfactionTimer.Init(customerData.patience, AngryLeave);
        }


        private void OrderServed()
        {
            satisfactionTimer.Stop();
            Leave();
        }

        private void EnterQueue()
        {
            transform.position = new Vector3(-15, 0, 0);
            transform.DOLocalMoveX(0, 2f).OnComplete(() =>
            {
                var randomOrder = GenerateRandomOrder();
                SetOrder(randomOrder);
                satisfactionTimer.Init(customerData.patience, AngryLeave);
                UpdateUI();
            });
        }

        private void AngryLeave()
        {
            Leave(new CustomerLeaveData(leaveReason: CustomerLeaveReason.Angry));
        }

        private void UpdateUI()
        {
            foodSprite.gameObject.SetActive(true);
            if (order == null || order.allOrders.Count == 0) return;
            var orderedFood = order.allOrders[0];
            foodSprite.sprite = orderedFood.foodItemData.icon;
            for (var i = 0; i < toppingSprites.Count; i++)
            {
                if (i < orderedFood.toppingItemData.Count)
                {
                    toppingSprites[i].gameObject.SetActive(true);
                    toppingSprites[i].sprite = orderedFood.toppingItemData[i].icon;
                }
                else
                {
                    toppingSprites[i].gameObject.SetActive(false);
                }
            }

            foodSprite.transform.parent.gameObject.SetActive(true);
            foodSprite.gameObject.SetActive(true);
        }

        private void OnOrderComplete(List<OrderedFoodItemData> orderedFoodItemDatas)
        {
            OrderServed();
        }

        private void OnOrderFulfilled(OrderedFoodItemData orderedFoodItemData)
        {
        }

        private void Leave(CustomerLeaveData customerLeaveData = default)
        {
            CloseOrder();
            transform.DOLocalMoveX(15, 2f).OnComplete(() => { _onLeave?.Invoke(this, customerLeaveData); });
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

        private Order GenerateRandomOrder()
        {
            var randomFood = allowedFoodItemData[Random.Range(0, allowedFoodItemData.Count)];
            var randomTopping = new List<ToppingItemData>();
            foreach (var topping in randomFood.allowedToppings)
            {
                var isAdd = Random.Range(0, 2) == 1;
                if (isAdd) randomTopping.Add(topping);
            }

            var randomOrder = new OrderedFoodItemData(randomFood, randomTopping);

            var orders = new List<OrderedFoodItemData> { randomOrder };

            return new Order(orders, OnOrderComplete, OnOrderFulfilled);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }

        public OrderedFoodItemData CanReceive(OrderedFoodItemData foodPlateOrderedFoodItemData)
        {
            if (order == null) return null;
            return order.CanReceive(foodPlateOrderedFoodItemData);
        }

        public int Receive(OrderedFoodItemData foodPlateOrderedFoodItemData)
        {
            var received = order.Receive(foodPlateOrderedFoodItemData);
            UpdateUI();
            return received;
        }
    }
}