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
        public PlayerUpgradeUIManager pUUI;
        public List<UpgradeData> upgradeData;

        void Start()
        {
            instance = this;
        }

        public void UpgradeStat(string statName)
        {
            var upgrade = upgradeData.Find(u => u.statName == statName);
            if (upgrade == null) return;

            float cost = upgrade.GetUpgradeCost();
            if (playerData.GetStat(StatType.Gold) >= cost)
            {
                playerData.SetStat(StatType.Gold, playerData.GetStat(StatType.Gold) - cost);
                upgrade.level++;

                SetUpgradeStat(upgrade);
            }
            else
            {
                Debug.Log("골드 부족");
            }
        }

        private void SetUpgradeStat(UpgradeData upgrade)
        {
            switch (upgrade.statName)
            {
                case "atk": playerData.SetStat(StatType.atk, upgrade.GetCurStatValue()); break;
                case "critRate": playerData.SetStat(StatType.critRate, upgrade.GetCurStatValue()); break;
                case "Oxygen": playerData.SetStat(StatType.Oxygen, upgrade.GetCurStatValue()); break;
                case "goldGain": playerData.SetStat(StatType.goldGain, upgrade.GetCurStatValue()); break;
            }
        }

        public void GetGold()//테스트용 임시 매서드
        {
            playerData.SetStat(StatType.Gold,playerData.GetStat(StatType.Gold) + playerData.GetStat(StatType.goldGain));
        }
    }
}
