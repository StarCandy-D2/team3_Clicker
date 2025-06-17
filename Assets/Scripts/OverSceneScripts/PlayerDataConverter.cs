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
                Oxygen = so.GetStat(StatType.Oxygen),
                atk = so.GetStat(StatType.atk),
                critRate = so.GetStat(StatType.critRate),
                gold = so.GetStat(StatType.Gold),
                goldGain = so.GetStat(StatType.goldGain),
                atkRate = so.GetStat(StatType.atkRate),
                autoAtktime = so.GetStat(StatType.autoAtktime),
                reviveAtkRate = so.GetStat(StatType.reviveAtkRate)
                // 기타 필드 추가
            };
        }

        public static void ApplyToPlayerData(UserData json, PlayerData so)
        {
            so.userName = json.userName;
            so.SetStat(StatType.Oxygen, json.Oxygen);
            so.SetStat(StatType.atk, json.atk);
            so.SetStat(StatType.critRate, json.critRate);
            so.SetStat(StatType.Gold, json.gold);
            so.SetStat(StatType.goldGain, json.goldGain);
            so.SetStat(StatType.atkRate, json.atkRate);
            so.SetStat(StatType.autoAtktime, json.autoAtktime);
            so.SetStat(StatType.reviveAtkRate, json.reviveAtkRate);
            // 기타 필드 추가
        }
    }
}

