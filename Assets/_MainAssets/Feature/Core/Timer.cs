using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class Timer : MonoBehaviour
    {
        public event Action OnTimerEnded;
        private float _duration;
        private float _elapsedTime;
        public bool isRunning;

        private void StartTimer()
        {
            StopAllCoroutines();
            _elapsedTime = 0;
            isRunning = true;
            StartCoroutine(TimerCoroutine());
        }

        public void StartTimer(float duration)
        {
            _duration = duration;
            StartTimer();
        }

        protected void SetDuration(float duration)
        {
            _duration = duration;
        }

        private void StopTimer()
        {
            isRunning = false;
            StopAllCoroutines();
        }

        public void PauseTimer()
        {
            isRunning = false;
            StopAllCoroutines();
        }

        public void ResumeTimer()
        {
            isRunning = true;
            StartCoroutine(TimerCoroutine());
        }

        public void ResetTimer()
        {
            _elapsedTime = 0;
            if (!isRunning) return;
            StopTimer();
            StartTimer(_duration);
        }

        public float GetElapsedTime()
        {
            return _elapsedTime;
        }

        protected float GetRemainingTime()
        {
            return _duration - _elapsedTime;
        }

        private IEnumerator TimerCoroutine()
        {
            while (isRunning && _elapsedTime < _duration)
            {
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (!(_elapsedTime >= _duration)) yield break;
            isRunning = false;
            OnTimerEnded?.Invoke();
        }
    }
}