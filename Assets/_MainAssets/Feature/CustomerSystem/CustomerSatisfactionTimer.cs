using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CustomerSystem
{
    public class CustomerSatisfactionTimer : Timer
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private Canvas canvas;

        private bool _isDispleased;

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }

        public void Init(float timeLimit, Action onTimerEnd = null)
        {
            SetDuration(timeLimit);
            SetListener(() =>
            {
                onTimerEnd?.Invoke();
                progressBar.DOFillAmount(0, 0.2f);
                canvas.gameObject.SetActive(false);
            });
            canvas.gameObject.SetActive(true);
            StartTimer(timeLimit);
        }

        private void Update()
        {
            if (!isRunning) return;
            progressBar.fillAmount = GetProgress();
            if (GetProgress() <= 0.3f && GetProgress() > 0.2f && !_isDispleased)
            {
                _isDispleased = true;
                progressBar.DOColor(Color.red, 0.2f);
            }
        }

        public void AddTime(float time)
        {
            DecreaseElapsed(time);
            progressBar.fillAmount = GetProgress();
        }

        public override void Stop()
        {
            base.Stop();
            DOTween.Kill(progressBar);
            canvas.gameObject.SetActive(false);
        }
    }
}