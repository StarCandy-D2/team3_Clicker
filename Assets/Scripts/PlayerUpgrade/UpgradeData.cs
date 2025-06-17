using System.Collections;
using System.Collections.Generic;
using PlayerUpgrade;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class UpgradeData
{
    public StatType statType;
    public int level;
    
    public float baseStatValue;
    public float valueIncrease;

    public float baseCost;
    public float costIncrease;

    
    public float GetCurStatValue() => baseStatValue + level * valueIncrease;
    public float GetUpgradeCost() => baseCost + level * costIncrease;
}
