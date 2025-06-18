using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UFOScene : MonoBehaviour
{
    public GameObject basicbuttons;
    public GameObject settingPanel;
    public GameObject statPanel;
    public GameObject shopPanel;
    public GameObject equipErrorText;

    public void ShowSettingPanel()
    {
        if (equipErrorText != null)
        {
            equipErrorText.SetActive(false);
        }
        settingPanel.SetActive(true);
        basicbuttons.SetActive(false);
    }
    public void ShowSkillPanel()
    {
        basicbuttons.SetActive(false);
    }
    public void ShowStatPanel()
    {
        if (equipErrorText != null)
        {
            equipErrorText.SetActive(false);
        }
        statPanel.SetActive(true);
        basicbuttons.SetActive(false);
    }
    public void ShowShopPanel()
    {
        if (equipErrorText != null)
        {
            equipErrorText.SetActive(false);
        }
        
        shopPanel.SetActive(true);
        basicbuttons.SetActive(false);
        
    }
    public void BackToBasic()
    {
        basicbuttons.SetActive(true);
        settingPanel.SetActive(false);
        statPanel.SetActive(false);
        shopPanel.SetActive(false);
    }
    public void GotoMain()
    {
        if (GameManager.Instance.equippedWeaponIndex < 0)
        {
            equipErrorText.SetActive(true);
        }
        else
        {
            FadeManager.Instance.FadeOutAndLoadScene("MainScene");
        }
        
    }
    public void QuitGame()
    {
        Debug.Log("게임 종료 시도");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
