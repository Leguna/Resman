using Core;
using TMPro;
using UnityEngine;

namespace StageSystem
{
    public class StageTimer : Timer
    {
        [SerializeField] private TMP_Text timerText;

        public void Init(float timeLimit)
        {
            timerText.text = Mathf.CeilToInt(GetRemainingTime()).ToString();
            SetDuration(timeLimit);
        }

        private void Update()
        {
            if (!isRunning) return;
            timerText.text = Mathf.CeilToInt(GetRemainingTime()).ToString();
        }
    }
}