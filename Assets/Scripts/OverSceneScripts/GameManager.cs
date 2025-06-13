using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserDataManager userDataManager;
    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // 저장할 때
    public void SavePlayerDataToJson()
    {
        UserData saveData = new UserData
        {
            userName = playerData.userName,
            Oxygen = playerData.Oxygen,
            atk = playerData.atk,
            critRate = playerData.critRate,
            gold = playerData.gold,
            goldGain = playerData.goldGain
        };

        userDataManager.SaveUserData(saveData, playerData.userName);
    }

    public void LoadPlayerDataFromJson(string fileName)
    {
        UserData loaded = userDataManager.LoadUserData(fileName);
        if (loaded != null)
        {
            playerData.userName = loaded.userName;
            playerData.Oxygen = loaded.Oxygen;
            playerData.atk = loaded.atk;
            playerData.critRate = loaded.critRate;
            playerData.gold = loaded.gold;
            playerData.goldGain = loaded.goldGain;

            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}