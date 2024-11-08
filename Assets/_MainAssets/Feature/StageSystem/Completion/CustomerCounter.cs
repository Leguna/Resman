using System;
using TMPro;
using UnityEngine;

namespace StageSystem.Completion
{
    public class CustomerCounter : MonoBehaviour
    {
        private int _customerCount;
        private int _customerLimit;
        private Action _onCustomerLimitReached;

        [SerializeField] private TMP_Text customerCountText;

        private bool IsCustomerLimitReached => _customerCount >= _customerLimit;

        private void Start()
        {
            customerCountText.gameObject.SetActive(false);
        }

        public void Init(int customerLimit, Action onCustomerLimitReached)
        {
            _customerLimit = customerLimit;
            _onCustomerLimitReached = onCustomerLimitReached;
            customerCountText.gameObject.SetActive(true);
        }

        public void IncrementCustomerCount()
        {
            _customerCount++;
            customerCountText.text = $"{_customerLimit - _customerCount}";
            if (!IsCustomerLimitReached) return;
            customerCountText.text = "Closed";
            _onCustomerLimitReached?.Invoke();
        }

        public void ResetCustomerCount()
        {
            _customerCount = 0;
            customerCountText.text = $"{_customerLimit - _customerCount}";
        }
    }
}