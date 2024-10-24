using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string UpgradeName;
    public Sprite Icon;
    public string Description;
    public UpgradeType Type;
    public float Value;

    public enum UpgradeType
    {
        PASSIVE,
        WEAPON,
    }
}