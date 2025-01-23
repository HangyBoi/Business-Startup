using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float detectionRange = 2;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float unDetectionRangeMultiplier = 2f;
    
    public System.Action TargetDetected;
    public System.Action TargetInAttackRange;
    public System.Action TargetUndetected;
    void Start()
    {
        if(!target) Debug.LogAssertion("No target set in target detector!");
    }
    
    void Update()
    {
        if(target) LookForTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void LookForTarget()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 tarPos = new Vector2(target.transform.position.x, target.transform.position.z);
        if (Vector2.Distance(tarPos, pos) <= detectionRange)
        {
            TargetDetected?.Invoke();
        }
        if (Vector2.Distance(tarPos, pos) >= detectionRange * unDetectionRangeMultiplier)
        {
            TargetUndetected?.Invoke();
        }

        if (Vector2.Distance(tarPos, pos) <= attackRange)
        {
            TargetInAttackRange?.Invoke();
        }
    }

    public GameObject GetTarget()
    {
        if (target) return target;
        else
        {
            Debug.Log("target is null");
            return null;
        }
    }
}
