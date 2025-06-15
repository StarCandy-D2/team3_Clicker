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
    [SerializeField] private GameObject[] _closePanels;
    [SerializeField] private GameObject[] _openPanels;

    [Header("스텟창 & cost")] 
    [SerializeField] private TMP_Text[] _attackText;
    [SerializeField] private TMP_Text[] _criticalText;
    [SerializeField] private TMP_Text[] _durabilityText;
    [SerializeField] private TMP_Text[] _costText;
    [SerializeField] private TMP_Text[] _clostCostText;
    [SerializeField] private TMP_Text[] _levelText;

    
    [Header("WeaponDateList")]
    [SerializeField] private WeaponData[] _weaponDatas;
    private int _weaponDataIndex = 0;
    
    [Header("기타")]
    public GameObject equippanel;
    [SerializeField] private TMP_Text _GoldText;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TMP_Text _sendErrorText;
    [SerializeField] private WeaponData[] _closeItemData;
    

    private void Awake()
    {
        UpdateWeaponUI();
        UpdateGoldUI();

        for (int i = 0; i < _closeItemData.Length && i<_clostCostText.Length; i++)
        {
            _clostCostText[i].text = _closeItemData[i].NeedGold.ToString("N0") + "G";
        }
    }
    public void UpdateWeaponUI()
    {
        //현재 인덱스 번호.
        int current = _currentIndex;
        //현재 강화단계
        int level = _weaponDatas[current].Upgrade;

        //아직 최대가 아니면 다음 강화 스텟 미리 보여주기.
        if (level < _weaponDatas[current].UpgradeStats.Count)
        {
            //Upgrade가 0이면 UpgradeStat[0]을 가져온다. 결국 1강할 때 스텟이 바큄.
            WeaponData.UpgradeData preview = _weaponDatas[current].UpgradeStats[level];
            _attackText[current].text = preview.Attack.ToString();
            _criticalText[current].text = preview.Critical.ToString();
            _durabilityText[current].text = preview.Durability.ToString();
            _costText[current].text = $"{preview.cost.ToString()}G";
            _levelText[current].text = $"Lv.{preview.UpgradeLevel.ToString()}";
        }
        else
        {
            _attackText[current].text = _weaponDatas[current].Attack.ToString();
            _criticalText[current].text = _weaponDatas[current].Critical.ToString();
            _durabilityText[current].text = _weaponDatas[current].Durability.ToString();
            _costText[current].text = "MAX";
            _levelText[current].text = "MAX";
        }

        equippanel.SetActive(_weaponDatas[current].IsEquipped);
       
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
        ShowSendError("장착을 완료했습니다.", Color.green);
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
            ShowSendError($"골드 부족해요 땅 더 파고 오시죠?",Color.red);
            return;
        }
        
        if (currentWeapon.Upgrade < currentWeapon.UpgradeStats.Count)
        {
            WeaponData.UpgradeData stat = currentWeapon.UpgradeStats[currentWeapon.Upgrade];

            currentWeapon.Attack = stat.Attack;
            currentWeapon.Critical = stat.Critical;
            currentWeapon.Durability = stat.Durability;
            currentWeapon.NeedGold = stat.cost;
            currentWeapon.Level = stat.UpgradeLevel;
            
            currentWeapon.Upgrade++;
            
            ShowSendError("업그레이드요~", Color.green);
            
            PayGold();
            UpdateWeaponUI();
        }
        else
        {
            ShowSendError("최대 강화, 무슨 욕심을 부리시나요 ><",Color.yellow);
        }
    }

    public void PayGold()
    {
        float upgradeCost = _weaponDatas[_weaponDataIndex].NeedGold;
        float playerGold = _playerData.gold;
    
        if (playerGold >= upgradeCost)
        {
            Debug.Log("골드 충분이요~ 바로 계산갑니데이.");
            _playerData.gold -= upgradeCost;
        }
        else
        {
            Debug.Log("골드 부족이요");
        }
        
        
        UpdateGoldUI();
    }

    public void ShowSendError(string error, Color color)
    {
        _sendErrorText.text = error;
        _sendErrorText.color = color;
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
            currentWeapon.Level = baseStat.UpgradeLevel;
        }

        // UI 갱신
        UpdateWeaponUI();
    }

    public void OnClickBuyButton()
    {
        if (_playerData.gold >= _closeItemData[_currentIndex-1].NeedGold)
        {
            //골드 차감
            _playerData.gold -= _closeItemData[_currentIndex-1].NeedGold;
            
            //구매처리
            _weaponDatas[_currentIndex-1].IsUnlocked = true;
            
            _closePanels[_currentIndex-1].SetActive(false);
            _openPanels[_currentIndex-1].SetActive(true);
            
            ShowSendError("새로운 삽 등장!", Color.white);
            
            UpdateGoldUI();
        }
        else
        {
            ShowSendError("골드 부족해요 땅 더 파고 오시죠?",Color.red);
        }
    }
}
