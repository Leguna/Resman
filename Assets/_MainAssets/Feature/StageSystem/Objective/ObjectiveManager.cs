using DG.Tweening;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace StageSystem.Objective
{
    public class ObjectiveManager : MonoBehaviour
    {
        private GoalData _goalData;

        [SerializeField] private Image objectiveImage;
        [SerializeField] private TMP_Text objectiveText;

        private void Start()
        {
            EventManager.AddEventListener<AddObjectiveEvent>(OnObjectiveEvent);
        }

        public void Init(GoalData goalData = null)
        {
            if (goalData == null)
            {
                Hide();
                return;
            }

            Show();
            _goalData = goalData;
            _goalData.ResetObjective();
            ResetObjective();
        }

        public void Hide()
        {
            objectiveText.transform.parent.gameObject.SetActive(false);
            objectiveImage.gameObject.SetActive(false);
        }

        private void Show()
        {
            objectiveText.transform.parent.gameObject.SetActive(true);
            objectiveImage.gameObject.SetActive(true);
        }

        private void UpdateObjectiveUI()
        {
            objectiveText.text = _goalData.GetProgressText();
            objectiveImage.DOFillAmount(_goalData.GetProgress(), 0.3f);
        }

        private void ResetObjective()
        {
            objectiveText.transform.parent.gameObject.SetActive(true);
            objectiveImage.gameObject.SetActive(true);
            UpdateObjectiveUI();
        }


        private void OnDisable()
        {
            EventManager.RemoveEventListener<AddObjectiveEvent>(OnObjectiveEvent);
        }

        public void OnObjectiveEvent(AddObjectiveEvent data)
        {
            if (data.GoalType != _goalData.goalType) return;
            switch (data.GoalType)
            {
                case GoalType.ChainComboGoal:
                    if (data.Amount > _goalData.currentAmount) _goalData.SetObjective(data.Amount);
                    break;
                case GoalType.SatisfactionGoal:
                case GoalType.RevenueGoal:
                case GoalType.ServeGoal:
                    _goalData.UpdateObjective(data.Amount);
                    break;
            }

            UpdateObjectiveUI();
        }
    }
}