using System;
using System.Collections.Generic;
using CookSystem;
using Currency;
using TMPro;
using UnityEngine;
using Utilities.SaveLoad;

namespace UpgradeSystem
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text infoText;
        [SerializeField] private Transform upgradedItemParent;

        private readonly List<UpgradedItem> _upgradedItems = new();
        private Gold _gold;

        private void Awake()
        {
            UpdateUI();
        }

        private void Start()
        {
            SpawnUtensil();
        }

        private void SpawnUtensil()
        {
            var upgradeItemUI = Resources.Load<UpgradedItem>(MyResource.UpgradedItemPrefab);
            var upgradedItems = Resources.LoadAll<UtensilData>(MyResource.Utensil);
            foreach (var upgradedItem in upgradedItems)
            {
                var item = Instantiate(upgradeItemUI, upgradedItemParent);
                item.SetData(upgradedItem);
                item.SetListeners(() => Upgrade(upgradedItem, item),
                    () => Downgrade(upgradedItem, item));
                _upgradedItems.Add(item);
            }
        }

        private void Downgrade(UtensilData upgradedItem, UpgradedItem upgradeItemUI)
        {
            try
            {
                if (upgradedItem.level == 1)
                    throw new Exception("Cannot downgrade below level 1");
                upgradedItem.Downgrade();
                _gold.Add(upgradedItem.Cost);
                SaveLoadSystem.Save(_gold);
                coinText.text = $"Gold: {_gold.value}";
                infoText.text = $"Downgraded {upgradedItem.utensilName} to level {upgradedItem.level}";
                upgradeItemUI.SetData(upgradedItem);
            }
            catch (Exception e)
            {
                infoText.text = e.Message;
            }
        }

        private void Upgrade(UtensilData upgradedItem, UpgradedItem upgradeItemUI)
        {
            try
            {
                if (_gold.value < upgradedItem.Cost)
                    throw new Exception("Not enough gold");
                _gold.Subtract(upgradedItem.Cost);
                SaveLoadSystem.Save(_gold);
                upgradedItem.Upgrade();
                infoText.text = $"Upgraded {upgradedItem.utensilName} to level {upgradedItem.level}";
                upgradeItemUI.SetData(upgradedItem);
                UpdateUI();
            }
            catch (Exception e)
            {
                infoText.text = e.Message;
            }
        }

        public void ToggleUpgradePanel(bool? setActive = null)
        {
            var isActivated = setActive ?? !upgradePanel.activeSelf;
            upgradePanel.SetActive(isActivated);
        }

        private void UpdateUI()
        {
            UpdateGold();
            foreach (var upgradedItem in _upgradedItems)
            {
                upgradedItem.UpdateUI();
            }
        }

        private void UpdateGold()
        {
            SaveLoadSystem.Load(out _gold);
            coinText.text = $"Gold: {_gold.value}";
        }
    }
}