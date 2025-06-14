using System.Collections;
using System.Collections.Generic;
using PlayerUpgrad;
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
    
    void Start()
    {
        UpdateCostTexts();
        UpdateStatTexts();
        Setgold();
    }

    public void UpdateCostTexts()
    {
        oxygencostText.text = GetCost("Oxygen");
        atkcostText.text =GetCost("atk");
        criratecostText.text = GetCost("critRate");
        goldgaincostText.text =GetCost("goldGain");
        Setgold();
        UpdateStatTexts();

    }
    public void UpdateStatTexts()
    {
        oxygenText.text = upgradeManager.playerData.Oxygen.ToString();
        atkText.text = upgradeManager.playerData.atk.ToString();
        crirateText.text = upgradeManager.playerData.critRate.ToString();
        goldgainText.text = upgradeManager.playerData.goldGain.ToString();
    }

    private string GetCost(string statName)
    {
        var upgrade =  upgradeManager.upgradeData.Find(u => u.statName == statName);
        string costText = upgrade.GetUpgradeCost().ToString();
        return costText;
    }

    public void Setgold()
    {
        goldgaintestText.text =  upgradeManager.playerData.goldGain.ToString();
        goldText.text = upgradeManager.playerData.gold.ToString();
    }
}
