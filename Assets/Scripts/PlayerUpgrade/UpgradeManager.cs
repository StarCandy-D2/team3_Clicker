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

        private Coroutine upgraderoutine;
        void Start()
        {
            instance = this;
        }

        public void StartUpgradeHold(StatType statType)
        {
            upgraderoutine = StartCoroutine(UpgradeLoop(statType));
        }

        public void StopUpgradeHold()
        {
            if (upgraderoutine != null)
            {
                StopCoroutine(upgraderoutine);
                upgraderoutine = null;
            }
        }

        private IEnumerator UpgradeLoop(StatType statType)
        {
            while (true)
            {
                UpgradeStat(statType);
                yield return new WaitForSeconds(0.5f); // 원하는 간격
            }
        }
        
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
    }
}
