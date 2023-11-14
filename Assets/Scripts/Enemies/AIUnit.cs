using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class AIUnit : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AIManager.Instance.Units.Add(this);
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
