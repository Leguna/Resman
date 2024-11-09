using StageSystem.Objective;

namespace Events
{
    public struct AddObjectiveEvent
    {
        public float Amount { get; set; }
        public GoalType GoalType { get; set; }
    }
}