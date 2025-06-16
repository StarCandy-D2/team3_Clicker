using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    public static StageUIManager Instance;

    [Header("UI")] public TextMeshProUGUI stageText;
    public TextMeshProUGUI layerText;
    public TextMeshProUGUI sessionGoldText;
    public TextMeshProUGUI totalGoldText;

    [Header("스테이지")] public int currentStage = 1;
    public int currentLayer = 1;
    public int maxLayersPerStage = 100;
    public int sessionGold;

    [Header("스테이지별 골드")] 
    public int[] stageGoldRewards = { 10, 15, 25, 40, 60 };
    
    private string[] stageNames = { "지각", "상부맨틀", "하부맨틀", "외핵", "내핵" };

    void Awake()
    {
        if (Instance == null) Instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        sessionGold = 0;
        UpdateUI();
    }

    public void OnLayerCleared()
    {
        currentLayer++;

        if (currentLayer > maxLayersPerStage)
        {
            NextStage();
        }
        else
        {
            UpdateUI();
        }

    }

    public void AddSessionGold(int amount)
    {
        sessionGold += amount;
        UpdateUI();

    }

    void NextStage()
    {
        currentStage++;
        currentLayer = 1;
        UpdateUI();
        Debug.Log($"스테이지 {currentStage} 진입");
    }


    void UpdateUI()
    {

        if (stageText != null)
        {
            string stageName = stageNames[currentStage - 1];
            stageText.text = $"{stageName}";

        }

        if (layerText != null)
        {
            layerText.text = $"{currentLayer}층";
        }

        if (sessionGoldText != null)
        {
            sessionGoldText.text = $"획득 골드 : {sessionGold:N0}";
        }

        if (totalGoldText != null && GameManager.Instance?.playerData != null)
        {
            totalGoldText.text = $"보유 골드 : {GameManager.Instance.playerData.gold:N0}";
        }
    }

    public int GetCurrentStageGoldReward()
    {
        if (stageGoldRewards.Length >= currentStage)
        {
            return stageGoldRewards[currentStage - 1];
        }

        return 10;
    }
    public void SetStageInfo(int stage, int layer) // 테스트용
    {
        currentStage = stage;
        currentLayer = layer;
        UpdateUI();
    } 
    
}





