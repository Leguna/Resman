namespace StageSystem.Completion
{
    public class CustomerCountCompletion : StageCompletion
    {
        private int _totalCustomerCount;

        public sealed override void Init(StageData stageData)
        {
            this.stageData = stageData;
            _totalCustomerCount = stageData.customerLimit;
        }

        public void DecrementCustomerCount()
        {
            _totalCustomerCount--;
        }

        public override void UpdateCompletion()
        {
            if (_totalCustomerCount != 0) return;
            onStageCompleted?.Invoke();
        }

        public override void ResetCompletion()
        {
            _totalCustomerCount = stageData.customerLimit;
        }

        public override void OnPause()
        {
        }

        public override void OnResume()
        {
        }

        public override string GetCompletionText()
        {
            if (_totalCustomerCount == 0) return closedShopText;
            return $"Customers Remaining: {_totalCustomerCount}";
        }

        public override bool IsCompleted()
        {
            return _totalCustomerCount == 0;
        }

        public CustomerCountCompletion(StageData stageData) : base(stageData)
        {
            Init(stageData);
        }
    }
}