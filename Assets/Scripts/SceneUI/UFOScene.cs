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

    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
        basicbuttons.SetActive(false);
    }
    public void ShowSkillPanel()
    {
        basicbuttons.SetActive(false);
    }
    public void ShowStatPanel()
    {
        statPanel.SetActive(true);
        basicbuttons.SetActive(false);
    }
    public void ShowShopPanel()
    {
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
        SceneManager.LoadScene("MainScene");
    }
}
