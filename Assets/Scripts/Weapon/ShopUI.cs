using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Buttons")] 
    public GameObject weapon;
    public GameObject weapon2;
    public GameObject equippanel;

    public void OnClickNextButton()
    {
        weapon.SetActive(false);
        weapon2.SetActive(true);
    }

    public void OnClickUndoButton()
    {
        weapon2.SetActive(false);
        weapon.SetActive(true);
    }

    public void EquipButton()
    {
        equippanel.SetActive(true);
    }
}
