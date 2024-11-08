using CustomerSystem;
using StageSystem;
using Touch;
using UnityEngine;
using Utilities;

public class GameManager : DontDestroyThis
{
    private Stage _stage;
    private CustomerSpawner _customerSpawner;
    private TouchInput _touchInput;

    public StageLoader stageLoader;
    public GameOver gameOver;

    public static float gameSpeed = 1;

    public static float GameDeltaTime => Time.deltaTime * gameSpeed;

    protected override void Awake()
    {
        base.Awake();
        _touchInput = FindObjectOfType<TouchInput>();
        _customerSpawner = FindObjectOfType<CustomerSpawner>();
        stageLoader = FindObjectOfType<StageLoader>();
        _stage = FindObjectOfType<Stage>();

        stageLoader.ToggleStagePanel(true);

        _touchInput.enabled = false;
        stageLoader.onStageSelected = OnStageLoad;
        stageLoader.onPause = OnPause;
        stageLoader.onResume = OnResume;
        gameOver.onRestart = OnRestart;
        gameOver.onOpenMenu = OnOpenMenu;
    }

    private void OnResume()
    {
        gameSpeed = 1;
        _stage.Resume();
        _touchInput.enabled = true;
        _customerSpawner.Resume();
    }

    private void OnPause()
    {
        gameSpeed = 0;
        _touchInput.enabled = false;
        _stage.Pause();
        _customerSpawner.Pause();
    }

    private void OnStageLoad(StageData stageData)
    {
        _stage.Init(stageData);
        _stage.onStageStateChanged = StageOnStageStateChanged;
        _stage.Play();
        _touchInput.enabled = true;
    }

    private void StageOnStageStateChanged(StageState state)
    {
        if (state != StageState.Ended) return;
        OnGameOver();
    }

    private void OnRestart()
    {
        _customerSpawner.Restart();
        _stage.Restart();
        gameOver.Close();
        gameSpeed = 1;
        _touchInput.enabled = true;
    }

    private void OnGameOver()
    {
        gameOver.Open();
        _customerSpawner.Reset();
        stageLoader.enabled = false;
        _stage.Reset();
        _touchInput.enabled = false;
        gameSpeed = 0;
    }

    private void OnOpenMenu()
    {
        gameOver.Close();
        stageLoader.ToggleStagePanel(true);
        _customerSpawner.Reset();
        _stage.Init(null);
        _touchInput.enabled = false;
        gameSpeed = 0;
    }
}