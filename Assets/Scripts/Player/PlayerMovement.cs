using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float timeBetweenDash = 1f;

    [Space(10)]
    [Header("Effects")]
    [SerializeField] private GameObject rightDashEffect;
    [SerializeField] private GameObject leftDashEffect;
    [SerializeField] private GameObject upDashEffect;
    [SerializeField] private GameObject downDashEffect;

    private Melee melee;

    private float horizonalMovement;
    private float verticalMovement;
    private float idleHorizonalDirection;
    private float idleVerticalDirection;

    private float speedFactor;

    [HideInInspector] public bool canDash;
    [HideInInspector] public bool isDashing;

    [HideInInspector] public string direction
    {
        get
        {
            string direction = "";
            switch(idleHorizonalDirection)
            {
                case -1:
                    direction = "Left";
                    break;
                case 1:
                    direction = "Right";
                    break;
            }
            switch (idleVerticalDirection)
            {
                case -1:
                    if(horizonalMovement == 0)
                    {
                        direction = "Down";
                    }
                    break;
                case 1:
                    if (horizonalMovement == 0)
                    {
                        direction = "Up";
                    }
                    break;
            }
            return direction;
        }
    }

    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        melee = GetComponent<Melee>();
        speedFactor = defaultSpeed;
        canDash = true;
    }

    private void Update()
    {
        DashMovement();
        AxialMovement();
        Animations();
    }
    private void FixedUpdate()
    {
        UpdateVelocity();
    }

    private void AxialMovement()
    {
        horizonalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (verticalMovement != 0)
        {
            idleVerticalDirection = verticalMovement;
            idleHorizonalDirection = 0;
        }
        if (horizonalMovement != 0)
        {
            idleHorizonalDirection = horizonalMovement;
            idleVerticalDirection = 0;
        }

    }
    private void DashMovement()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && (horizonalMovement != 0 || verticalMovement != 0))
        {
            isDashing = true;
            melee.DashAttack();
            speedFactor = dashSpeed;

            switch (direction)
            {
                case "Right":

                    leftDashEffect.SetActive(true);

                    break;

                case "Left":

                    rightDashEffect.SetActive(true);

                    break;

                case "Up":

                    downDashEffect.SetActive(true);

                    break;

                case "Down":

                    upDashEffect.SetActive(true);

                    break;
            }

            Invoke("RecoverDash", timeBetweenDash);
            Invoke("StopDash", 0.5f);
            canDash = false;
        }

    }
    private void StopDash()
    {
        isDashing = false;
    }
    private void RecoverDash()
    {
        rightDashEffect.SetActive(false);
        leftDashEffect.SetActive(false);
        upDashEffect.SetActive(false);
        downDashEffect.SetActive(false);

        canDash = true;
    }
    private void UpdateVelocity()
    {
        rb.velocity = new Vector2(horizonalMovement 
            , verticalMovement ).normalized;
        rb.velocity = rb.velocity * Time.fixedDeltaTime * speedFactor;

        speedFactor = defaultSpeed;
        
    }
    private void Animations()
    {
        bool isIdle = horizonalMovement == 0 && verticalMovement == 0;

        if(isIdle && anim.GetBool("isMoving"))
        {
            anim.SetBool("isMoving", false);
        }
        if(!isIdle && !anim.GetBool("isMoving"))
        {
            anim.SetBool("isMoving", true);
        }

        anim.SetFloat("horizontalMovement", horizonalMovement);
        anim.SetFloat("verticalMovement", verticalMovement);

        anim.SetFloat("idleHorizonalDirection", idleHorizonalDirection);
        anim.SetFloat("idleVerticalDirection", idleVerticalDirection);
    }

    public void StopPlayer()
    {
        horizonalMovement = 0;
        verticalMovement = 0;
        Animations();
        rb.velocity = Vector2.zero;

        this.enabled = false;
    }
}
