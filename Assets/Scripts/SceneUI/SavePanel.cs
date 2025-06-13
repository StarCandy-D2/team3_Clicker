using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavePanelUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform contentParent;

    private void OnEnable()
    {
        RefreshSaveList();
    }

    public void RefreshSaveList()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        List<string> fileNames = UserDataManager.Instance.GetSaveFileNames();
        foreach (string name in fileNames)
        {
            GameObject btn = Instantiate(buttonPrefab, contentParent);
            btn.GetComponentInChildren<TMP_Text>().text = name;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.Instance.LoadPlayerDataFromJson(name); // 불러오기 기능
            });
        }
    }

    public void SaveCurrentUser()
    {
        GameManager.Instance.SavePlayerDataToJson(); // 저장 기능
    }
}