using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private Animator hudAnimator;

    private EnemyHealth health;

    [SerializeField] private Image healthBar;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }
    public void TakeDamage(int damage,string hitFromDirection)
    {
        health.TakeDamage(damage, hitFromDirection);

        hudAnimator.SetTrigger("Damage");
        healthBar.fillAmount = health.healthAmount / 100f;
    }
}
