using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePhrases : MonoBehaviour
{
    public List<string> doubtPhrases;
    public List<string> tempPhrases;

    public string[] bossCutscenePhrases;
    public int currentBossCutscenePhraseIndex;

    private void Start()
    {
        ResetTempPhrases();
    }
    private void ResetTempPhrases()
    {
        foreach (string phrase in doubtPhrases)
        {
            tempPhrases.Add(phrase);
        }
       
    }
    public string RandomPhrase()
    {
        int randomIndex = Random.Range(0, tempPhrases.Count);
        string randomPhrase = tempPhrases[randomIndex];

        tempPhrases.Remove(randomPhrase);

        if(tempPhrases.Count == 0) ResetTempPhrases();

        return randomPhrase;
    }
    public string BossCutscenePhrase()
    {
        string phrase = bossCutscenePhrases[currentBossCutscenePhraseIndex];
        currentBossCutscenePhraseIndex++;

        return phrase;
    }
}
