using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private float minAttackRange = 2f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float delayBetweenAttacks = 1f,safeTime = 0.5f;

    [SerializeField] private int damage = 25;

    private bool cantAttack;

    private Transform player;

    private LayerMask attackLayer;

    private Animator anim;

    private void Start()
    {
        player = ReferencesManager.instance.player;
        attackLayer = ReferencesManager.instance.enemyAttackLayer;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer <= minAttackRange)
        {
            if(!cantAttack)
            {
                StartCoroutine(Attack());
                cantAttack = true;
                anim.SetTrigger("Attack");
            }
        }
    }

    private IEnumerator Attack() 
    {
        yield return new WaitForSeconds(safeTime);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, attackLayer);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.transform.CompareTag("Player") && !hit.transform.GetComponent<PlayerMovement>().isDashing)
            {
                PlayerHealth playerHealth = hit.transform.GetComponent<PlayerHealth>();

                playerHealth.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(delayBetweenAttacks - safeTime);
        cantAttack = false;

    }

}
