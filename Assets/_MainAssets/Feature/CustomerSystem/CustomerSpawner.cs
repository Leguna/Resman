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

        private List<CustomerComponent> _customers = new();

        public void Init()
        {
            _spawnTimer = TryGetComponent(out Timer timer) ? timer : gameObject.AddComponent<Timer>();
            _spawnTimer.onTimerEnded = SpawnCustomer;
            _customers = new List<CustomerComponent>();
            _spawnTimer.StartTimer(Random.Range(spawnInterval.min, spawnInterval.max));
        }

        private void SpawnCustomer()
        {
            _spawnTimer.StartTimer(Random.Range(spawnInterval.min, spawnInterval.max));
            if (_customers.Count >= customerPositions.Count) return;
            var pos = UnoccupiedPosition();
            var randomCustomerData = customerData[Random.Range(0, customerData.Count)];
            var customer = Instantiate(customerPrefab, pos.position, Quaternion.identity, pos);
            customer.Init(randomCustomerData, OnCustomerLeave);
            _customers.Add(customer);
        }

        private void OnValidate()
        {
            if (spawnInterval.min > spawnInterval.max) spawnInterval.max = spawnInterval.min;
            if (spawnInterval.min < 0) spawnInterval.min = 0;
            if (spawnInterval.max < 0) spawnInterval.max = 0;
        }


        private void OnCustomerLeave(CustomerComponent customer)
        {
            _customers.Remove(customer);
            Destroy(customer.gameObject);
        }

        public void Reset()
        {
            _customers.ForEach(customer => Destroy(customer.gameObject));
            _customers.Clear();
        }

        public void Resume()
        {
            foreach (var customer in _customers)
            {
                DOTween.Play(customer.transform);
            }

            _spawnTimer.ResumeTimer();
        }

        public void Pause()
        {
            foreach (var customer in _customers)
            {
                DOTween.Pause(customer.transform);
            }

            _spawnTimer.PauseTimer();
        }

        public void Restart()
        {
            Reset();
            Init();
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
                foodPlate.RemoveFood();
                return;
            }
        }

        [Serializable]
        private struct SpawnInterval
        {
            public float min;
            public float max;
        }
    }
}