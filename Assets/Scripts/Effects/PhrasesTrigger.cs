using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhrasesTrigger : MonoBehaviour
{
    private DialogueBox dialogueBox;

    public int enemiesNeededToDisplay;

    private void Start()
    {
        dialogueBox = FindObjectOfType<DialogueBox>();
        ResetEnemiesNeededToDisplay();
    }
    public void ResetEnemiesNeededToDisplay()
    {
        enemiesNeededToDisplay = Random.Range(4, 8);
    }
    public void OnLevelCompletion()
    {
        dialogueBox.StartDialogue(false);
    }
    public void OnEnemyKilled()
    {
        enemiesNeededToDisplay--;

        if(enemiesNeededToDisplay <= 0)
        {
            dialogueBox.StartDialogue(false);
            ResetEnemiesNeededToDisplay();
        }
    }
}
