using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFlip : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject flippedTrigger;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void FlipTable()
    {
        anim.SetTrigger("Flip");
        gameObject.layer = 2;
        gameObject.tag = "FlippedTable";
        GetComponent<Collider2D>().isTrigger = true;
        flippedTrigger.SetActive(true);
    }
}
