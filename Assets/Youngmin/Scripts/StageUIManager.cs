using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    public static StageUIManager Instance;
    private EnemyGenerator enemyGenerator;
    
    [Header("UI")] public TextMeshProUGUI stageText;
    public TextMeshProUGUI layerText;

    [Header("스테이지")] public int currentStage = 1;
    public int currentLayer = 1;
    public int maxLayersPerStage = 100;
    public float sessionGold;

    [Header("스테이지별 골드")] 
    public float[] stageGoldRewards = { 10, 15, 25, 40, 60, 1500 };
    

    public string[] stageNames = { "지각", "상부맨틀", "하부맨틀", "외핵", "내핵","지구의 핵" };


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

    public void AddSessionGold(float amount)
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
            int stageIndex = Mathf.Clamp(currentStage -1,0,stageNames.Length-1);
            string stageName = stageNames[stageIndex];
            stageText.text = $"{stageName}";

        }

        if (layerText != null)
        {
            if (currentStage >= 6)
            {
                stageText.text = "지구의 핵";
                layerText.text = "";
            }

            else
            layerText.text = $"{currentLayer}m";
        }
    }

    public float GetCurrentStageGoldReward()
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

    public void OnClickNextButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("UFOScene");
    }
}





