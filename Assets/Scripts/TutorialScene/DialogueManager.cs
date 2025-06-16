using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueTyper typer;
    public TMP_Text speakerNameText;

    private Queue<DialogueLine> dialogueQueue = new Queue<DialogueLine>();
    private bool isDialogueActive = false;
    public DialogueTrigger DialogueTrigger;
    void Start()
    {
        DialogueTrigger.TriggerDialogue(); // 테스트용 강제 실행
    }
    public void StartDialogue(List<DialogueLine> lines)
    {

        dialogueQueue.Clear();
        foreach (var line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        isDialogueActive = true;
        ShowNextLine();
    }

    public void ShowNextLine()
    {

        if (!isDialogueActive)
        {
            return;
        }

        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueQueue.Dequeue();

        if (speakerNameText != null)
            speakerNameText.text = line.speaker;

        typer.StartDialogue(line.message);
    }


    public void EndDialogue()
    {
        isDialogueActive = false;
        speakerNameText.text = "";
        typer.dialogueText.text = "";

        Debug.Log("대화 종료");
    }
}
