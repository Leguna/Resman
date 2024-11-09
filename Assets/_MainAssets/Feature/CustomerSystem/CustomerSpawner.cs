using System;
using System.Collections.Generic;
using CookSystem;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CustomerSystem
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> customerPositions;
        [SerializeField] private List<CustomerData> customerData;
        [SerializeField] private SpawnInterval spawnInterval;
        [SerializeField] private CustomerComponent customerPrefab;
        private Timer _spawnTimer;


        private Action<CustomerLeaveData> _onCustomerLeave = delegate { };
        private Action _onCustomerEnter = delegate { };
        private Action<int> _onPaymentReceived = delegate { };

        private List<CustomerComponent> _customers = new();

        public bool isShopOpen;

        private void Awake()
        {
            _spawnTimer = TryGetComponent(out Timer timer) ? timer : gameObject.AddComponent<Timer>();
        }

        public void Init(Action<int> paymentReceived, Action<CustomerLeaveData> onLastCustomerLeave,
            Action customerEnter)
        {
            _onCustomerLeave = onLastCustomerLeave;
            _onCustomerEnter = customerEnter;
            _onPaymentReceived = paymentReceived;
            _spawnTimer.onTimerEnded = SpawnCustomer;
            _customers = new List<CustomerComponent>();
            _spawnTimer.Start(Random.Range(spawnInterval.min, spawnInterval.max));
            isShopOpen = true;
        }

        private void SpawnCustomer()
        {
            if (!isShopOpen) return;
            _spawnTimer.Start(Random.Range(spawnInterval.min, spawnInterval.max));
            if (_customers.Count >= customerPositions.Count) return;
            var pos = UnoccupiedPosition();
            var randomCustomerData = customerData[Random.Range(0, customerData.Count)];
            var customer = Instantiate(customerPrefab, pos.position, Quaternion.identity, pos);
            customer.Init(randomCustomerData, OnCustomerLeave);
            _customers.Add(customer);
            _onCustomerEnter?.Invoke();
        }

        private void OnValidate()
        {
            if (spawnInterval.min > spawnInterval.max) spawnInterval.max = spawnInterval.min;
            if (spawnInterval.min < 0) spawnInterval.min = 0;
            if (spawnInterval.max < 0) spawnInterval.max = 0;
        }


        private void OnCustomerLeave(CustomerComponent customer, CustomerLeaveData customerLeaveData)
        {
            _customers.Remove(customer);
            _onCustomerLeave?.Invoke(new CustomerLeaveData
            {
                isLastCustomer = _customers.Count == 0,
                leaveReason = customerLeaveData.leaveReason
            });
            Destroy(customer.gameObject);
        }

        public void Reset()
        {
            isShopOpen = false;
            _customers.ForEach(customer => Destroy(customer.gameObject));
            _customers.Clear();
            _spawnTimer.Stop();
        }

        public void Resume()
        {
            foreach (var customer in _customers)
            {
                DOTween.Play(customer.transform);
            }

            _spawnTimer.Resume();
        }

        public void Pause()
        {
            foreach (var customer in _customers)
            {
                DOTween.Pause(customer.transform);
            }

            _spawnTimer.Pause();
        }

        public void Restart()
        {
            Reset();
            Init(_onPaymentReceived, _onCustomerLeave, _onCustomerEnter);
        }

        private Transform UnoccupiedPosition()
        {
            return customerPositions.Find(position => position.childCount == 0);
        }

        public void OnPlateServe(FoodItemData foodItemData, FoodPlate foodPlate)
        {
            foreach (var customer in _customers)
            {
                if (!customer.TryReceive(foodItemData)) continue;
                _onPaymentReceived?.Invoke(foodItemData.price);
                foodPlate.RemoveFood();
                return;
            }
        }

        public void CloseShop()
        {
            isShopOpen = false;
        }

        [Serializable]
        private struct SpawnInterval
        {
            public float min;
            public float max;
        }
    }
}