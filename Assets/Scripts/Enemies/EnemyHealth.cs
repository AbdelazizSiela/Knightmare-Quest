using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int healthAmount = 100;

    [Space(10)]
    [Header("Damage Feedback")]
    [SerializeField] private float knockbackForce = 100;
    [SerializeField] private float knockbackDuration = .25f;
    [SerializeField] private Vector2 knockbackOffset;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void TakeDamage(int damage,string hitFromDirection)
    {
        healthAmount -= damage;

        if(healthAmount <= 0 )
        {
            FindObjectOfType<PhrasesTrigger>().OnEnemyKilled();
            GetComponent<EnemyMelee>().enabled = false;
            GetComponent<AIUnit>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Dead");
            return;
        }

        DamageFeedback(hitFromDirection);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
   private void DamageFeedback(string hitFromDirection)
   {
        DamageKnockback(hitFromDirection);
   }

    private void DamageKnockback(string hitFromDirection)
    {
        switch (hitFromDirection)
        {
            case "Right":

                anim.SetFloat("horizontalDirection", 1);

                break;

            case "Left":

                anim.SetFloat("horizontalDirection", -1);

                break;

            case "Up":

                anim.SetFloat("verticalDirection", 1);

                break;

            case "Down":

                anim.SetFloat("verticalDirection", -1);

                break;
        }

        anim.SetTrigger("TakeDamage");
    }

}
