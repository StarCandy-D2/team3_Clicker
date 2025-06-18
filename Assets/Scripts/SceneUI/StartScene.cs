using System.Collections.Generic;
using OverSceneScripts;
using PlayerUpgrade;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public TMP_InputField nameInput;

    public GameObject basicButtons;
    public GameObject startGamePanel;
    public GameObject loadGamePanel;
    public GameObject creditPanel;
    public GameObject title;

    public PlayerData playerData;
    [SerializeField] private WeaponData[] weaponDatas;

    public void CallStartGamePanel()
    {
        startGamePanel.SetActive(true);
        basicButtons.SetActive(false);
        title.SetActive(false);
    }
    public void CallCreditPanel()
    {
        creditPanel.SetActive(true);
        basicButtons.SetActive(false);
        title.SetActive(false);
    }
    public void CallLoadGamePanel()
    {
        loadGamePanel.SetActive(true);
        basicButtons.SetActive(false);
        title.SetActive(false);
    }
    public void StartGame()
    {
        string enteredName = nameInput.text;

        if (string.IsNullOrEmpty(enteredName))
        {
            Debug.LogWarning("이름을 입력해주세요!");
            return;
        }
        var playerData = GameManager.Instance.playerData;

        // 값 저장
        playerData.userName = enteredName;
        playerData.SetStat(StatType.MaxEnergy, 10f);
        playerData.SetStat(StatType.CurEnergy, 10f);
        playerData.SetStat(StatType.atk, 10f);
        playerData.SetStat(StatType.Gold, 100f);
        playerData.SetStat(StatType.critRate, 10f);
        playerData.SetStat(StatType.goldGain, 100f);
        // 누락 방지
        // 업그레이드 기본값 설정
        UserData initialData = PlayerDataConverter.ToUserData(playerData,weaponDatas);

        // 초기화할 업그레이드 리스트 추가
        initialData.upgradeLevels = new List<UpgradeSaveData>
        {
            new UpgradeSaveData { statName = "atk", level = 0 },
            new UpgradeSaveData { statName = "MaxEnergy", level = 0 },
            new UpgradeSaveData { statName = "critRate", level = 0 },
            new UpgradeSaveData { statName = "goldGain", level = 0 },
        };
        
        //저장
        GameManager.Instance.userDataManager.SaveUserData(initialData, enteredName);
    }

    public void BackToBasic()
    {
        basicButtons.SetActive(true);
        title.SetActive(true);
        startGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        creditPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료 시도");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
