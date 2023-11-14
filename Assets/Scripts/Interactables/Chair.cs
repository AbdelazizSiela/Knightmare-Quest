using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private Animator anim;

    private bool isDestroyed;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void DestroyChair()
    {
        if (isDestroyed) return;

        anim.SetTrigger("Break");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
        isDestroyed = true;
    }
    
}
