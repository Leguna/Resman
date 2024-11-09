using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Combo
{
    public class ComboSystem : MonoBehaviour
    {
        [SerializeField] private Image comboBar;
        [SerializeField] private float comboTime = 3f;

        private int _comboCount = 0;
        private Timer _timer;

        private void Awake()
        {
            if (_timer == null) _timer = gameObject.AddComponent<Timer>();
        }

        public void Init()
        {
            _comboCount = 0;
            comboBar.fillAmount = 0;
            _timer.onTimerEnded += ResetCombo;
        }

        private void Update()
        {
            if (_timer.isRunning)
            {
                comboBar.fillAmount = _timer.GetProgress();
            }
        }

        private void ResetCombo()
        {
            _comboCount = 0;
            comboBar.fillAmount = 0;
            _timer.Stop();
        }

        public void AddCombo()
        {
            _comboCount++;
            comboBar.DOFillAmount((float)_comboCount / 10, 0.5f);
            _timer.Start(comboTime);
        }

        public void PauseComboTimer()
        {
            _timer.Pause();
        }

        public void ResumeComboTimer()
        {
            _timer.Resume();
        }
    }
}