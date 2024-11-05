using StageSystem;
using UnityEngine;
using Utilities;

namespace Core
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public Stage stageManager;
        public StageData stageData;

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
                stageManager.Play();
            }
        }
    }
}