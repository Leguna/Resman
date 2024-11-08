using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StageSystem.Objective
{
    public class ObjectiveManager : MonoBehaviour
    {
        private Goal _goalData;

        [SerializeField] private Image objectiveImage;
        [SerializeField] private TMP_Text objectiveText;

        private Action _onObjectiveComplete = delegate { };

        private void Awake()
        {
            objectiveText.transform.parent.gameObject.SetActive(false);
        }

        public void Init(Goal goalData, Action onObjectiveComplete = null)
        {
            _goalData = goalData;
            objectiveText.transform.parent.gameObject.SetActive(false);
            _onObjectiveComplete = onObjectiveComplete;
            UpdateObjectiveUI();
        }

        private void UpdateObjectiveUI()
        {
            objectiveImage.DOFillAmount(_goalData.goalData.currentAmount, 0.5f);
        }
    }
}