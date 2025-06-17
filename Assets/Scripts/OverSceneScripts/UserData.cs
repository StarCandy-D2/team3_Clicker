using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    public float curEnergy;
    public float atk;
    public float critRate;
    public float gold;
    public float goldGain;
    
    //내구도
    public float atkRate;
    public float autoAtktime;
    //내구도 회복
    public float reviveAtkRate;
    
    public List<UpgradeSaveData> upgradeLevels = new(); 
}