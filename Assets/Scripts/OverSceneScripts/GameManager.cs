using PlayerUpgrade;
using System;
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
            userName = playerData.userName
        };

        // 현재 PlayerData의 statList 내용을 모두 복사
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            float value = playerData.GetStat(stat);
            saveData.SetStat(stat, value);
        }

        userDataManager.SaveUserData(saveData, playerData.userName);
    }

    public void LoadPlayerDataFromJson(string fileName)
    {
        UserData loaded = userDataManager.LoadUserData(fileName);
        if (loaded != null)
        {
            playerData.userName = loaded.userName;

            foreach (StatEntry statEntry in loaded.statList)
            {
                playerData.SetStat(statEntry.statType, statEntry.value);
            }
        }
    }
}
