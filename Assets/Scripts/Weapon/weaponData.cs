using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Item/Weapon Data")]
public class WeaponData : ScriptableObject
{
    //플레이어의 속성과 무기 속성이 달라서 합산해서 진행 할꺼라면 따로 만들어야함.
    [Header("기본 속성")]
    public float Attack;
    public float Critical;
    public int NeedGold;
    

    [Header("무기 속성")] 
    public string WeaponName;
    public Sprite WeaponIcon;
    //내구도
    public float Durability;
    //자동공격지속시간
    public float AutoAttackDuration;
    //내구도 회복
    public float DurabilityRecovery;
    //강화
    public int Upgrade;


    [System.Serializable]
    public class UpgradeData
    {
        public int UpgradeLevel;
        public float Attack;
        public float Critical;
    }

    public List<UpgradeData> UpgradeStats;
}
