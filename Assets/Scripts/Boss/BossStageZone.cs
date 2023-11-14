using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossStageZone : MonoBehaviour
{
    private Boss boss;

    [SerializeField] private int stage;

    [SerializeField] GameObject[] otherStageZones;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            boss.attackStage = stage;
            boss.OnChangeStage();

            for (int i = 0; i < otherStageZones.Length; i++)
            {
                otherStageZones[i].SetActive(true);
            }
            gameObject.SetActive(false);

        }
    }
}
