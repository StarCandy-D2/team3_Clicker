using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] Shovels;
    // Start is called before the first frame update
    void Start()
    {
        int index = GameManager.Instance.equippedWeaponIndex;
        ShowShovel(index);
    }

    public void ShowShovel(int index)
    {
        foreach (var shovel in Shovels)
        {
            shovel.SetActive(false);
        }

        if (index >= 0 && index < Shovels.Length)
        {
            Shovels[index].SetActive(true);
        }
        else
        {
            Debug.LogError("잘못된 삽 인덱스입니다.");
        }
    }
}
