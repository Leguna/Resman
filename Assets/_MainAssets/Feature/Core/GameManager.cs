using StageSystem;
using UnityEngine;
using Utilities;

namespace Core
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public Stage stageManager;
        public StageData stageData;
        
        public float timeScale = 1f;

        protected override void Awake()
        {
            base.Awake();
            stageManager = Stage.Instance;
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 120, 40), "Load Stage"))
            {
                stageManager.Init(stageData);
                stageManager.onStageChanged = state => { print($"Stage State: {state}"); };
                stageManager.Play();
            }

            // Add Customer
            if (GUI.Button(new Rect(10, 60, 120, 40), "Add Customer"))
            {
                stageManager.CustomerCounterIncrement();
            }
            
            // Add objective
            if (GUI.Button(new Rect(10, 110, 120, 40), "Add Objective"))
            {
                stageManager.ObjectiveUpdate(10);
            }
        }
    }
}