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
    public class CustomerComponent : MonoBehaviour, IReceive<FoodItemData>
    {
        [SerializeField] private Order order;
        [SerializeField] private SpriteRenderer foodSprite;
        [SerializeField] private SpriteRenderer customerSprite;
        [SerializeField] private CustomerSatisfactionTimer satisfactionTimer;

        [SerializeField] private CustomerData customerData;

        [SerializeField] private List<FoodItemData> allowedFoodItemData = new();

        private Action _onLeave;

        private void Start()
        {
            Init();
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 40), "Receive Food"))
            {
                order.TryReceive(allowedFoodItemData.FirstOrDefault());
            }
        }

        private void Init(Action onLeave = null)
        {
            _onLeave = onLeave;
            customerSprite.sprite = customerData.customerSprite;
            EnterQueue();
        }

        private void SetOrder(Order order)
        {
            this.order = order;
            satisfactionTimer.Init(customerData.patience, Leave);
        }


        private void Pay(float objPrice)
        {
            print("CustomerComponent: Paying " + objPrice);
            satisfactionTimer.Stop();
            Leave();
        }

        private void EnterQueue()
        {
            transform.position = new Vector3(-15, 0, 0);
            transform.DOMoveX(0, 2f).OnComplete(() =>
            {
                GenerateRandomOrder();
                satisfactionTimer.Init(customerData.patience, Leave);
                UpdateUI();
            });
        }

        private void UpdateUI()
        {
            foodSprite.gameObject.SetActive(true);
            // Need handle multiple food items
            if (!order?.IsOrderComplete ?? false)
            {
                foodSprite.sprite = order.GetNextFoodItem().icon;
                foodSprite.transform.parent.gameObject.SetActive(true);
                foodSprite.gameObject.SetActive(true);
            }
        }

        private void OnOrderComplete(List<FoodItemData> obj)
        {
            print("CustomerComponent: Order Complete");
            Pay(obj.Sum(foodItemData => foodItemData.price));
        }

        private void OnOrderFulfilled(FoodItemData obj)
        {
            print("CustomerComponent: Order " + obj + " fulfilled");
        }

        private void Leave()
        {
            CloseOrder();
            transform.DOMoveX(15, 2f).OnComplete(() =>
            {
                _onLeave?.Invoke();
                Destroy(gameObject);
            });
        }

        public bool TryReceive(FoodItemData item)
        {
            if (!allowedFoodItemData.Contains(item)) return false;
            order.TryReceive(item);
            return true;
        }

        public bool CanReceive(FoodItemData item)
        {
            return order != null && order.CanReceive(item);
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

        public void GenerateRandomOrder()
        {
            var randomOrder = new List<FoodItemData>
            {
                allowedFoodItemData[Random.Range(0, allowedFoodItemData.Count)]
            };

            SetOrder(new Order(randomOrder, OnOrderComplete, OnOrderFulfilled));
        }
    }
}