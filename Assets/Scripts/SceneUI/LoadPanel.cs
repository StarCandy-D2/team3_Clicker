using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    public GameObject buttonPrefab;         // 프리팹: 버튼 1개
    public Transform contentParent;         // 버튼들을 담을 부모 (스크롤뷰 안 content 등)
    public GameManager gameManager;         // GameManager 참조

    private void OnEnable()
    {
        RefreshLoadButtons();
    }

    public void RefreshLoadButtons()
    {
        // 기존 버튼 제거
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        List<string> fileNames = UserDataManager.Instance.GetSaveFileNames();

        foreach (string fileName in fileNames)
        {
            GameObject btn = Instantiate(buttonPrefab, contentParent);
            btn.GetComponentInChildren<TMP_Text>().text = fileName;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                gameManager.LoadPlayerDataFromJson(fileName);

                //버튼이 클릭되었을 때만 씬 전환
                FadeManager.Instance.FadeOutAndLoadScene("UFOScene");
            });
        }
    }
}