using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weapondata;
    
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
        return weapondata.Attack;
    }

    public float GetCritical()
    {
        return weapondata.Critical;
    }

    public float GetAutoAttackDuration()
    {
        return weapondata.AutoAttackDuration;
    }
}
