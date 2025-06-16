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
    public float NeedGold;
    public float Level;
    public string WeaponName;
    public Sprite WeaponIcon;
    

    [Header("기타 속성")] 
    //자동공격지속시간
    public float AutoAttackDuration;
    //내구도
    public float CurrentDurability;
    //최대내구도
    public float MaxDurability;
    
    [Header("강화 속성")]
    //강화
    public int Upgrade;
    public List<UpgradeData> UpgradeStats;
    
    [System.Serializable]
    public class UpgradeData
    {
        public int UpgradeLevel;
        public float Attack;
        public float Critical;
        public int cost;
        public float Durability;
    }

    
    [Header("장착")]
    public bool IsEquipped;
    public bool IsUnlocked = false;
}
