using System;
using System.Collections.Generic;
using System.Linq;
using StageSystem;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class StageLoader : DontDestroyThis
{
    private List<StageData> _stages;
    [SerializeField] private RectTransform stagePanel;

    private MainInputAction _mainInputAction;

    public Action<StageData> onStageSelected = delegate { };

    private void OnEnable()
    {
        _mainInputAction = new MainInputAction();
        _mainInputAction.Enable();
        _mainInputAction.UI.Menu.performed += ctx => { ToggleStagePanel(); };
    }

    private void ToggleStagePanel()
    {
        stagePanel.gameObject.SetActive(!stagePanel.gameObject.activeSelf);
    }

    private void OnDisable()
    {
        _mainInputAction.Disable();
    }

    protected override void Awake()
    {
        _stages = LoadStageFromResources().ToList();
    }

    private StageData[] LoadStageFromResources()
    {
        var stageDataList = Resources.LoadAll<StageData>(MyResource.levelResourcePath);
        var stageItem = Resources.Load<Button>(MyResource.stageItemResourcePath);
        foreach (var stageData in stageDataList)
        {
            var stage = Instantiate(stageItem, stagePanel);
            stage.onClick.AddListener(() =>
            {
                onStageSelected?.Invoke(stageData);
                GameManager.gameSpeed = 1.2f;
            });
        }

        return stageDataList;
    }
}