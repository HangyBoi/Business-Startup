using System;
using UnityEngine;

public class IdleStateEnemy : IdleState
{
    public override void OnEnterState()
    {
        Debug.Log("Idle");
        SM.TransitToState(GetComponent<RoamingStateEnemy>());
    }

    public override void Handle() {}

    public override void OnExitState()
    {
    }
    
}