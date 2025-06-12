using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("선택창")] 
    [SerializeField] private GameObject[] uiPanels;
    private int currentIndex = 0;
    
    public GameObject equippanel;
    
    public void OnClickNextButton()
    {
        if (currentIndex >= uiPanels.Length - 1) return;
        //현재 UI 비활성화
        uiPanels[currentIndex].SetActive(false);
        //인덱스 증가
        currentIndex++;

        //범위 체크
        // if (currentIndex > uiPanels.Length)
        // {
        //     //마지막 패널에서 멈추려고 하는데 안되네;;
        //     currentIndex = uiPanels.Length - 1;
        //     return;
        // }
        //다음 UI활성화
        uiPanels[currentIndex].SetActive(true);
    }
    
    public void OnClickUndoButton()
    {
        if (currentIndex <= 0) return;
        //현재 UI 비활성화
        uiPanels[currentIndex].SetActive(false);
        //인덱스 감소
        currentIndex--;
        //표출
        uiPanels[currentIndex].SetActive(true);
    }

    public void EquipButton()
    {
        equippanel.SetActive(true);
    }
}
