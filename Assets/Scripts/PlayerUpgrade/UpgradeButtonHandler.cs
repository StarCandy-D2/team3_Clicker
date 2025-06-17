using System.Collections;
using PlayerUpgrade;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace PlayerUpgrade
{
    public class UpgradeButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public StatType statType;
        
        private bool isPointerDown = false;
        private float holdTime = 0f;
        private bool isHolding = false;
        public float minHoldTime;

        private Coroutine upgraderoutine;
        void Update()
        {
            if (isPointerDown && !isHolding)
            {
                holdTime += Time.deltaTime;
                if (holdTime >= minHoldTime)
                {
                    isHolding = true;
                    StartUpgradeHold(statType);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            holdTime = 0f;
            isHolding = false;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;

            if (isHolding)
            {
                StopUpgradeHold();
            }
            else
            {
                UpgradeManager.instance.UpgradeStat(statType);
            }

            holdTime = 0f;
            isHolding = false;
        }
        
        //홀딩시 코루틴 실행
        public void StartUpgradeHold(StatType statType)
        {
            upgraderoutine = StartCoroutine(UpgradeLoop(statType));
        }
        //코루틴 정지
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
                UpgradeManager.instance.UpgradeStat(statType);
                yield return new WaitForSeconds(0.2f); //업그레이드 딜레이
            }
        }
    }
}
