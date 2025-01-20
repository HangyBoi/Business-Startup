using System;
using UnityEngine;

public class IdleStateEnemy : IdleState
{
    private TargetDetector targetDetector;
    private void Start()
    {
        if (GetComponent<TargetDetector>()) targetDetector = GetComponent<TargetDetector>();
        else
        {
            Debug.LogAssertion("No target detector attached to enemy!");
        }
    }

    public override void OnEnterState()
    {
        Start();
        targetDetector.TargetDetected += StartMove;
        Debug.Log("Idle");
    }

    public override void Handle() {}

    public override void OnExitState()
    {
        targetDetector.TargetDetected -= StartMove;
    }
    
    private void StartMove()
    {
        SM.TransitToState(GetComponent<ChasingStateEnemy>());
    }
    
}