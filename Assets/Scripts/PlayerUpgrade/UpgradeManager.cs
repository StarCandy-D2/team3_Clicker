using System.Collections;
using System.Collections.Generic;
using PlayerUpgrade;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerUpgrad
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager instance;
        public PlayerData playerData;
        public List<UpgradeData> upgradeData;
        
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
        //업그레이드시 stat값 계산
        public void UpgradeStat(StatType stat)
        {
            var upgrade = upgradeData.Find(u => u.statName == stat.ToString());
            if (upgrade == null) return;

            float cost = upgrade.GetUpgradeCost();
            if (playerData.GetStat(StatType.Gold) >= cost)
            {
                playerData.SetStat(StatType.Gold, playerData.GetStat(StatType.Gold) - cost);
                upgrade.level++;

                playerData.SetStat(stat, upgrade.GetCurStatValue());
            }
            else
            {
                Debug.Log("골드 부족");
            }
        }

        

        public void GetGold()//테스트용 임시 매서드
        {
            playerData.SetStat(StatType.Gold,playerData.GetStat(StatType.Gold) + playerData.GetStat(StatType.goldGain));
        }
        
        //각 stat별 level 저장
        public void SaveUpgradeLevels(UserData data)
        {
            data.upgradeLevels = new List<UpgradeSaveData>();
            foreach (var upgrade in upgradeData)
            {
                data.upgradeLevels.Add(new UpgradeSaveData
                {
                    statName = upgrade.statName,
                    level = upgrade.level
                });
            }
        }
        //level 불러오기
        public void LoadUpgradeLevels(UserData data)
        {
            foreach (var upgradeSave in data.upgradeLevels)
            {
                var upgrade = upgradeData.Find(u => u.statName == upgradeSave.statName);
                if (upgrade != null)
                {
                    upgrade.level = upgradeSave.level;
                    if (System.Enum.TryParse(upgrade.statName, out StatType stat))
                    {
                        playerData.SetStat(stat, upgrade.GetCurStatValue());
                    }
                }
            }
        }
    }
}
