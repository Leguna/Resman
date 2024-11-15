using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StageSystem
{
    public class StageLoader : MonoBehaviour
    {
        [SerializeField] private GameObject stagePanel;
        [SerializeField] private Button bg;
        [SerializeField] private Button pauseButton;

        private MainInputAction _mainInputAction;

        public Action<StageData> onStageSelected = delegate { };
        public Action onPause;
        public Action onResume;


        protected void Awake()
        {
            LoadStageFromResources();
            _mainInputAction = new MainInputAction();
            pauseButton.onClick.AddListener(() => { ToggleStagePanel(); });
        }

        private void Start()
        {
            _mainInputAction.Disable();
        }

        public void ToggleStagePanel(bool? setActive = null)
        {
            var isActivated = setActive ?? !stagePanel.activeSelf;
            stagePanel.SetActive(isActivated);
            bg.gameObject.SetActive(isActivated);
            pauseButton.gameObject.SetActive(!isActivated);

            GameManager.gameSpeed = isActivated ? 0 : 1;
            if (isActivated) onPause?.Invoke();
            else onResume?.Invoke();
        }

        private void LoadStageFromResources()
        {
            var stageDataList = Resources.LoadAll<StageData>(MyResource.LevelResourcePath);
            var stageItem = Resources.Load<Button>(MyResource.StageItemResourcePath);
            foreach (var stageData in stageDataList)
            {
                var stage = Instantiate(stageItem, stagePanel.transform);
                stage.GetComponentInChildren<TMP_Text>().text = stageData.name;

                stage.onClick.RemoveAllListeners();
                stage.onClick.AddListener(() => LoadStage(stageData));
            }
        }

        private void LoadStage(StageData stageData)
        {
            bg.onClick.RemoveAllListeners();
            bg.onClick.AddListener(() => ToggleStagePanel(false));
            _mainInputAction.Enable();
            onStageSelected?.Invoke(stageData);
            GameManager.gameSpeed = 1f;
            bg.gameObject.SetActive(false);
            stagePanel.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _mainInputAction.Enable();
            _mainInputAction.UI.Menu.performed += _ => { ToggleStagePanel(); };
        }

        private void OnDisable()
        {
            _mainInputAction?.Disable();
        }

        public void Disable()
        {
            _mainInputAction?.Disable();
            bg.onClick.RemoveAllListeners();
            pauseButton.gameObject.SetActive(false);
        }

        public void Enable()
        {
            _mainInputAction?.Enable();
            bg.onClick.AddListener(() => ToggleStagePanel(false));
            pauseButton.gameObject.SetActive(true);
        }
    }
}