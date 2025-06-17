using PlayerUpgrade;
using TMPro;
using UnityEngine;

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

    public GameObject settingPanel;

    public PlayerData playerData;
    public WeaponData weaponData;

    private void Update()
    {
        ShowStat();
    }



    public void ShowStat()
    {
        name.text = playerData.userName;
        atk.text = $"공격력 : {(playerData.GetStat(StatType.atk) + weaponData.Attack).ToString()}";
        oxygen.text = $"산소 : {(playerData.GetStat(StatType.Oxygen)).ToString()}";
        crit.text = $"치명타 : {playerData.GetStat(StatType.critRate).ToString()}";
        gold.text = $"보유 골드 : {playerData.GetStat(StatType.Gold).ToString()}";
        goldgain.text = $"골드 획득량 증가 : {playerData.GetStat(StatType.goldGain).ToString()}";
        sessiongold.text = $"획득 골드 : {stageUIManager.sessionGold.ToString()}";
        durability.text = $"내구도 : {weaponData.CurrentDurability.ToString()}";
    }

    public void Showsetting()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void hidesetting()
    {
        settingPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
