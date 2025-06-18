using System;
using PlayerUpgrade;

namespace OverSceneScripts
{
    public static class PlayerDataConverter
    {
        public static UserData ToUserData(PlayerData so)
        {
            return new UserData
            {
                userName = so.userName,
                curEnergy = so.GetStat(StatType.CurEnergy),
                atk = so.GetStat(StatType.atk),
                critRate = so.GetStat(StatType.critRate),
                gold = so.GetStat(StatType.Gold),
                goldGain = so.GetStat(StatType.goldGain),
                // 기타 필드 추가
            };
        }

        public static void ApplyToPlayerData(UserData json, PlayerData so)
        {
            so.userName = json.userName;
            so.SetStat(StatType.CurEnergy, json.curEnergy);
            so.SetStat(StatType.atk, json.atk);
            so.SetStat(StatType.critRate, json.critRate);
            so.SetStat(StatType.Gold, json.gold);
            so.SetStat(StatType.goldGain, json.goldGain);
            // 기타 필드 추가
        }
    }
}

