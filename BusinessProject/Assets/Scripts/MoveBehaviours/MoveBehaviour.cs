using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Animator))]
public abstract class MoveBehaviour : MonoBehaviour
{
    [SerializeField] protected float targetRange = 0.2f;
    protected Vector3 targetPos;
    public System.Action TargetReached;
    
    // protected virtual void Awake()
    // { }

    public virtual void SetTargetPosition(Vector3 position)
    {
        targetPos = position;
        //PlayMovingAnimation();
    }

    public void Stop()
    {
        SetTargetPosition(gameObject.transform.position);
        TargetReached?.Invoke();
    }

    private void Update()
    {
        Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector2 tarPos = new Vector2(targetPos.x, targetPos.z);
        
        if (Vector2.Distance(pos, tarPos) <= targetRange)
        { 
            if(gameObject) Stop();
        }
    }

    // #region "animation related fields"
    // [SerializeField]
    // private Animator mAnimator;
    // //If idling animation is already playing, don't play it again.
    // private bool idleAnimationPlaying = false;
    // //If moving animation is already playing, don't play it again.
    // private bool moveAnimationPlaying = false;
    // //Sub-classes can use the following functions to play animations accordingly
    // protected void PlayMovingAnimation()
    // {
    //     idleAnimationPlaying = false;
    //     if (!moveAnimationPlaying)
    //     { 
    //         mAnimator.Play("Move");
    //         moveAnimationPlaying = true;
    //     }
    // }
    // protected void PlayIdleAnimation()
    // {
    //     moveAnimationPlaying = false;
    //     if (!idleAnimationPlaying)
    //     {
    //         mAnimator.Play("Idle");
    //         idleAnimationPlaying = true;
    //     }
    // }
    // #endregion
}