using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpProText;
    private string writer;

    [SerializeField] private float delayBeforeStart = 0f;
    [SerializeField] private float timeBtwChars = 0.1f;
    [SerializeField] private float timeToStopDialogue = 2f;
    [SerializeField] private string leadingChar = "";
    [SerializeField] private bool leadingCharBeforeDelay = false;

    private DialogueBox dialogueBox;

    private bool isCutscene;

    private void Awake()
    {
        dialogueBox = FindObjectOfType<DialogueBox>();
    }
    public void StartTypewriter(bool cutsceneState)
    {
        isCutscene = cutsceneState;

        StopAllCoroutines();

        writer = tmpProText.text;
        tmpProText.text = "";
        StartCoroutine("TypeWriterTMP");
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator TypeWriterTMP()
    {
        tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (tmpProText.text.Length > 0)
            {
                tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
            }
            tmpProText.text += c;
            tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
        }

        yield return new WaitForSeconds(timeToStopDialogue);
        dialogueBox.StopDialogue(isCutscene);
    }
}