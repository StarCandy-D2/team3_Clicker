using PlayerUpgrade;
using OverSceneScripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserDataManager userDataManager;
    public PlayerData playerData;
    public int equippedWeaponIndex = -1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
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