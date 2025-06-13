using System.Collections;
using System.Collections.Generic;
using PlayerUpgrad;
using TMPro;
using UnityEngine;

public class PlayerUpgradeUIManager : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    
    public TMP_Text oxygencostText;
    public TMP_Text atkcostText;
    public TMP_Text criratecostText;
    public TMP_Text goldgaincostText;
    public TMP_Text goldgainText;
    public TMP_Text goldText;
    
    void Start()
    {
        UpdateAllTexts();
    }

    public void UpdateAllTexts()
    {
        oxygencostText.text = GetCost("Oxygen");
        atkcostText.text =GetCost("atk");
        criratecostText.text = GetCost("critRate");
        goldgaincostText.text =GetCost("goldGain");
        Setgold();
        
    }

    private string GetCost(string statName)
    {
        var upgrade =  upgradeManager.upgradeData.Find(u => u.statName == statName);
        string costText = upgrade.GetUpgradeCost().ToString();
        return costText;
    }

    public void Setgold()
    {
        goldgainText.text =  upgradeManager.playerData.goldGain.ToString();
        goldText.text = upgradeManager.playerData.gold.ToString();
    }
}
