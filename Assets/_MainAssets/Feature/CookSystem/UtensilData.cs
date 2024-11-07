using System;
using UnityEngine;

namespace CookSystem
{
    [CreateAssetMenu(fileName = "Utensil", menuName = "Cooking/Utensil")]
    public class UtensilData : MyScriptableObject
    {
        public string utensilName;
        public int level = 1;
        public float baseCookSpeed = 1f;
        public float upgradeMultiplier = 1.2f;

        public Color color = Color.white;

        public float GetCookSpeed() => baseCookSpeed * Mathf.Pow(upgradeMultiplier, level - 1);

        public void Upgrade() => level++;

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