using PlayerUpgrade;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [TextArea] public string description;
        public Vector2 screenPosition;
    }

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public RectTransform panelRect;

    public List<TutorialStep> steps;
    private int currentStep = 0;
    private bool isWaitingForTap = false;
    public Button nextButton;
    void Start()
    {
        StartTutorial();
        //nextButton.onClick.AddListener(OnClickNext);
    }

    void Update()
    {
        if (isWaitingForTap && Input.GetMouseButtonDown(0))
        {
            ContinueGame();
        }
    }

    public void StartTutorial()
    {
        currentStep = 0;
        ShowStep();
    }

    public void ShowStep()
    {
        if (currentStep >= steps.Count)
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1f;
            return;
        }

        Time.timeScale = 0f; // 게임 정지
        isWaitingForTap = true;

        tutorialPanel.SetActive(true);
        tutorialText.text = steps[currentStep].description;
        panelRect.anchoredPosition = steps[currentStep].screenPosition; // 위치 조절 (옵션)

        Debug.Log($"튜토리얼 {currentStep}단계 표시");
    }

    public void ContinueGame()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        isWaitingForTap = false;

        StartCoroutine(WaitAndShowNextStep()); // 다음 조건이 되면 다시 튜토리얼 표시
    }

    private IEnumerator WaitAndShowNextStep()
    {
        yield return new WaitForSecondsRealtime(1f); // 조건 기다림(예: 특정 이벤트)

        // 다음 단계로 진행
        currentStep++;

        // 특정 타이밍에만 다시 멈추고 튜토리얼 진행
        if (ShouldTriggerNextStep(currentStep))
        {
            ShowStep();
        }
    }

    // 필요한 경우: 특정 타이밍 조건 검사
    private bool ShouldTriggerNextStep(int stepIndex)
    {
        // 예: 모든 단계 표시
        return true;

        // 예: 특정 조건에서만 표시
        // return stepIndex == 1 || stepIndex == 3;
    }
}

