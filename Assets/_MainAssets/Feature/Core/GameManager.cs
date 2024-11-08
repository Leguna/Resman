using StageSystem;
using UnityEngine;
using Utilities;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public Stage stage;
    public CustomerQueue customerQueue;
    public StageLoader stageLoader;

    public static float gameSpeed = 1;
    
    public static float GameDeltaTime => Time.deltaTime * gameSpeed;

    protected override void Awake()
    {
        base.Awake();
        stage = Stage.Instance;
        customerQueue = FindObjectOfType<CustomerQueue>();
        stageLoader = FindObjectOfType<StageLoader>();
        stageLoader.onStageSelected = OnStageLoad;
    }

    private void OnStageLoad(StageData stageData)
    {
        stage.Init(stageData);
        stage.onStageStateChanged = state => { print($"Stage State: {state}"); };
        stage.Play();
    }

    private void Start()
    {
    }


    public void OnGUI()
    {
        // if (GUI.Button(new Rect(10, 10, 120, 40), "Load Stage"))
        // {
        //     stageManager.Init(stageData);
        //     stageManager.onStageChanged = state => { print($"Stage State: {state}"); };
        //     stageManager.Play();
        // }
        //
        // // Add Customer
        // if (GUI.Button(new Rect(10, 60, 120, 40), "Add Customer"))
        // {
        //     stageManager.CustomerCounterIncrement();
        // }
        //
        // // Add objective
        // if (GUI.Button(new Rect(10, 110, 120, 40), "Add Objective"))
        // {
        //     stageManager.ObjectiveUpdate(10);
        // }
    }
}