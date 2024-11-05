using UnityEngine;

namespace Utilities
{
    public class Timer : MonoBehaviour
    {
        public bool isRunning;
        private float _timeElapsed;

        private void Update()
        {
            if (isRunning) _timeElapsed += Time.deltaTime;
        }

        public void StartTimer()
        {
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public void ResetTimer()
        {
            _timeElapsed = 0;
        }

        public float GetTimeElapsed()
        {
            return _timeElapsed;
        }

        public string GetTimeElapsedFormatted()
        {
            return $"{_timeElapsed / 3600:00}:{_timeElapsed / 60:00}:{_timeElapsed % 60:00}";
        }
    }
}