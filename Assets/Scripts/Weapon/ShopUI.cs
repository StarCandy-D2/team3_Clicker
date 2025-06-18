using System.Collections;
using PlayerUpgrade;
using UnityEngine;
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
    private int _weaponDisplayIndex = 1;
    
    [Header("기타")]
    public GameObject equippanel;
    [SerializeField] private TMP_Text _GoldText;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TMP_Text _sendErrorText;
    [SerializeField] private WeaponData[] _closeItemData;
    
    [SerializeField] private float errorDisplayDuration = 2.5f; // 표시 시간
    [SerializeField] private float typingSpeed = 0.05f; // 타이핑 속도
    private Coroutine _errorCoroutine;
    [SerializeField] private WeaponData equippedWeaponData;
    
    

    private void Awake()
    {
        UpdateWeaponUI();
        UpdateGoldUI();
        OpenPanelCost();
        
    }
    public void UpdateWeaponUI()
    {
        //현재 인덱스 번호.
        int current = _currentIndex;
        //현재 강화단계
        int level = _weaponDatas[current].Upgrade;

        
        if (level < _weaponDatas[current].UpgradeStats.Count)
        {
            WeaponData weapon = _weaponDatas[current];
            
            _attackText[current].text = $"{weapon.Attack * 100f}% 증가";
            _criticalText[current].text = $"{weapon.Critical * 100f}% 증가";
            _durabilityText[current].text = weapon.CurrentDurability.ToString();
            _costText[current].text = $"{weapon.NeedGold.ToString()}G";
            _levelText[current].text = $"Lv.{weapon.Level.ToString()}";
        }
        else
        {
            _attackText[current].text = _weaponDatas[current].Attack.ToString();
            _criticalText[current].text = _weaponDatas[current].Critical.ToString();
            _durabilityText[current].text = _weaponDatas[current].CurrentDurability.ToString();
            _costText[current].text = "MAX";
            _levelText[current].text = "MAX";
        }

        equippanel.SetActive(_weaponDatas[current].IsEquipped);
       
    }

    public void UpdateGoldUI()
    {
        _GoldText.text = _playerData.GetStat(StatType.Gold).ToString("N0") + "G";

    }

    public void OpenPanelCost()
    {
        for (int i = 0; i < _closeItemData.Length && i<_clostCostText.Length; i++)
        {
            _clostCostText[i].text = _closeItemData[i].NeedGold.ToString("N0") + "G";
        }
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
        WeaponData selectedWeapon = _weaponDatas[_weaponDataIndex];

        //현재 무기가 이미 장착되 상태라면
        if (selectedWeapon.IsEquipped)
        {
            selectedWeapon.IsEquipped = false;
            GameManager.Instance.equippedWeaponIndex = -1;

            //해제 . 스텟 원복
            float baseAtk = _playerData.GetStat(StatType.atk);
            float baseCrit = _playerData.GetStat(StatType.critRate);

            int upgradeLevel = selectedWeapon.Upgrade;

            if (upgradeLevel < selectedWeapon.UpgradeStats.Count)
            {
                WeaponData.UpgradeData stat = selectedWeapon.UpgradeStats[upgradeLevel];
                baseAtk /= (1f + stat.Attack);
                baseCrit /= (1f + stat.Critical);
            }
            else if (selectedWeapon.UpgradeStats.Count > 0)
            {
                WeaponData.UpgradeData maxStat = selectedWeapon.UpgradeStats[selectedWeapon.UpgradeStats.Count - 1];
                baseAtk /= (1f + maxStat.Attack);
                baseCrit /= (1f + maxStat.Critical);
            }
            
            _playerData.SetStat(StatType.atk, baseAtk);
            _playerData.SetStat(StatType.critRate, baseCrit);
            
            equippanel.SetActive(false);
            ShowSendError("장착을 해제하였습니다",Color.green);
        }
        else
        {
            equippanel.SetActive(true);
            
            foreach (WeaponData weapon in _weaponDatas)
            {
                weapon.IsEquipped = false;
            }
            
            selectedWeapon.IsEquipped = true;
            
            GameManager.Instance.equippedWeaponIndex = _weaponDataIndex;
            
            int upgradeLevel = selectedWeapon.Upgrade;
            
            float baseAtk = _playerData.GetStat(StatType.atk);
            float baseCrit = _playerData.GetStat(StatType.critRate);
        
            if (upgradeLevel < selectedWeapon.UpgradeStats.Count)
            {
                WeaponData.UpgradeData upgradedStat = selectedWeapon.UpgradeStats[upgradeLevel];
                
                float finalAtk = baseAtk * (1f + upgradedStat.Attack);
                float finalCrit = baseCrit * (1f + upgradedStat.Critical);
                
                _playerData.SetStat(StatType.atk, finalAtk);
                _playerData.SetStat(StatType.critRate, finalCrit);
            }
            else if (selectedWeapon.UpgradeStats.Count > 0)
            {
                WeaponData.UpgradeData maxStat = selectedWeapon.UpgradeStats[selectedWeapon.UpgradeStats.Count - 1];

                float finalAtk = baseAtk * (1f + maxStat.Attack);
                float finalCrit = baseCrit * (1f + maxStat.Critical);
                
                _playerData.SetStat(StatType.atk, finalAtk);
                _playerData.SetStat(StatType.critRate, finalCrit);
            }
            
            equippedWeaponData.LoadFrom(selectedWeapon);
            ShowSendError("장착을 완료했습니다.",Color.green);
        }
        
        UpdateWeaponUI();
    }

    public void HideEquipMarker()
    {
        equippanel.SetActive(false);
    }

    public void UpgradeButton()
    {
        WeaponData currentWeapon = _weaponDatas[_weaponDataIndex];

        if (_playerData.GetStat(StatType.Gold) < currentWeapon.NeedGold)
        {
            ShowSendError($"골드가 부족합니다",Color.red);
            return;
        }
        
        bool isEquipped = currentWeapon.IsEquipped;
        
        if (isEquipped)
        {
            ShowSendError("장비 해제 후 업그레이드를 해주세요",Color.yellow);
            return;
        }
        
        currentWeapon.Upgrade++;
            
        if (currentWeapon.Upgrade < currentWeapon.UpgradeStats.Count)
        {
            WeaponData.UpgradeData stat = currentWeapon.UpgradeStats[currentWeapon.Upgrade];

            currentWeapon.Attack = stat.Attack;
            currentWeapon.Critical = stat.Critical;
            currentWeapon.CurrentDurability = stat.Durability;
            currentWeapon.NeedGold = stat.cost;
            currentWeapon.Level = stat.UpgradeLevel;
            

            if (currentWeapon.IsEquipped)
            {
                float baseAtk = _playerData.GetStat(StatType.atk);
                float baseCrit = _playerData.GetStat(StatType.critRate);
                
                float finalAtk = baseAtk * (1f + stat.cost);
                float finalCrit = baseCrit * (1f + stat.cost);
                
                
                _playerData.SetStat(StatType.atk, finalAtk);
                _playerData.SetStat(StatType.critRate, finalCrit);
            }
            
            ShowSendError("업그레이드를 완료하였습니다", Color.green);
            PayGold();
            UpdateWeaponUI();
        }
        else
        {
            ShowSendError("최대 강화입니다",Color.yellow);
        }
    }

    public void PayGold()
    {
        float upgradeCost = _weaponDatas[_weaponDataIndex].NeedGold;
        float playerGold = _playerData.GetStat(StatType.Gold);
    
        if (playerGold >= upgradeCost)
        {
            Debug.Log("골드 충분이요~ 바로 계산갑니데이.");
            _playerData.SetStat(StatType.Gold, _playerData.GetStat(StatType.Gold) - upgradeCost);
        }
        else
        {
            Debug.Log("골드 부족이요");
        }
        
        
        UpdateGoldUI();
    }

    public void ShowSendError(string error, Color color)
    {
        if (_errorCoroutine != null)
        {
            StopCoroutine(_errorCoroutine);
        }

        _errorCoroutine = StartCoroutine(TypeErrorMessage(error, color));
        
    }

    IEnumerator TypeErrorMessage(string error, Color color)
    {
        _sendErrorText.text = "";
        _sendErrorText.color = color;

        for (int i = 0; i < error.Length; i++)
        {
            _sendErrorText.text += error[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        
        yield return new WaitForSeconds(errorDisplayDuration);
        _sendErrorText.text = "";
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
            currentWeapon.CurrentDurability = baseStat.Durability;
            currentWeapon.NeedGold = baseStat.cost;
            currentWeapon.Level = baseStat.UpgradeLevel;
        }

        // UI 갱신
        UpdateWeaponUI();
    }

    public void OnClickBuyButton()
    {
        if (_playerData.GetStat(StatType.Gold) >= _closeItemData[_currentIndex-1].NeedGold)
        {
            //골드 차감
            _playerData.SetStat(StatType.Gold, _playerData.GetStat(StatType.Gold)- _closeItemData[_currentIndex-1].NeedGold);
            
            //구매처리
            _weaponDatas[_currentIndex-1].IsUnlocked = true;
            
            _closePanels[_currentIndex-1].SetActive(false);
            _openPanels[_currentIndex-1].SetActive(true);
            
            ShowSendError("새로운 삽이 등장합니다", Color.white);
            
            UpdateGoldUI();
        }
        else
        {
            ShowSendError("골드가 부족합니다",Color.red);
        }
    }
}