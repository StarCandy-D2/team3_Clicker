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
    public TextMeshProUGUI gameovergold;
    public TextMeshProUGUI gameoversessiongold;
    public TextMeshProUGUI gameoverstage;
    public TextMeshProUGUI gameoverlayer;

    public GameObject settingPanel;
    public GameObject gameoverPanel;

    public PlayerData playerData;
    public WeaponData weaponData;

    private void Update()
    {
        ShowStat();
        Gameover();
    }



    public void ShowStat()
    {
        string stageName = stageUIManager.stageNames[stageUIManager.currentStage - 1];
        name.text = playerData.userName;
        atk.text = $"공격력 : {(playerData.GetStat(StatType.atk) + weaponData.Attack).ToString()}";
        oxygen.text = $"산소 : {(playerData.GetStat(StatType.CurEnergy)).ToString()}";
        crit.text = $"치명타 : {playerData.GetStat(StatType.critRate).ToString()}";
        gold.text = $"보유 골드 : {playerData.GetStat(StatType.Gold).ToString()}";
        goldgain.text = $"골드 획득량 증가 : {playerData.GetStat(StatType.goldGain).ToString()}";
        sessiongold.text = $"획득 골드 : {stageUIManager.sessionGold.ToString()}";
        durability.text = $"내구도 : {weaponData.CurrentDurability.ToString()}";
        gameovergold.text = $"보유 골드 : {playerData.GetStat(StatType.Gold).ToString()}";
        gameoversessiongold.text = $"획득 골드 : {stageUIManager.sessionGold.ToString()}";
        gameoverstage.text = $"현재 스테이지 : {stageName}";
        gameoverlayer.text = $"현재 위치 = {stageUIManager.currentLayer}m";
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
    public void Gameover()
    {
        if (playerData.GetStat(StatType.CurEnergy) <= 0f)
        {
            gameoverPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void OnClickNext()
    {
        var playerData = GameManager.Instance.playerData;
        playerData.SetStat(StatType.CurEnergy, playerData.GetStat(StatType.MaxEnergy));
        Time.timeScale = 1f;
        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.FadeOutAndLoadScene("UFOScene");
        }
        else
        {
            Debug.LogWarning("FadeManager 인스턴스가 존재하지 않습니다.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("UFOScene"); // 백업
        }
    }
}
