
using System;
using System.Collections.Generic;
using PlayerUpgrade;
using UnityEngine;

[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public float value;
}//

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public string userName;
    
    [SerializeField]
    private List<StatEntry> statList = new();//
    
    //StatType 변화시 OnStatChanged실행
    public event Action<StatType, float> OnStatChanged;
    
    //Stat값 가져오기
    public float GetStat(StatType stat)
    {
        var entry = statList.Find(e => e.statType == stat);
        return entry != null ? entry.value : 0f;
    }
    
    //Stat값 설정
    public void SetStat(StatType stat, float value)
    {
        var entry = statList.Find(e => e.statType == stat);
        if (entry != null)
        {
            if (entry.value != value)
            {
                entry.value = value;
                OnStatChanged?.Invoke(stat, value);
            }
        }
        else
        {
            statList.Add(new StatEntry { statType = stat, value = value });
            OnStatChanged?.Invoke(stat, value);
        }
    }
    
    // 작성법 예시
    //playerData.GetStat(StatType.Gold)
    //playerData.SetStat(StatType.불러올 타입, 저장 할 값);
    
    public void Init(float oxygen, float atk, float critRate, float gold, float goldGain)
    {
        SetStat(StatType.Oxygen, oxygen);
        SetStat(StatType.atk, atk);
        SetStat(StatType.critRate, critRate);
        SetStat(StatType.Gold, gold);
        SetStat(StatType.goldGain, goldGain);
    }
}
