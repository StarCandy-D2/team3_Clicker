using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UserData
{
    public string userName;
    public float Oxygen;
    public float atk;
    public float critRate;
    public float gold;
    public float goldGain;

    //내구도
    public float atkRate;
    public float autoAtktime;
    //내구도 회복
    public float reviveAtkRate;
}