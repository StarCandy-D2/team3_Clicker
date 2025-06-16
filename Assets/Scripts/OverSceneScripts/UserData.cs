using PlayerUpgrade;
using System;
using System.Collections.Generic;

[System.Serializable]
public class UpgradeSaveData
{
    public string statName;
    public int level;
}
[System.Serializable]
public class UserData
{
    public string userName;
    public float Oxygen;
    public float atk;
    public float critRate;
    public float gold;
    public float goldGain;

    public List<UpgradeSaveData> upgradeLevels = new();  // ← 추가
}
