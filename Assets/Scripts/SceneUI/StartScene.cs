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

        // PlayerData ScriptableObject 인스턴스 생성
        playerData = ScriptableObject.CreateInstance<PlayerData>();

        // 데이터 설정
        playerData.userName = enteredName;
        playerData.name = enteredName; // ScriptableObject 자체 이름

#if UNITY_EDITOR
        //에디터 상에서 Asset 저장 (테스트용)
        UnityEditor.AssetDatabase.CreateAsset(playerData, $"Assets/UserData/{enteredName}_PlayerData.asset");
        UnityEditor.AssetDatabase.SaveAssets();
        Debug.Log($"PlayerData ScriptableObject 생성됨: {enteredName}");
#endif

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
