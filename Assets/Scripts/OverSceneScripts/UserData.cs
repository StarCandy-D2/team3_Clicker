using PlayerUpgrade;
using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public string userName;

    public List<StatEntry> statList = new(); // StatType, value 묶음

    // 스탯 조회
    public float GetStat(StatType statType)
    {
        var entry = statList.Find(e => e.statType == statType);
        return entry != null ? entry.value : 0f;
    }

    // 스탯 설정
    public void SetStat(StatType statType, float value)
    {
        var entry = statList.Find(e => e.statType == statType);
        if (entry != null)
            entry.value = value;
        else
            statList.Add(new StatEntry { statType = statType, value = value });
    }
}
