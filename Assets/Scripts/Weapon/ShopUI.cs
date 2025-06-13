using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("무기선택 창")] 
    [SerializeField] private GameObject[] uiPanels;
    private int currentIndex = 0;

    [Header("스텟창 & cost")] 
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text criticalText;
    [SerializeField] private TMP_Text durabilityText;
    [SerializeField] private TMP_Text costText;

    
    [Header("WeaponDateList")]
    [SerializeField] private WeaponData[] weaponDatas;
    private int weaponDataIndex = 0;
    
    [Header("기타")]
    public GameObject equippanel;
    [SerializeField] private TMP_Text GoldText;
    [SerializeField] private PlayerData playerData;
    

    private void Awake()
    {
        UpdateWeaponUI();
        UpdateGoldUI();
    }
    public void UpdateWeaponUI()
    {
        WeaponData currentWeapon = weaponDatas[weaponDataIndex];
        attackText.text = currentWeapon.Attack.ToString();
        criticalText.text = currentWeapon.Critical.ToString();
        durabilityText.text = currentWeapon.Durability.ToString();
        costText.text = currentWeapon.NeedGold.ToString();

        if (currentWeapon.IsEquipped)
        {
            equippanel.SetActive(true);
        }
        else
        {
            equippanel.SetActive(false);
        }
    }

    public void UpdateGoldUI()
    {
        GoldText.text = playerData.gold.ToString("N0") + "G";
    }

    public void OnClickNextButton()
    {
        if (currentIndex >= uiPanels.Length - 1 || weaponDataIndex >= weaponDatas.Length - 1) return;
        
        //현재 UI 비활성화
        uiPanels[currentIndex].SetActive(false);
        HideEquipMarker();
        //인덱스 증가
        currentIndex++;
        weaponDataIndex++;
        //다음 UI활성화
        uiPanels[currentIndex].SetActive(true);
        UpdateWeaponUI();
    }
    
    public void OnClickUndoButton()
    {
        if (currentIndex <= 0 || weaponDataIndex <= 0) return;
        //현재 UI 비활성화
        uiPanels[currentIndex].SetActive(false);
        //인덱스 감소
        currentIndex--;
        weaponDataIndex--;
        //표출
        uiPanels[currentIndex].SetActive(true);
        UpdateWeaponUI();
    }

    public void EquipButton()
    {
        equippanel.SetActive(true);
        foreach (WeaponData weapon in weaponDatas)
        {
            weapon.IsEquipped = false;
        }
        weaponDatas[weaponDataIndex].IsEquipped = true;
    }

    public void HideEquipMarker()
    {
        equippanel.SetActive(false);
    }

    public void UpgradeButton()
    {
        WeaponData currentWeapon = weaponDatas[weaponDataIndex];

        if (currentWeapon.Upgrade < currentWeapon.UpgradeStats.Count)
        {
            WeaponData.UpgradeData stat = currentWeapon.UpgradeStats[currentWeapon.Upgrade];

            currentWeapon.Attack = stat.Attack;
            currentWeapon.Critical = stat.Critical;
            currentWeapon.Durability = stat.Durability;
            currentWeapon.NeedGold = stat.cost;
            
            currentWeapon.Upgrade++;
            
            PayGold();
            UpdateWeaponUI();
        }
        else
        {
            
            Debug.Log("이미 최대 강화입니다.");
        }
    }

    public void PayGold()
    {
        float upgradeCost = weaponDatas[weaponDataIndex].NeedGold;
        float playerGold = playerData.gold;

        if (playerGold >= upgradeCost)
        {
            Debug.Log("골드 충분이요~ 바로 계산갑니데이.");
        }
        else
        {
            Debug.Log("골드 부족이요");
        }
        
        playerData.gold -= upgradeCost;
        
        UpdateGoldUI();
    }

}
