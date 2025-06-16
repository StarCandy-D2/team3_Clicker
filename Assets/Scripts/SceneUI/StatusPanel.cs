using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI oxygen;
    public TextMeshProUGUI crit;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI goldgain;
    public TextMeshProUGUI sessiongold;
    public TextMeshProUGUI durability;
    public StageUIManager stageUIManager;


    public PlayerData playerData;
    public WeaponData weaponData;

    private void Update()
    {
        ShowStat();
    }



    public void ShowStat()
    {
        name.text = playerData.userName;
        atk.text = $"공격력 : {(playerData.atk + weaponData.Attack).ToString()}";
        oxygen.text = $"산소 : {(playerData.Oxygen).ToString()}";
        crit.text = $"치명타 : {playerData.critRate.ToString()}";
        gold.text = $"보유 골드 : {playerData.gold.ToString()}";
        goldgain.text = $"골드 획득량 증가 : {playerData.goldGain.ToString()}";
        sessiongold.text = $"획득 골드 : {stageUIManager.sessionGold.ToString()}";
        durability.text = $"내구도 : {weaponData.CurrentDurability.ToString()}";
    }
}
