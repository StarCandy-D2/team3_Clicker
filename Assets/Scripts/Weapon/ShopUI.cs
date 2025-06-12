using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("선택창")] 
    [SerializeField] private GameObject[] uiPanels;
    private int currentIndex = 0;

    [Header("스텟창")] 
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text criticalText;
    [SerializeField] private TMP_Text durabilityText;

    [SerializeField] private WeaponData[] weaponDatas;
    private int weaponDataIndex = 0;
    
    public GameObject equippanel;

    private void Awake()
    {
        UpdateWeaponUI();
    }

    public void OnClickNextButton()
    {
        if (currentIndex >= uiPanels.Length - 1 || weaponDataIndex >= weaponDatas.Length - 1) return;
        //현재 UI 비활성화
        uiPanels[currentIndex].SetActive(false);
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
    }

    public void UpdateWeaponUI()
    {
        WeaponData currentWeapon = weaponDatas[weaponDataIndex];
        attackText.text = currentWeapon.Attack.ToString();
        criticalText.text = currentWeapon.Critical.ToString();
        durabilityText.text = currentWeapon.Durability.ToString();
    }
}
