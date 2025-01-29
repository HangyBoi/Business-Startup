using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auka : MonoBehaviour
{
    [SerializeField] private Transform observer;
    [SerializeField] private GameObject aukaEmptyPrefab;
    [Range(1f, 20f)][SerializeField] private float detectionRange;
    private GameObject aukaBody;
    private GameObject aukaLeaf;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (!observer) Debug.Log("Auka has no observer assigned!");
        if (!aukaBody) aukaBody = GameObject.Find("AukaSprite");
        if (!aukaLeaf) aukaLeaf = GameObject.Find("AukaLeafSprite");
        if (aukaLeaf) aukaLeaf.SetActive(false);

        GetComponent<PickupInteractable>().PickupDestroyed += InstantiateLog;
    }

    private void OnDisable()
    {
        GetComponent<PickupInteractable>().PickupDestroyed -= InstantiateLog;
    }

    void Update()
    {
        if (observer) LookForObserver();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void LookForObserver()
    {
        if (aukaBody && aukaLeaf)
        {
            float distance = Vector2.Distance(new Vector2(observer.position.x, observer.position.z),
                new Vector2(transform.position.x, transform.position.z));
            bool isClose = (distance <= detectionRange);
            animator.SetBool("IsClose", isClose);
        }
    }

    private void InstantiateLog()
    {
        if (aukaEmptyPrefab) Instantiate(aukaEmptyPrefab, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Instantiate empty auka.");
    }
}
