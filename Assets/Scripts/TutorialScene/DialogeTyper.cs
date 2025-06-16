using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTyper : MonoBehaviour
{
    public TMP_Text dialogueText;
    public AudioSource audioSource;
    public AudioClip typeSFX;
    public float typingSpeed = 0.04f;

    private Coroutine typingCoroutine;
    private string currentMessage;
    private bool canClickToProceed = false;

    public UnityEvent onDialogueComplete;
    private void Awake()
    {
        StartCoroutine(EnableClickDelay(0.1f)); // 실행 여부만 테스트용
    }
    public void StartDialogue(string message)
    {

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        currentMessage = message;
        typingCoroutine = StartCoroutine(TypeText(message));

        canClickToProceed = false;
        StartCoroutine(EnableClickDelay(0.1f));
    }

    IEnumerator EnableClickDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canClickToProceed = true;
    }

    IEnumerator TypeText(string message)
    {
        dialogueText.text = "";

        for (int i = 0; i < message.Length; i++)
        {
            dialogueText.text += message[i];

            if (typeSFX && audioSource)
                audioSource.PlayOneShot(typeSFX, 0.5f);

            yield return new WaitForSeconds(typingSpeed);
        }

        typingCoroutine = null; // 타이핑 끝남
    }
    private void Update()
    {

        if (!canClickToProceed)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentMessage;
                typingCoroutine = null;
            }
            else
            {
                onDialogueComplete?.Invoke();
            }
        }
    }
}
