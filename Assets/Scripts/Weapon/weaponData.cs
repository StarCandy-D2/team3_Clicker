using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponData : MonoBehaviour
{
    
    //무기의 필요한 속성은 내구도, 공격력. 치명타 정도? 
    public string Name;
    public int Durability;
    public int Attack;
    public int Critical;

    public weaponData(string name, int durability, int attack, int critical)
    {
        Name = name;
        Durability = durability;
        Attack = attack;
        Critical = critical;
    }
}
