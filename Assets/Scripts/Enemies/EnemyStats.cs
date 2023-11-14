using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [HideInInspector] public bool isOutside;

    private void Start()
    {
        ReferencesManager.instance.enemiesList.Add(gameObject);

        isOutside = FindObjectOfType<PlayerStats>().isOutside;
    }
    private void OnDestroy()
    {
        ReferencesManager.instance.enemiesList.Remove(gameObject);
    }
}
