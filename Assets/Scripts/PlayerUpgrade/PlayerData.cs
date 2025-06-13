
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public string userName;
    public float Oxygen;
    public float atk;
    public float critRate;
    public float gold;
    public float goldGain;
}
