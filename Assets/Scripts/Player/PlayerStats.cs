using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public bool isOutside;

    private void Start()
    {
        isOutside = true;
    }
}
