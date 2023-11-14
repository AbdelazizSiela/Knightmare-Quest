using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOutsideTrigger : MonoBehaviour
{
    [SerializeField] private GameObject outisdeLayer, secondOutsideLayer;

    private bool isVisiting;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isVisiting)
        {
            secondOutsideLayer.SetActive(true);
            outisdeLayer.SetActive(false);

            isVisiting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isVisiting)
        {
            secondOutsideLayer.SetActive(false);
            outisdeLayer.SetActive(true);

            isVisiting = false;
        }
    }
}
