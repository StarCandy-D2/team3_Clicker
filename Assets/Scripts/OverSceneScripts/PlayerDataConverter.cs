public static class PlayerDataConverter
{
    public static UserData ToUserData(PlayerData so)
    {
        return new UserData
        {
            userName = so.userName,
            Oxygen = so.Oxygen,
            atk = so.atk,
            critRate = so.critRate,
            gold = so.gold,
            goldGain = so.goldGain,
            // 기타 필드 추가
        };
    }

    public static void ApplyToPlayerData(UserData json, PlayerData so)
    {
        so.userName = json.userName;
        so.Oxygen = json.Oxygen;
        so.atk = json.atk;
        so.critRate = json.critRate;
        so.gold = json.gold;
        so.goldGain = json.goldGain;
        // 기타 필드 추가
    }
}
