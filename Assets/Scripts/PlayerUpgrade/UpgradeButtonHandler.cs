
using PlayerUpgrad;
using PlayerUpgrade;
using UnityEngine;

public class UpgradeButtonHandler : MonoBehaviour
{
    public StatType statType;

    public void OnPressDown()
    {
        UpgradeManager.instance.StartUpgradeHold(statType);
    }

    public void OnPressUp()
    {
        UpgradeManager.instance.StopUpgradeHold();
    }
}
