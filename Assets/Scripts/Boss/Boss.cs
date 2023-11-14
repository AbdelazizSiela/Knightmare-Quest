using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject commandPrefab;
    [SerializeField] private int numberOfCommands = 10;
    [SerializeField] private float circleRadius = 5f;
    [SerializeField] private float commandSpeed = 5f;
    [SerializeField] private float timeBtwAttacks = 1f;
    [SerializeField] private float bossSpeed = 1f;

    public int attackStage;

    private bool canAttack;

    private Transform player;

    private Vector3 originalPos;

    private void Start()
    {
        player = ReferencesManager.instance.player;
        canAttack = true;

        originalPos = transform.position;
    }
    void Update()
    {
        if (BossCutscene.inCutscene) return;

        if(canAttack)
        {
            Attack();
        }
        if(attackStage == 2)
        {
            // LookAtPlayer();
            MoveAwayFromPlayer();
        }
        else if(transform.position != originalPos)
        {
            Vector3 originalPosDirection = (originalPos - transform.position).normalized;
            transform.localPosition += originalPosDirection * bossSpeed * Time.deltaTime;
        }
    }

    private void MoveAwayFromPlayer()
    {
        // Assuming player is a reference to the player GameObject
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        // Move the boss away from the player
        transform.localPosition -= playerDirection * bossSpeed * Time.deltaTime;
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, 59, 83),
            Mathf.Clamp(transform.localPosition.y, -93, -90), 0);
    }
    public void OnChangeStage()
    {
        switch (attackStage)
        {
            case 0:
                numberOfCommands = 10;
                commandSpeed = 10f;
                timeBtwAttacks = 1.5f;
                break;
            case 1:
                numberOfCommands = 15;
                commandSpeed = 12.5f;
                timeBtwAttacks = 1.75f;
                break;
            case 2:
                timeBtwAttacks = 2.5f;
                break;
        }
    }
    private void Attack()
    {
        switch(attackStage)
        {
            case 0:
                SpawnCircularCommands();
                break;
            case 1:
                SpawnCircularCommands();
                break;
            case 2:
                SpawnDirectedCommands();
                break;
        }
        canAttack = false;
        Invoke("RechargeAttack", timeBtwAttacks);
    }
    private void RechargeAttack()
    {
        canAttack = true;
    }
    private void SpawnCircularCommands()
    {
        float angleStep = 180f / (numberOfCommands - 1);

        for (int i = 0; i < numberOfCommands; i++)
        {
            float angle = i * angleStep;
            float radians = Mathf.Deg2Rad * angle;

            float x = circleRadius * Mathf.Cos(radians);
            float y = circleRadius * Mathf.Sin(radians);

            Vector3 commandPosition = new Vector3(x, y, 0f) + transform.position;
            Quaternion commandRotation = Quaternion.Euler(0f, 0f, angle);

            GameObject command = Instantiate(commandPrefab, commandPosition, commandRotation);
            Rigidbody2D commandRb = command.GetComponent<Rigidbody2D>();

            commandRb.velocity = command.transform.right * commandSpeed;
        }
    }

    private void SpawnDirectedCommands()
    {
        // Assuming player is a reference to the player GameObject
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        int numberOfBullets = Random.Range(1,4);
        float angleStep = 25f; // 45 degrees for a quarter circle
        float offset = 2.5f; // Adjust this value to control the separation between bullets

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the angle between the boss and the player
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            // Apply an offset to the angle for each bullet
            angle += i * angleStep;

            // Calculate the position offset based on the offset value
            Vector3 offsetVector = Quaternion.Euler(0f, 0f, angle) * Vector3.right * offset;

            // Instantiate bullet at the boss position with the offset
            GameObject bullet = Instantiate(commandPrefab, transform.position + offsetVector, Quaternion.Euler(0f, 0f, angle));
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // Set the velocity to move towards the 
            bulletRb.velocity = playerDirection * (commandSpeed * Random.Range(0.5f,2f));
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<EndGame>().OnBossDefeated();
    }
}
