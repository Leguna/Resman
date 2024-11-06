using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StageSystem.Objective
{
    public class ObjectiveManager : MonoBehaviour
    {
        private ObjectiveData _objectiveData;

        [SerializeField] private Image objectiveImage;
        [SerializeField] private TMP_Text objectiveText;

        private void Start()
        {
            objectiveText.transform.parent.gameObject.SetActive(false);
        }

        public void Init(ObjectiveData objectiveData)
        {
            _objectiveData = objectiveData;
            _objectiveData.ResetObjective();
            UpdateObjectiveUI();
            objectiveText.transform.parent.gameObject.SetActive(true);
        }

        public void ResetObjective()
        {
            _objectiveData.ResetObjective();
            UpdateObjectiveUI();
        }

        public void SetListener(Action onAchieved)
        {
            _objectiveData.SetListener(onAchieved);
        }

        public void UpdateObjective(float amount)
        {
            _objectiveData.UpdateObjective(amount);
            UpdateObjectiveUI();
        }

        public bool IsObjectiveAchieved() => _objectiveData.IsAchieved;

        private void UpdateObjectiveUI()
        {
            objectiveText.text = _objectiveData.currentAmount + "/" + _objectiveData.targetAmount;
            objectiveImage.fillAmount = _objectiveData.GetProgress();
        }
    }
}