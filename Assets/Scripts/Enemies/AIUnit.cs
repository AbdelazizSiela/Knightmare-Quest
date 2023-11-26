using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class AIUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    private bool isMoving;

    private Transform player;

    private float horizonalMovement;
    private float verticalMovement;
    private float idleHorizonalDirection;
    private float idleVerticalDirection;

    private Vector2 previousPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        AIManager.Instance.Units.Add(this);
        player = GameObject.Find("Player").transform;

        previousPosition = transform.position;

    }

    public void Update()
    {
        Vector2 direction = (Vector2)transform.position - previousPosition;

        previousPosition = transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                horizonalMovement = 1;
                verticalMovement = 0;
            }
            else if (direction.x < 0)
            {
                horizonalMovement = -1;
                verticalMovement = 0;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                verticalMovement = 1;
                horizonalMovement = 0;
            }
            else if (direction.y < 0)
            {
                verticalMovement = -1;
                horizonalMovement = 0;
            }
        }

        Vector3 playerToEnemy = transform.position - player.position;

        float rightDot = Vector3.Dot(player.right, playerToEnemy);
        float upDot = Vector3.Dot(player.up, playerToEnemy);

        if (rightDot < 0)
        {
            idleHorizonalDirection = 1;
        }
        else
        {
            idleHorizonalDirection = -1;
        }

        if (upDot < 0)
        {
            idleVerticalDirection = 1;
        }
        else
        {
            idleVerticalDirection = -1;
        }

        if(Mathf.Abs(rightDot) > Mathf.Abs(upDot))
        {
            idleVerticalDirection = 0;
        }
        else
        {
            idleHorizonalDirection = 0;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        anim.SetBool("isMoving", isMoving);

        anim.SetFloat("horizontalMovement", horizonalMovement);
        anim.SetFloat("verticalMovement", verticalMovement);

        anim.SetFloat("idleHorizonalDirection", idleHorizonalDirection);
        anim.SetFloat("idleVerticalDirection", idleVerticalDirection);
    }
    private void OnDestroy()
    {
        AIManager.Instance.Units.Remove(this);
    }
    public void MoveTo(Vector3 pos)
    {
        if (!this.enabled) return;

        agent.SetDestination(pos);
        transform.rotation = Quaternion.identity;
    }
}
