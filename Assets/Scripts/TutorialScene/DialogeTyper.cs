using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTyper : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float typingSpeed = 0.01f;
    public AudioSource audioSource;
    public AudioClip typeSFX;
    public bool IsTyping { get; private set; }

    public void StartDialogue(string message)
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(message));
    }

    IEnumerator TypeSentence(string sentence)
    {
        IsTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            if (letter != ' ' &&typeSFX && audioSource)
            {
                audioSource.PlayOneShot(typeSFX, 0.5f);

            }
            yield return new WaitForSeconds(typingSpeed);
        }

        IsTyping = false;
    }
}
