using System;

namespace StageSystem.Completion
{
    public abstract class StageCompletion
    {
        public StageData stageData;
        protected StageCompletion(StageData stageData) => this.stageData = stageData;

        public Action onStageCompleted = delegate { };
        public abstract void Init(StageData stageData);
        public abstract void UpdateCompletion();
        public abstract void ResetCompletion();

        public abstract void OnPause();
        public abstract void OnResume();

        public abstract string GetCompletionText();
        
        public string closedShopText = "Shop Closed";

        public abstract bool IsCompleted();
    }
}