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
    private UpgradeData upgradeData;

    public void SetCard(UpgradeData data, int count)
    {
        upgradeData = data;

        Title.text = $"{upgradeData.UpgradeName} {count}";
        Sprite.sprite = upgradeData.Icon;
        Description.text = upgradeData.Description;
    }

    public void HandleClick()
    {
        UpgradeDisplay upgradeDisplay = GetComponentInParent<UpgradeDisplay>();

        if (upgradeDisplay != null)
        {
            upgradeDisplay.SelectUpgrade(upgradeData);
        }
    }
}
