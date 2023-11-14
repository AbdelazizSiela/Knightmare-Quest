using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class BossCommand : MonoBehaviour
{
    private TextMeshPro myText;

    [SerializeField] private string[] commands;
    [SerializeField] private GameObject destroyParticle;

    private void Start()
    {
        Destroy(gameObject, 10f);
        myText = GetComponent<TextMeshPro>();

        SetRandomColor();
        SetRandomText();
        Invoke("SetColliderSize", 0.1f);
    }
    private void SetColliderSize()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Vector2 textSize = myText.GetRenderedValues();

        boxCollider.size = textSize;
    }
    private void SetRandomColor()
    {
        myText.color = new Color(Random.Range(0, 256) / 256f,
   Random.Range(0, 256) / 256f,
   Random.Range(0, 256) / 256f);
    }
    private void SetRandomText()
    {
        int randomCommandIndex = Random.Range(0, commands.Length);
        myText.text = commands[randomCommandIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("FlippedTable"))
        {
            BreakCommand();
        }
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerMovement>().canDash) return;

            collision.GetComponent<PlayerHealth>().TakeDamage(25);
            BreakCommand();
        }
    }

    private void BreakCommand()
    {
        GameObject effect = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        effect.GetComponent<ParticleSystem>().startColor = myText.color;
        Destroy(gameObject);
    }
}
