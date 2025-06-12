using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewUserData", menuName = "User/User Data")]
public class UserData : ScriptableObject
{
    public string userName;
    public float Oxygen;
    public float atk;
    public float critRate;
    public int gold;

    //내구도
    public float atkRate;
    public float autoAtktime;
    //내구도 회복
    public float reviveAtkRate;
}
