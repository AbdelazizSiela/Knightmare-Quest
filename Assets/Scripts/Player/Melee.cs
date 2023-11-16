using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float perAttackDuration = 0.5f;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private int attackDamage = 25;

    [SerializeField] private LayerMask attackLayerMask;

    [SerializeField] private Transform rightHitPoint, leftHitPoint, upHitPoint, downHitPoint;

    private bool hasAttacked;

    [HideInInspector] public List<Collider2D> enemiesBehindPlayer;

    private Animator anim;
    private PlayerMovement playerMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (BossCutscene.inCutscene) return;

        if(Input.GetMouseButton(0) && !hasAttacked)
        {
            Attack();
        }
    }

    public void DashAttack()
    {
        if (!hasAttacked) return;

        switch (playerMovement.direction)
        {
            case "Right":

                RaycastHit2D[] rightHit = Physics2D.RaycastAll(rightHitPoint.position, Vector2.right,3f,attackLayerMask);
                for (int i = 0; i < rightHit.Length; i++)
                {
                    if (rightHit[i].transform.CompareTag("Chair"))
                    {
                        rightHit[i].transform.GetComponent<Chair>().DestroyChair();
                    }
                }
                {
                    
                }
                break;
            case "Left":

                RaycastHit2D[] leftHit = Physics2D.RaycastAll(leftHitPoint.position, Vector2.left, 3f, attackLayerMask);
                for (int i = 0; i < leftHit.Length; i++)
                {
                    if (leftHit[i].transform.CompareTag("Chair"))
                    {
                        leftHit[i].transform.GetComponent<Chair>().DestroyChair();
                    }
                }

                break;

            case "Up":

                RaycastHit2D[] upHit = Physics2D.RaycastAll(upHitPoint.position, Vector2.up, 3f, attackLayerMask);
                for (int i = 0; i < upHit.Length; i++)
                {
                    if (upHit[i].transform.CompareTag("Chair"))
                    {
                        upHit[i].transform.GetComponent<Chair>().DestroyChair();
                    }
                }

                break;

            case "Down":

                RaycastHit2D[] downHit = Physics2D.RaycastAll(downHitPoint.position, Vector2.up, 3f, attackLayerMask);
                for (int i = 0; i < downHit.Length; i++)
                {
                    if (downHit[i].transform.CompareTag("Chair"))
                    {
                        downHit[i].transform.GetComponent<Chair>().DestroyChair();
                    }

                    if (downHit[i].transform.CompareTag("Table"))
                    {
                        downHit[i].transform.GetComponent<TableFlip>().FlipTable();
                    }
                }

                break;
        }
    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
        AudioManager.instance.PlaySound("sword_swing");

        switch (playerMovement.direction)
        {
            case "Right":

                Collider2D[] rightHitColliders = Physics2D.OverlapCircleAll(rightHitPoint.position, attackRadius, attackLayerMask);
                CheckEnemy(rightHitColliders);

                break;
            case "Left":

                Collider2D[] leftHitColliders = Physics2D.OverlapCircleAll(leftHitPoint.position, attackRadius, attackLayerMask);
                CheckEnemy(leftHitColliders);

                break;

            case "Up":

                Collider2D[] upHitColliders = Physics2D.OverlapCircleAll(upHitPoint.position, attackRadius, attackLayerMask);
                CheckEnemy(upHitColliders);

                break;

            case "Down":

                Collider2D[] downHitColliders = Physics2D.OverlapCircleAll(downHitPoint.position, attackRadius, attackLayerMask);
                CheckEnemy(downHitColliders);

                break;
        }

        Invoke("UnblockAttack",perAttackDuration);
        hasAttacked = true;
    }

    public void CheckEnemy(Collider2D[] hitColliders)
    {
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].transform.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hitColliders[i].transform.GetComponent<EnemyHealth>();

                enemyHealth.TakeDamage(attackDamage, playerMovement.direction);
            }
            if (hitColliders[i].transform.CompareTag("Boss"))
            {
                BossHealth bossHealth = hitColliders[i].transform.GetComponent<BossHealth>();

                bossHealth.TakeDamage(attackDamage, playerMovement.direction);
            }
            if (hitColliders[i].transform.CompareTag("Chair"))
            {
                Chair chair = hitColliders[i].transform.GetComponent<Chair>();

                chair.DestroyChair();
            }

            if (hitColliders[i].transform.CompareTag("Table"))
            {
                hitColliders[i].transform.GetComponent<TableFlip>().FlipTable();
            }
        }

        for (int i = 0; i < enemiesBehindPlayer.Count; i++)
        {
            if (!hitColliders.Contains(enemiesBehindPlayer[i]))
            {
                if (enemiesBehindPlayer[i].transform.CompareTag("Boss"))
                {
                    BossHealth bossHealth = enemiesBehindPlayer[i].transform.GetComponent<BossHealth>();

                    bossHealth.TakeDamage(attackDamage, playerMovement.direction);
                    continue;
                }

                EnemyHealth enemyHealth = enemiesBehindPlayer[i].transform.GetComponent<EnemyHealth>();

                enemyHealth.TakeDamage(attackDamage, playerMovement.direction);
            }
        }


    }
    private void UnblockAttack()
    {
        hasAttacked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Boss")) && !enemiesBehindPlayer.Contains(collision))
        {
            enemiesBehindPlayer.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Boss")) && enemiesBehindPlayer.Contains(collision))
        {
            enemiesBehindPlayer.Remove(collision);
        }
    }
}
