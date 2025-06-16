using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public void TriggerDialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>
        {
            new DialogueLine { speaker = "드릴봇", message = "이곳은... 어째서 이렇게 깊지?" },
            new DialogueLine { speaker = "??", message = "너는... 왜 여기로 내려왔나." },
            new DialogueLine { speaker = "드릴봇", message = "나는 기억을 찾아야 해." }
        };

        dialogueManager.StartDialogue(lines);
    }
}
