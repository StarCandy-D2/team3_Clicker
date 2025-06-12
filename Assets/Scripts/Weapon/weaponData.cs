using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Inventory/Weapon Data")]
public class WeaponData : ScriptableObject
{
    //플레이어의 속성과 무기 속성이 달라서 합산해서 진행 할꺼라면 따로 만들어야함.
    
    public string WeaponName;
    public Sprite WeaponIcon;

    [Header("무기 속성")] 
    
    //무기의 필요한 속성은 내구도, 공격력, 자동공격지속시간 정도? 
    public float Durability;
    public float AutoAttackDuration;
    public float Attack;
    public float Critical;



}
