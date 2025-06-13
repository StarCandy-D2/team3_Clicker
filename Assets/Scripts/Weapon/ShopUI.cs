using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("무기선택 창")] 
    [SerializeField] private GameObject[] _uiPanels;
    private int _currentIndex = 0;

    [Header("스텟창 & cost")] 
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private TMP_Text _criticalText;
    [SerializeField] private TMP_Text _durabilityText;
    [SerializeField] private TMP_Text _costText;

    
    [Header("WeaponDateList")]
    [SerializeField] private WeaponData[] _weaponDatas;
    private int _weaponDataIndex = 0;
    
    [Header("기타")]
    public GameObject equippanel;
    [SerializeField] private TMP_Text _GoldText;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TMP_Text _sendErrorText;
    [SerializeField] private Button _resetButton;
    
    

    private void Awake()
    {
        UpdateWeaponUI();
        UpdateGoldUI();
    }
    public void UpdateWeaponUI()
    {
        WeaponData currentWeapon = _weaponDatas[_weaponDataIndex];
        _attackText.text = currentWeapon.Attack.ToString();
        _criticalText.text = currentWeapon.Critical.ToString();
        _durabilityText.text = currentWeapon.Durability.ToString();
        _costText.text = currentWeapon.NeedGold.ToString();

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
        _GoldText.text = _playerData.gold.ToString("N0") + "G";
    }

    public void OnClickNextButton()
    {
        if (_currentIndex >= _uiPanels.Length - 1 || _weaponDataIndex >= _weaponDatas.Length - 1) return;
        
        //현재 UI 비활성화
        _uiPanels[_currentIndex].SetActive(false);
        HideEquipMarker();
        //인덱스 증가
        _currentIndex++;
        _weaponDataIndex++;
        //다음 UI활성화
        _uiPanels[_currentIndex].SetActive(true);
        UpdateWeaponUI();
    }
    
    public void OnClickUndoButton()
    {
        if (_currentIndex <= 0 || _weaponDataIndex <= 0) return;
        //현재 UI 비활성화
        _uiPanels[_currentIndex].SetActive(false);
        //인덱스 감소
        _currentIndex--;
        _weaponDataIndex--;
        //표출
        _uiPanels[_currentIndex].SetActive(true);
        UpdateWeaponUI();
    }

    public void EquipButton()
    {
        equippanel.SetActive(true);
        foreach (WeaponData weapon in _weaponDatas)
        {
            weapon.IsEquipped = false;
        }
        _weaponDatas[_weaponDataIndex].IsEquipped = true;
    }

    public void HideEquipMarker()
    {
        equippanel.SetActive(false);
    }

    public void UpgradeButton()
    {
        WeaponData currentWeapon = _weaponDatas[_weaponDataIndex];

        if (_playerData.gold < currentWeapon.NeedGold)
        {
            ShowSendError($"골드가 부족합니다.");
            return;
        }
        
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
            ShowSendError("이미 최대 강화입니다.");
        }
    }

    public void PayGold()
    {
        float upgradeCost = _weaponDatas[_weaponDataIndex].NeedGold;
        float playerGold = _playerData.gold;

        if (playerGold >= upgradeCost)
        {
            Debug.Log("골드 충분이요~ 바로 계산갑니데이.");
        }
        else
        {
            Debug.Log("골드 부족이요");
        }
        
        _playerData.gold -= upgradeCost;
        
        UpdateGoldUI();
    }

    public void ShowSendError(string error)
    {
        _sendErrorText.text = error;
    }
    
    public void ResetUpgradeButton()
    {
        WeaponData currentWeapon = _weaponDatas[_weaponDataIndex];

        // 업그레이드 수치를 0으로 초기화
        currentWeapon.Upgrade = 0;

        // 초기 능력치 설정 (UpgradeStats의 첫 번째 항목으로 되돌리기)
        if (currentWeapon.UpgradeStats.Count > 0)
        {
            WeaponData.UpgradeData baseStat = currentWeapon.UpgradeStats[0];
            currentWeapon.Attack = baseStat.Attack;
            currentWeapon.Critical = baseStat.Critical;
            currentWeapon.Durability = baseStat.Durability;
            currentWeapon.NeedGold = baseStat.cost;
        }

        // UI 갱신
        UpdateWeaponUI();
    }
}
