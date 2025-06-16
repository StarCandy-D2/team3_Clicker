
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public string userName;
    public float Oxygen;
    public float atk = 10f;
    public float critRate =20f;
    public float gold;
    public float goldGain;
}
