using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingStateEnemy : State
{
    private MoveBehaviour moveBehaviour;
    private TargetDetector targetDetector;
    private void Start()
    {
        if (GetComponent<TargetDetector>()) targetDetector = GetComponent<TargetDetector>();
        else
        {
            Debug.LogAssertion("No target detector attached to enemy!");
        }
        if (GetComponent<MoveBehaviour>()) moveBehaviour = GetComponent<MoveBehaviour>();
        else
        {
            Debug.LogAssertion("No move behaviour attached to enemy!");
        }
    }

    public override void OnEnterState()
    {
        if(moveBehaviour && targetDetector) moveBehaviour.SetTargetPosition(targetDetector.GetTarget().transform.position);
        MoveBehaviour.TargetReached += StopMove;
        Debug.Log("Chasing");
    }

    public override void Handle()
    {
    }
    public override void OnExitState()
    {
        MoveBehaviour.TargetReached -= StopMove;
    }
    
    private void StopMove(GameObject obj)
    {
        if(SM && obj == gameObject) SM.TransitToState(GetComponent<IdleStateEnemy>());
    }
}
