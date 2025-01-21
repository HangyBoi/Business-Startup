using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateEnemy : State
{
    public override void OnEnterState()
    {
        Debug.Log("Attack");
        StartCoroutine(Attack());
    }

    public override void Handle()
    {
    }

    public override void OnExitState()
    {
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        if(SM) SM.TransitToState(GetComponent<ChasingStateEnemy>());
    }
}
