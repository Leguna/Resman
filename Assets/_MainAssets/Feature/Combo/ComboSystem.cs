using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combo
{
    public class ComboSystem : MonoBehaviour
    {
        [SerializeField] private Image comboBar;
        [SerializeField] private TMP_Text comboText;
        [SerializeField] private float comboTimeLimit = 3f;

        private int _comboCount;
        private Timer _timer;

        private void Awake()
        {
            if (_timer == null) _timer = gameObject.AddComponent<Timer>();
        }

        public void Init()
        {
            ResetCombo();
            _timer.onTimerEnded += ResetCombo;
            Show();
            _timer.Start(0);
        }

        private void Update()
        {
            if (_timer.isRunning)
            {
                comboBar.fillAmount = _timer.GetProgress();
            }
        }

        public void ResetCombo()
        {
            _comboCount = 0;
            comboBar.fillAmount = 0;
            _timer.Stop();
            comboText.text = _comboCount.ToString();
        }

        public void AddCombo()
        {
            _comboCount++;
            comboBar.DOFillAmount(1, 0.2f);
            _timer.Start(comboTimeLimit);
            comboText.text = _comboCount.ToString();
        }

        public void Hide()
        {
            Pause();
            comboBar.transform.parent.gameObject.SetActive(false);
        }

        private void Show()
        {
            comboBar.transform.parent.gameObject.SetActive(true);
        }

        public void Pause()
        {
            _timer.Pause();
        }

        public void Resume()
        {
            _timer.Resume();
        }

        public float GetComboCount()
        {
            return _comboCount;
        }
    }
}