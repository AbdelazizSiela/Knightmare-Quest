using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private Animator dialogueBoxAnimator;
    private DialoguePhrases dialoguePhrase;
    private TypeWriterEffect typeWriterEffect;
    [SerializeField] private TextMeshProUGUI dialogueText,talkerNameText;
    [SerializeField] private Sprite playerSprite, bossSprite;
    [SerializeField] private Image talkerImage;


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

        string talkerName = dialogue.Substring(0, dialogue.IndexOf(":"));
        talkerNameText.text = talkerName;
        switch(talkerName.Substring(0,talkerName.Length))
        {
            case "Player":
                talkerImage.sprite = playerSprite;
                break;

            case "Boss":
                talkerImage.sprite = bossSprite;
                break;

        }

        dialogue = dialogue.Substring(dialogue.IndexOf(":") + 2);

        dialogueText.text = dialogue;
        typeWriterEffect.StartTypewriter(isCutscene,talkerName);

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
