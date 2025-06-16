using System.Collections;
using System.Collections.Generic;
using PlayerUpgrade;
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
            Oxygen = playerData.GetStat(StatType.Oxygen),
            atk = playerData.GetStat(StatType.atk),
            critRate = playerData.GetStat(StatType.critRate),
            gold = playerData.GetStat(StatType.Gold),
            goldGain = playerData.GetStat(StatType.goldGain),
        };

        userDataManager.SaveUserData(saveData, playerData.userName);
    }

    public void LoadPlayerDataFromJson(string fileName)
    {
        UserData loaded = userDataManager.LoadUserData(fileName);
        if (loaded != null)
        {
            playerData.userName = loaded.userName;
            playerData.SetStat(StatType.Oxygen, loaded.Oxygen);
            playerData.SetStat(StatType.atk, loaded.atk);
            playerData.SetStat(StatType.critRate, loaded.critRate);
            playerData.SetStat(StatType.Gold, loaded.gold);
            playerData.SetStat(StatType.goldGain, loaded.goldGain);
        }
    }
}