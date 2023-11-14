using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevsOnly : MonoBehaviour
{
    [SerializeField] private bool isActive;

    [SerializeField] private GameObject enemyWave;
    [SerializeField] private Transform playerPosition;

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(enemyWave,new Vector3(playerPosition.transform.position.x + Random.Range(-5f,5f),
                playerPosition.transform.position.y + Random.Range(-5f, 5f),
                0), Quaternion.identity);
        }
    }
}
