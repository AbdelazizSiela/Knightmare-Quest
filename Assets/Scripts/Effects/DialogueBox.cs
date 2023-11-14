using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private Animator dialogueBoxAnimator;
    private DialoguePhrases dialoguePhrase;
    private TypeWriterEffect typeWriterEffect;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Start()
    {
        dialoguePhrase = FindObjectOfType<DialoguePhrases>();
        typeWriterEffect = FindObjectOfType<TypeWriterEffect>();

        dialogueBoxAnimator.gameObject.SetActive(true);
    }
    public void StartDialogue(bool isCutscene)
    {
        string dialogue = "";
        if (!isCutscene)
        {
            dialogue = dialoguePhrase.RandomPhrase();
            dialogueBoxAnimator.SetTrigger("Pop");
        }
        else
        {
            dialogue = dialoguePhrase.BossCutscenePhrase();
            dialogueBoxAnimator.SetTrigger("Cutscene_pop");
        }

        dialogueText.text = dialogue;
        typeWriterEffect.StartTypewriter(isCutscene);

    }
    public void StopDialogue(bool isCutscene)
    {
        if (!isCutscene)
        {
            dialogueBoxAnimator.SetTrigger("Stop");

            typeWriterEffect.StopAllCoroutines();
        }
        else
        {

            if (dialoguePhrase.currentBossCutscenePhraseIndex >= dialoguePhrase.bossCutscenePhrases.Length)
            {
                dialogueBoxAnimator.SetTrigger("Cutscene_stop");
                FindObjectOfType<BossCutscene>().EndCutscene();
                typeWriterEffect.StopAllCoroutines();
            }
            else
            {
                StartDialogue(true);
            }
        }
    }
     


}
