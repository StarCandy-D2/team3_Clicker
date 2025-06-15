using System.Collections.Generic;
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
            if (playerData.gold >= cost)
            {
                playerData.gold -= cost;
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
                case "atk": playerData.atk = upgrade.GetCurStatValue(); break;
                case "critRate": playerData.critRate = upgrade.GetCurStatValue(); break;
                case "Oxygen": playerData.Oxygen = upgrade.GetCurStatValue(); break;
                case "goldGain": playerData.goldGain = upgrade.GetCurStatValue(); break;
            }
        }

        public void GetGold()//테스트용 임시 매서드
        {
            playerData.gold += playerData.goldGain;
            pUUI.Setgold();
        }
    }
}
