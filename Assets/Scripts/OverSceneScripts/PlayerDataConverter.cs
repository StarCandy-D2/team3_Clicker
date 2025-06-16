using PlayerUpgrade;
using System;

public static class PlayerDataConverter
{
    public static UserData ToUserData(PlayerData so)
    {
        UserData data = new UserData
        {
            userName = so.userName
        };

        // 모든 StatType에 대해 저장
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            float value = so.GetStat(stat);
            data.SetStat(stat, value);
        }

        return data;
    }

    public static void ApplyToPlayerData(UserData json, PlayerData so)
    {
        so.userName = json.userName;

        foreach (StatEntry entry in json.statList)
        {
            so.SetStat(entry.statType, entry.value);
        }
    }
}

