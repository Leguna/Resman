using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace StageSystem
{
    public class StageLoader : DontDestroyThis
    {
        private List<StageData> _stages;
        [SerializeField] private GameObject stagePanel;
        [SerializeField] private GameObject bg;

        private MainInputAction _mainInputAction;

        public Action<StageData> onStageSelected = delegate { };
        public Action onPause;
        public Action onResume;

        protected override void Awake()
        {
            base.Awake();
            _stages = LoadStageFromResources().ToList();
            ToggleStagePanel(true);
        }

        public void ToggleStagePanel(bool? setActive = null)
        {
            var isActivated = setActive ?? !stagePanel.activeSelf;
            stagePanel.SetActive(isActivated);
            bg.SetActive(isActivated);

            GameManager.gameSpeed = isActivated ? 0 : 1;
            if (isActivated) onPause?.Invoke();
            else onResume?.Invoke();
        }

        private StageData[] LoadStageFromResources()
        {
            var stageDataList = Resources.LoadAll<StageData>(MyResource.levelResourcePath);
            var stageItem = Resources.Load<Button>(MyResource.stageItemResourcePath);
            foreach (var stageData in stageDataList)
            {
                var stage = Instantiate(stageItem, stagePanel.transform);
                stage.GetComponentInChildren<TMP_Text>().text = stageData.name;
                stage.onClick.AddListener(() =>
                {
                    onStageSelected?.Invoke(stageData);
                    GameManager.gameSpeed = 1f;
                    bg.SetActive(false);
                    stagePanel.gameObject.SetActive(false);
                });
            }

            return stageDataList;
        }

        private void OnEnable()
        {
            _mainInputAction = new MainInputAction();
            _mainInputAction.Enable();
            _mainInputAction.UI.Menu.performed += _ => { ToggleStagePanel(); };
        }

        private void OnDisable()
        {
            _mainInputAction?.Disable();
        }
    }
}