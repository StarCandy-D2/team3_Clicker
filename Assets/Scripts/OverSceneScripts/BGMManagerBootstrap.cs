using UnityEngine;

public class BGMManagerBootstrap : MonoBehaviour
{
    public GameObject bgmManagerPrefab;

    void Awake()
    {
        if (BGMManager.Instance == null)
        {
            Instantiate(bgmManagerPrefab);
        }
    }
}