using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeshlingAnimation : MonoBehaviour
{
    private StateMachine SM;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        SM = GetComponentInParent<StateMachine>();
        SM.OnEnterStateWithID += StartAnimation;

        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemyHealth.OnEnemyDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        SM.OnEnterStateWithID -= StartAnimation;
        enemyHealth.OnEnemyDeath -= PlayDeathAnimation;
    }

    void StartAnimation(string id)
    {
        if(id=="0") animator.Play("LeshlingIdleAnimation");
        if(id=="1") animator.Play("LeshlingWalkAnimation");
        if(id=="2") animator.Play("LeshlingWalkAnimation");
        if(id=="3") animator.Play("LeshlingIdleAnimation");
    }

    void PlayDeathAnimation()
    {
        animator.Play("LeshlingDeathAnimation");
    }
}
