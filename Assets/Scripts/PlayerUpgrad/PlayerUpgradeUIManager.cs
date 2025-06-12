using System.Collections;
using System.Collections.Generic;
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
        oxygencostText.text = $"Cost: {GetCost("Oxygen")}";
        atkcostText.text = $"Cost: {GetCost("atk")}";
        criratecostText.text = $"Cost: {GetCost("critRate")}";
        goldgaincostText.text = $"Cost: {GetCost("goldGain")}";
    }

    private string GetCost(string statName)
    {
        var upgrade =  upgradeManager.upgradeData.Find(u => u.statName == statName);
        string costText = upgrade.GetUpgradeCost().ToString();
        return costText;
    }

    public void Setgold(float gold, float goldGain)
    {
        goldgainText.text = goldGain.ToString();
        goldText.text = gold.ToString();
    }
}
