using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StageSystem.Objective
{
    public class ObjectiveManager : MonoBehaviour
    {
        private Goal _goal;

        [SerializeField] private Image objectiveImage;
        [SerializeField] private TMP_Text objectiveText;
        
        private Action _onObjectiveComplete = delegate { };

        public void Init(GoalData goalData, Action onObjectiveComplete)
        {
            return;
            _goal.goalData = goalData;
            _onObjectiveComplete = onObjectiveComplete;
            objectiveText.transform.parent.gameObject.SetActive(true);
            UpdateObjectiveUI();
        }

        private void UpdateObjectiveUI()
        {
            objectiveText.text = _goal.goalData.GetObjectiveDescription();
            objectiveImage.fillAmount = _goal.goalData.GetProgress();
        }
    }
}