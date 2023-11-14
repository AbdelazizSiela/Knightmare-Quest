using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string levelName;
    
    private ReferencesManager referencesManager;

    private bool isVisiting;

    [SerializeField] private triggerStatus status;
    [SerializeField] private Transform teleportPoint;

    private Transform player;
    private PlayerStats playerStats;

    private void Start()
    {
        referencesManager = ReferencesManager.instance;

        player = referencesManager.player;
        playerStats = player.GetComponent<PlayerStats>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isVisiting)
        {
            referencesManager.blackTransition.GetComponent<Animator>().SetTrigger("Fade");

            player.GetComponent<PlayerMovement>().StopPlayer();

            Invoke(nameof(TeleportPlayer), 0.2f);

            isVisiting = true;
        }
    }
    private void TeleportPlayer()
    {
        player.transform.position = teleportPoint.position;
        player.GetComponent<PlayerMovement>().enabled = true;

        VistTrigger(player.transform.position);

        if(levelName == "Amphi 3" && status == triggerStatus.entrance)
        {
            FindObjectOfType<BossCutscene>().StartCutscene();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isVisiting)
        {
            isVisiting = false;
        }
    }
    public void VistTrigger(Vector3 pos)
    {
        if (status == triggerStatus.entrance) //Entering
        {
            TriggerLayers(true);

            referencesManager.levelName.GetComponent<TextMeshProUGUI>().text = levelName;
            referencesManager.levelName.GetComponent<Animator>().SetTrigger("Fade");
        }
        else //Exiting
        {
            TriggerLayers(false);

            referencesManager.levelName.GetComponent<Animator>().SetTrigger("Cancel");
        }   
    }

    private void TriggerLayers(bool state)
    {
        referencesManager.outsideLayers.SetActive(!state);
        referencesManager.groundLayers.SetActive(!state);
        referencesManager.innerLayers.SetActive(state);

        playerStats.isOutside = !state;

        foreach (GameObject enemy in referencesManager.enemiesList)
        {
            if (enemy.GetComponent<EnemyStats>().isOutside != playerStats.isOutside)
            {
                enemy.SetActive(false);
                enemy.GetComponent<AIUnit>().enabled = false;
            }
        }
        foreach (GameObject enemy in referencesManager.enemiesList)
        {
            if (enemy.GetComponent<EnemyStats>().isOutside == playerStats.isOutside && !enemy.activeSelf)
            {
                enemy.SetActive(true);
                enemy.GetComponent<AIUnit>().enabled = true;
            }
        }
    }

   
}

public enum triggerStatus
{
    entrance,
    exit,
}
