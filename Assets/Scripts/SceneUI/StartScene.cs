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
        playerData.SetStat(StatType.Oxygen, 100f);
        playerData.SetStat(StatType.atk, 10f);
        playerData.SetStat(StatType.Gold, 100f);
        playerData.SetStat(StatType.critRate, 10f);
        playerData.SetStat(StatType.goldGain, 0f); // 누락 방지
        // 저장
        UserData initialData = PlayerDataConverter.ToUserData(playerData);
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
