using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public PlayerData playerData; 

    void Start()
    {
        TriggerDialogue(); // 자동 실행 (테스트용)
    }

    public void TriggerDialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>
        {
            new DialogueLine { speaker = "PLANET : DA BBU SYEO", message = "30XX년 06월 19일\n한 과학자가 있었다." ,playAnimation = false },
            new DialogueLine { speaker = "", message = "그의 이름\nDr. BBU BBU BBU BBU BBU BBU BBU BBU BBU Sahur\n.\n.\n.\n줄여서 닥터 뿌라고 하자...." ,playAnimation = true},
            new DialogueLine { speaker = "닥터뿌", message = $"드디어... \r\n내 오랜 염원이 이루어진다...!\r\n나를 무시했던 녀석들\r\n모조리 부셔주겠어.. \r\n일어나라 나의 창조물..!\r\n \r\n{playerData.userName}!" , playAnimation = true},
            new DialogueLine { speaker = "닥터뿌", message = "드디어 일어났군..\r\n너의 목표는 행성들의 땅을 전부\r\n파.괴.해.버.리.는.것\r\n실수는 용납하지 않는다!\r\n", playAnimation = false},
            new DialogueLine { speaker = "", message = "임무코드 : OKAY333\r\n.\r\n.\r\n임무 - 모든 행성의 파괴\r\n.\r\n.\r\n.\r\n임무 승인\r\n\r\n알..겠...습..니..다..\r\n\r\n행성 파괴를 시작 합니다",playAnimation = true }
        };

        dialogueManager.StartDialogue(lines);
    }
}
