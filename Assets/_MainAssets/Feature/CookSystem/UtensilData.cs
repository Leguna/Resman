using System;
using UnityEngine;
using UpgradeSystem;

namespace CookSystem
{
    [CreateAssetMenu(fileName = "Utensil", menuName = "Cooking/Utensil")]
    public class UtensilData : MyScriptableObject, IUpgradeable
    {
        public string utensilName;
        public int level = 1;
        public int maxLevel = 5;
        public float baseCookSpeed = 1f;
        public float upgradeMultiplier = 1.2f;

        public Color color = Color.white;

        public float GetCookSpeed() => baseCookSpeed * Mathf.Pow(upgradeMultiplier, level - 1);

        public string Name => utensilName;
        public int Level => level;
        public int MaxLevel => maxLevel;
        public int Cost => level * 100;

        public void Upgrade()
        {
            if (level >= maxLevel) throw new Exception("Max level reached");
            level++;
        }

        public void Downgrade()
        {
            if (level <= 1)  throw new Exception("Min level reached");
            level--;
        }

        protected override void OnValidate()
        {
            TryCatchWrapper(Validation);
            return;

            void Validation()
            {
                base.OnValidate();
                if (string.IsNullOrEmpty(utensilName))
                    throw new ArgumentNullException($"Utensil Name cannot be null or empty");
                if (baseCookSpeed <= 0)
                    throw new ArgumentOutOfRangeException($"Base Cook Speed must be greater than 0");
                if (upgradeMultiplier <= 1)
                    throw new ArgumentOutOfRangeException($"Upgrade Multiplier must be greater than 1");
            }
        }
    }
}