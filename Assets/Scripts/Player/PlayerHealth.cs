using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealthAmount = 100;
    public int currentHealthAmount;

    [SerializeField] private Sprite[] heartPieces;
    [SerializeField] private List<GameObject> hearts;

    public int currentHeartIndex;

    private bool playedAnimation;

    private void Start()
    {
        currentHeartIndex = hearts.Count - 1;
        currentHealthAmount = maxHealthAmount;
    }
    public void TakeDamage(int damage)
    {
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");

        currentHealthAmount -= damage;

        if(currentHealthAmount <= (maxHealthAmount / 2) && currentHealthAmount > 0)
        {
            if(!playedAnimation) hearts[currentHeartIndex].GetComponent<Animator>().SetTrigger("TakeDamage");
            playedAnimation = true;
        }
        if(currentHealthAmount <= 0)
        {
            if (playedAnimation) hearts[currentHeartIndex].GetComponent<Animator>().SetTrigger("TakeDamage");
            playedAnimation = false;

            if (currentHeartIndex > 0)
            {
                currentHeartIndex--;
                currentHealthAmount += maxHealthAmount;

                TakeDamage(0);

            }
            else
            {
                FindObjectOfType<GameOver>().EndGame();
            }
        }

        AudioManager.instance.PlaySound("Damage");


    }

    private void Update()
    {
       // if(Input.GetKeyDown(KeyCode.Escape)) TakeDamage(300);
    }
}
