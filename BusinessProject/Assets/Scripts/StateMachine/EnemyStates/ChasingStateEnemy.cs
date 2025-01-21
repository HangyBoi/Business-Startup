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
        moveBehaviour.TargetReached += StopChase;
        targetDetector.TargetUndetected += StopChase;
        targetDetector.TargetInAttackRange += StartAttack;
        Debug.Log("Chasing");
    }

    public override void Handle()
    {
        if(moveBehaviour && targetDetector) moveBehaviour.SetTargetPosition(targetDetector.GetTarget().transform.position);
    }
    public override void OnExitState()
    {
        moveBehaviour.TargetReached -= StopChase;
        targetDetector.TargetUndetected -= StopChase;
        targetDetector.TargetInAttackRange -= StartAttack;
    }
    
    private void StopChase()
    {
        if(SM) SM.TransitToState(GetComponent<RoamingStateEnemy>());
    }

    private void StartAttack()
    {
        if(SM) SM.TransitToState(GetComponent<AttackStateEnemy>());
    }
}
