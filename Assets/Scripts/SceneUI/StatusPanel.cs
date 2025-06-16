using System.Collections;
using System.Collections.Generic;
using PlayerUpgrade;
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
    public TextMeshProUGUI durability;


    public PlayerData playerData;
    public WeaponData weaponData;

    private void Start()
    {
        ShowStat();
    }


    public void ShowStat()
    {
        name.text = playerData.userName;
        atk.text = $"공격력 : {(playerData.GetStat(StatType.atk) + weaponData.Attack).ToString()}";
        oxygen.text = $"산소 : {(playerData.GetStat(StatType.Oxygen)).ToString()}";
        crit.text = $"치명타 : {playerData.GetStat(StatType.critRate).ToString()}";
    }
}
