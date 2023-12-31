using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private AudioClip[] bossMumblingSounds,playerMumblingSounds;
    [SerializeField] private AudioSource source;

    private DialogueBox dialogueBox;

    private bool isCutscene;
    private string talkerName;
    private int lastSoundIndex;

    private void Awake()
    {
        dialogueBox = FindObjectOfType<DialogueBox>();
    }
    public void StartTypewriter(bool cutsceneState,string talker)
    {
        isCutscene = cutsceneState;
        talkerName = talker;
        
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

            switch(talkerName)
            {
                case "Boss":
                    int randomIndex = lastSoundIndex;
                    while (randomIndex == lastSoundIndex)
                    {
                        randomIndex = Random.Range(0, bossMumblingSounds.Length);
                    }
                    
                    if (!source.isPlaying) source.PlayOneShot(bossMumblingSounds[randomIndex]);
                    lastSoundIndex = randomIndex;
                    break;
                case "Player":
                    int randomIndex2 = lastSoundIndex;
                    while (randomIndex2 == lastSoundIndex)
                    {
                        randomIndex2 = Random.Range(0, playerMumblingSounds.Length);
                    }

                    if (!source.isPlaying) source.PlayOneShot(playerMumblingSounds[randomIndex2]);
                    lastSoundIndex = randomIndex2;
                    break;
            }
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