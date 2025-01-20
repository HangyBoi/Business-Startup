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

    private bool executed = false;
    
    public System.Action TargetDetected; //static?
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
    }

    void LookForTarget()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= detectionRange && !executed)
        {
            TargetDetected?.Invoke();
            executed = true;
            Debug.Log("detected");
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
