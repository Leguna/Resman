using System;
using TMPro;
using UnityEngine;

namespace StageSystem.Completion
{
    public class StageCompletionComponent : MonoBehaviour
    {
        [SerializeField] private TMP_Text completionText;
        [SerializeField] private Timer timer;

        private StageCompletion _stageCompletion;

        private bool _isCompleted = true;

        public Action closeShop = delegate { };

        public void Init(StageData stageData, Action stageFinished)
        {
            if (timer == null) timer = gameObject.AddComponent<Timer>();
            _stageCompletion = stageData.completionType switch
            {
                CompletionType.Timed => new TimerCompletion(stageData, timer),
                CompletionType.Customer => new CustomerCountCompletion(stageData),
                _ => _stageCompletion
            };
            _isCompleted = false;
            Show();
            _stageCompletion.onStageCompleted = () =>
            {
                stageFinished?.Invoke();
                Hide();
            };
        }

        private void Update()
        {
            if (_isCompleted) return;
            UpdateView();
        }

        private void UpdateView()
        {
            completionText.text = _stageCompletion.GetCompletionText();
        }

        public void Pause()
        {
            _stageCompletion.OnPause();
        }

        public void Resume()
        {
            _stageCompletion.OnResume();
        }

        public void Show()
        {
            completionText.gameObject.SetActive(true);
        }

        public void Hide()
        {
            completionText.gameObject.SetActive(false);
        }

        public void DecrementCustomerCount()
        {
            if (_stageCompletion is CustomerCountCompletion customerCountCompletion)
            {
                customerCountCompletion.DecrementCustomerCount();
                UpdateView();
            }
        }

        public bool CheckIfCompleted()
        {
            _isCompleted = _stageCompletion.IsCompleted();
            return _isCompleted;
        }

        public void UpdateCompletion()
        {
            _stageCompletion.UpdateCompletion();
        }
    }
}