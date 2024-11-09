namespace StageSystem.Completion
{
    public class TimerCompletion : StageCompletion
    {
        private readonly Timer _timer;
        private readonly float _duration;
        private bool _isCompleted;

        private void OnTimerEnded()
        {
            onStageCompleted?.Invoke();
        }

        public sealed override void Init(StageData stageData)
        {
            _timer.Start(_duration);
            _isCompleted = false;
            _timer.onTimerEnded += OnTimerEnded;
        }

        public override void UpdateCompletion()
        {
        }

        public override void ResetCompletion()
        {
            _timer.Stop();
        }

        public override void OnPause()
        {
            _timer.Pause();
        }

        public override void OnResume()
        {
            _timer.Resume();
        }

        public override string GetCompletionText()
        {
            if (_timer.GetRemainingTime() <= 0) return closedShopText;
            return $"Time Remaining: {_timer.GetRemainingTime():0}";
        }

        public override bool IsCompleted()
        {
            return _isCompleted;
        }

        public TimerCompletion(StageData stageData, Timer timer) : base(stageData)
        {
            _duration = stageData.duration;
            _timer = timer;
            Init(stageData);
        }
    }
}