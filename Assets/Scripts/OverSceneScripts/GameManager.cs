using System.Collections;
using System.Collections.Generic;
using OverSceneScripts;
using PlayerUpgrad;
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
        UserData saveData = PlayerDataConverter.ToUserData(playerData);
        UpgradeManager.instance.SaveUpgradeLevels(saveData);
        userDataManager.SaveUserData(saveData, saveData.userName);

    }

    public void LoadPlayerDataFromJson(string fileName)
    {
        UserData loaded = userDataManager.LoadUserData(fileName);
        PlayerDataConverter.ApplyToPlayerData(loaded, playerData);
        UpgradeManager.instance.LoadUpgradeLevels(loaded);
    }
}