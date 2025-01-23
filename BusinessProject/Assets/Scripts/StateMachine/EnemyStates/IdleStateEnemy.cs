using System;
using UnityEngine;

public class IdleStateEnemy : IdleState
{
    public override void OnEnterState()
    {
        Debug.Log("Enemy is Idling");
        SM.TransitToState(GetComponent<RoamingStateEnemy>());
    }

    public override void Handle() { }

    public override void OnExitState()
    {
    }

}