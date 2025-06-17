
using System.Collections.Generic;
using PlayerUpgrade;
using TMPro;
using UnityEngine;

public class PlayerUpgradeUIManager : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    
    [Header("costText")]
    public TMP_Text oxygencostText;
    public TMP_Text atkcostText;
    public TMP_Text criratecostText;
    public TMP_Text goldgaincostText;
    public TMP_Text goldgaintestText;
    public TMP_Text goldText;
    [Header("statText")]
    public TMP_Text oxygenText;
    public TMP_Text atkText;
    public TMP_Text crirateText;
    public TMP_Text goldgainText;
    
    private Dictionary<StatType, TMP_Text> statTextMap;
    private Dictionary<StatType, TMP_Text> costTextMap;
    
    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        Mapping();
        UpdateAllTexts();
    }

    void Mapping()
    {
        var data = upgradeManager.playerData;

        statTextMap = new()
        {
            { StatType.Oxygen, oxygenText },
            { StatType.atk, atkText },
            { StatType.critRate, crirateText },
            { StatType.goldGain, goldgainText },
        };

        costTextMap = new()
        {
            { StatType.Oxygen, oxygencostText },
            { StatType.atk, atkcostText },
            { StatType.critRate, criratecostText },
            { StatType.goldGain, goldgaincostText },
        };

        data.OnStatChanged += OnStatChanged;
    }

    public void OnStatChanged(StatType stat, float value)
    {
        if (stat == StatType.Gold)
        {
            goldText.text = value.ToString();
            UpdateCostColors(value);
        }
        else if (stat == StatType.goldGain)
        {
            goldgaintestText.text = value.ToString();
        }

        if (statTextMap.TryGetValue(stat, out var text))
        {
            text.text = value.ToString();
        }
    }
    //UI 업데이트
    public void UpdateAllTexts() //Gpt 코드
    {
        var data = upgradeManager.playerData;
        
        foreach (var kvp in statTextMap)
        {
            float value = upgradeManager.playerData.GetStat(kvp.Key);
            kvp.Value.text = value.ToString();
        }

        goldText.text = data.GetStat(StatType.Gold).ToString();
        goldgaintestText.text = data.GetStat(StatType.goldGain).ToString();
        UpdateCostColors(data.GetStat(StatType.Gold));
    }

    //gold 부족시 빨간색 표시
    private void UpdateCostColors(float currentGold)
    {
        foreach (var kvp in costTextMap)
        {
            var upgrade = upgradeManager.upgradeData.Find(u => u.statType == kvp.Key);
            if (upgrade != null)
            {
                kvp.Value.text = upgrade.GetUpgradeCost().ToString();
                kvp.Value.color = currentGold < upgrade.GetUpgradeCost() ? Color.red : Color.black;
            }
        }
    }
}
