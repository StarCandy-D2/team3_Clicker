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

    public PlayerData playerData;

    public void CallStartGamePanel()
    {
        startGamePanel.SetActive(true);
        basicButtons.SetActive(false);
    }
    public void CallCreditPanel()
    {
        creditPanel.SetActive(true);
        basicButtons.SetActive(false);
    }
    public void CallLoadGamePanel()
    {
        loadGamePanel.SetActive(true);
        basicButtons.SetActive(false);
    }
    public void StartGame()
    {
        string enteredName = nameInput.text;

        if (string.IsNullOrEmpty(enteredName))
        {
            Debug.LogWarning("이름을 입력해주세요!");
            return;
        }

        PlayerData playerData = GameManager.Instance.playerData;

        playerData.userName = enteredName;
        playerData.SetStat(StatType.Oxygen, 100f);
        playerData.SetStat(StatType.atk, 10f);
        playerData.SetStat(StatType.Gold, 100f);
        playerData.SetStat(StatType.critRate, 10f);
        // 초기값 설정 등...

        // 저장도 가능하다면
        UserData initialData = PlayerDataConverter.ToUserData(playerData);
        UserDataManager.Instance.SaveUserData(initialData, enteredName);
        // 이후 GameManager 또는 다음 씬에서 playerData 사용 가능
        SceneManager.LoadScene("TutorialScene");
    }

    public void BackToBasic()
    {
        basicButtons.SetActive(true);
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
