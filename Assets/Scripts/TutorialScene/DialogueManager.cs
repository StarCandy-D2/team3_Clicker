using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public DialogueTyper[] typers;               // 텍스트
    public TMP_Text[] speakerNameTexts;          // 화자
    public Animator[] dialogueAnimators; //애니메이션
    private List<DialogueLine> dialogueLines;    // 전체 대사 목록
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    public Animator skip;

    public void StartDialogue(List<DialogueLine> lines)
    {
        if (isDialogueActive) return; // 중복 방지

        dialogueLines = lines;
        currentLineIndex = 0;
        isDialogueActive = true;

        ShowCurrentLine();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            // 타이핑 중이면 클릭 무시
            if (currentLineIndex < typers.Length && typers[currentLineIndex].IsTyping)
                return;

            if (currentLineIndex < dialogueLines.Count)
            {
                DialogueLine currentLine = dialogueLines[currentLineIndex];

                // 애니메이션 재생
                if (currentLine.playAnimation &&
                    currentLineIndex < dialogueAnimators.Length &&
                    dialogueAnimators[currentLineIndex] != null) //애니메이션 true일때만
                {
                    Debug.Log(dialogueAnimators[currentLineIndex]);
                    dialogueAnimators[currentLineIndex].SetTrigger("Show"); //트리거로 재생
                }

                // 다음 대사로 이동하고 출력
                currentLineIndex++;
                ShowCurrentLine();
            }
        }
    }

    void ShowCurrentLine()
    {
        if (currentLineIndex >= dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines[currentLineIndex];

        if (currentLineIndex < typers.Length && currentLineIndex < speakerNameTexts.Length)
        {
            speakerNameTexts[currentLineIndex].text = line.speaker;
            typers[currentLineIndex].StartDialogue(line.message);

            
            
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;

        Debug.Log("대화 종료");
    }
   public void Skip()
    {

        skip.SetTrigger("Show");
        isDialogueActive = false;
        Invoke("GoToTutorial", 3f);


        
        
    }
    public void GoToTutorial() { FadeManager.Instance.FadeOutAndLoadScene("TutorialScene"); }
}
