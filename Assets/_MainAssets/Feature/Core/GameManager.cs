using Currency;
using CustomerSystem;
using StageSystem;
using StageSystem.Objective;
using Touch;
using UnityEngine;
using UpgradeSystem;
using Utilities;
using Utilities.SaveLoad;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private Stage _stage;
    private CustomerSpawner _customerSpawner;
    private TouchInput _touchInput;
    public Gold gold;

    public StageLoader stageLoader;
    public GameOver gameOver;
    public ObjectiveManager objectiveManager;
    public UpgradeManager upgradeManager;

    public static float gameSpeed = 1;

    public static float GameDeltaTime => Time.deltaTime * gameSpeed;

    protected override void Awake()
    {
        base.Awake();
        SaveLoadSystem.Load(out gold);
        _touchInput = FindObjectOfType<TouchInput>();
        _customerSpawner = FindObjectOfType<CustomerSpawner>();
        stageLoader = FindObjectOfType<StageLoader>();
        _stage = FindObjectOfType<Stage>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();

        stageLoader.ToggleStagePanel(true);
        _stage.onPaymentReceived = OnPaymentReceived;

        _touchInput.enabled = false;
        stageLoader.onStageSelected = OnStageLoad;
        stageLoader.onPause = OnPause;
        stageLoader.onResume = OnResume;
        gameOver.onRestart = OnRestart;
        gameOver.onOpenMenu = OnOpenMenu;
        upgradeManager.ToggleUpgradePanel(true);
    }

    private void OnPaymentReceived(Gold payment)
    {
        if (!objectiveManager.IsCompleted()) return;
        gold.Add(payment.value);
    }

    private void OnResume()
    {
        gameSpeed = 1;
        _stage.Resume();
        _touchInput.enabled = true;
        _customerSpawner.Resume();
        upgradeManager.ToggleUpgradePanel(false);
    }

    private void OnPause()
    {
        gameSpeed = 0;
        _touchInput.enabled = false;
        _stage.Pause();
        _customerSpawner.Pause();
        upgradeManager.ToggleUpgradePanel(false);
    }

    private void OnStageLoad(StageData stageData)
    {
        _stage.Init(stageData);
        _stage.onStageStateChanged = StageOnStageStateChanged;
        _stage.Play();
        _touchInput.enabled = true;
        upgradeManager.ToggleUpgradePanel(false);
    }

    private void StageOnStageStateChanged(StageState state)
    {
        if (state != StageState.Ended) return;
        OnGameOver();
        SaveLoadSystem.Save(gold);
    }

    private void OnRestart()
    {
        stageLoader.Enable();
        _customerSpawner.Restart();
        _stage.Restart();
        gameOver.Close();
        gameSpeed = 1;
        _touchInput.enabled = true;
    }

    private void OnGameOver()
    {
        SaveLoadSystem.Save(gold);
        var result = objectiveManager.GetObjectiveResult();
        gameOver.SetTextFinish(result.title, result.content);
        gameOver.Open();
        _customerSpawner.Reset();
        _stage.Reset();
        _touchInput.enabled = false;
        gameSpeed = 0;
        stageLoader.Disable();
        upgradeManager.ToggleUpgradePanel(false);
    }

    private void OnOpenMenu()
    {
        gameOver.Close();
        stageLoader.ToggleStagePanel(true);
        stageLoader.Disable();
        _customerSpawner.Reset();
        _stage.Init(null);
        _touchInput.enabled = false;
        gameSpeed = 0;
        upgradeManager.ToggleUpgradePanel(true);
    }
}