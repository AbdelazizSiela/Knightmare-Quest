using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    [SerializeField] private Animator blackBarsAnim;
    private DialogueBox dialogueBox;

    public static bool inCutscene;

    private void Start()
    {
        dialogueBox = FindObjectOfType<DialogueBox>();
    }

    public void StartCutscene()
    {
        FindObjectOfType<PlayerMovement>().StopPlayer();

        dialogueBox.StartDialogue(true);
        blackBarsAnim.SetTrigger("Activate");

        inCutscene = true;
    }

    public void EndCutscene()
    {
        FindObjectOfType<PlayerMovement>().enabled = true;
        blackBarsAnim.SetTrigger("Desactivate");

        inCutscene = false;
    }
}
