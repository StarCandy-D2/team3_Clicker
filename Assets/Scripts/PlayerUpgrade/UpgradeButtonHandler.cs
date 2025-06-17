using PlayerUpgrad;
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

        void Update()
        {
            if (isPointerDown && !isHolding)
            {
                holdTime += Time.deltaTime;
                if (holdTime >= minHoldTime)
                {
                    isHolding = true;
                    UpgradeManager.instance.StartUpgradeHold(statType);
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
                UpgradeManager.instance.StopUpgradeHold();
            }
            else
            {
                UpgradeManager.instance.UpgradeStat(statType);
            }

            holdTime = 0f;
            isHolding = false;
        }
    }
}
