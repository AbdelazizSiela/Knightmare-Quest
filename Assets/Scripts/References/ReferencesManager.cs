using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencesManager : MonoBehaviour
{
    public static ReferencesManager instance;

    public Transform player;

    public GameObject outsideLayers, innerLayers,groundLayers;
    public GameObject levelName;
    public GameObject blackTransition;

    public List<GameObject> enemiesList;

    public LayerMask enemyAttackLayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
