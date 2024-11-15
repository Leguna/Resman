using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpgradeSystem;

public class UpgradedItem : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button downgradeButton;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text nameText;


    private bool IsMaxLevel => _upgradeableItem.Level >= _upgradeableItem.MaxLevel;
    private IUpgradeable _upgradeableItem;
    private Action _onUpgrade;
    private Action _onDowngrade;


    public void SetListeners(Action onUpgrade, Action onDowngrade)
    {
        _onUpgrade = onUpgrade;
        _onDowngrade = onDowngrade;
    }

    private void Awake()
    {
        upgradeButton.onClick.RemoveAllListeners();
        downgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => _onUpgrade?.Invoke());
        downgradeButton.onClick.AddListener(() => _onDowngrade?.Invoke());
    }

    public void UpdateUI()
    {
        priceText.gameObject.SetActive(!IsMaxLevel);
        priceText.text = IsMaxLevel ? "" : $"{_upgradeableItem.Cost}G";
        nameText.text = _upgradeableItem.Name + " Lv" + _upgradeableItem.Level + (IsMaxLevel ? " (Max)" : "");
        upgradeButton.interactable = !IsMaxLevel;
        downgradeButton.interactable = _upgradeableItem.Level > 1;
    }

    public void SetData(IUpgradeable upgradeable)
    {
        _upgradeableItem = upgradeable;
        UpdateUI();
    }
}