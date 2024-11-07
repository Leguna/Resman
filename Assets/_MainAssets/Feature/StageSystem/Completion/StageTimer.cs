using System;
using TMPro;
using UnityEngine;

namespace StageSystem.Completion
{
    public class StageTimer : Timer
    {
        [SerializeField] private TMP_Text timerText;

        private void Start()
        {
            timerText.gameObject.SetActive(false);
        }

        public void Init(float timeLimit, Action onTimerEnd = null)
        {
            timerText.text = Mathf.CeilToInt(GetRemainingTime()).ToString();
            SetDuration(timeLimit);
            timerText.gameObject.SetActive(true);
            SetListener(() =>
            {
                onTimerEnd?.Invoke();
                timerText.text = "Closed";
            });
        }

        private void Update()
        {
            if (!isRunning) return;
            timerText.text = Mathf.CeilToInt(GetRemainingTime()).ToString();
        }
    }
}