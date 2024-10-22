using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Image Sprite;
    public TextMeshProUGUI Description;
    private UpgradeData _upgradeData;

    public void SetCard(UpgradeData data, int count)
    {
        _upgradeData = data;

        Title.text = $"{_upgradeData.UpgradeName} {count}";
        Sprite.sprite = _upgradeData.Icon;
        Description.text = _upgradeData.Description;
    }

    public void HandleClick()
    {
        UpgradeDisplay upgradeDisplay = GetComponentInParent<UpgradeDisplay>();

        if (upgradeDisplay != null)
        {
            upgradeDisplay.SelectUpgrade(_upgradeData);
        }
    }
}
