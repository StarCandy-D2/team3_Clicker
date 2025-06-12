using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weapondata;
    [SerializeField] private int currentUpgradeLevel = 0;
    public float CurrentDurability { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeWeapon();
    }

    private void InitializeWeapon()
    {
        if (weapondata != null)
        {
            CurrentDurability = weapondata.Durability;
        }
    }

    public void UseWeapon()
    {
        if (CurrentDurability > 0)
        {
            CurrentDurability -= 1f;
            Debug.Log($"{weapondata.WeaponName} 사용이요");
        }
    }

    public void RepairWeapon()
    {
        CurrentDurability += weapondata.DurabilityRecovery;
        if (CurrentDurability > weapondata.Durability)
        {
            CurrentDurability = weapondata.Durability;
        }
        Debug.Log($"{weapondata.WeaponName} 수리완료요 내구도 {CurrentDurability}");
    }

    public float GetAttack()
    {
        //강화 단계가 리스트 범위 내일때
        if (currentUpgradeLevel >= 0 && currentUpgradeLevel < weapondata.UpgradeStats.Count)
        {
            return weapondata.UpgradeStats[currentUpgradeLevel].Attack;
        }
        //아니면 기본공격
        return weapondata.Attack;
    }

    public float GetCritical()
    {
        //강화 단계가 리스트 범위 내일때
        if (currentUpgradeLevel >= 0 && currentUpgradeLevel < weapondata.UpgradeStats.Count)
        {
            return weapondata.UpgradeStats[currentUpgradeLevel].Critical;
        }
        //아니면 기본 치명타
        return weapondata.Critical;
    }

    public float GetAutoAttackDuration()
    {
        return weapondata.AutoAttackDuration;
    }
}
