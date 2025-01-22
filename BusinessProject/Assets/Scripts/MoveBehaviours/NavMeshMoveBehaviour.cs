using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveBehaviour : MoveBehaviour
{
    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        if (!GetComponent<NavMeshAgent>())
        {
            agent = gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.speed = gameObject.GetComponent<Enemy>().GetSpeed();
        agent.stoppingDistance = targetRange;
    }

    public override void SetTargetPosition(Vector3 position)
    {
        base.SetTargetPosition(position);
        if (agent != null)
        {
            agent.destination = position;
        }
        else
        {
            Debug.LogError("NavMeshAgent is not initialized!");
        }
    }
}