using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auka : MonoBehaviour
{
    [SerializeField] private Transform observer;
    [Range(1f, 20f)] [SerializeField] private float detectionRange;
    private GameObject aukaBody;
    private GameObject aukaLeaf;
    void Start()
    {
        if(!observer) Debug.Log("Auka has no observer assigned!");
        if(!aukaBody) aukaBody = GameObject.Find("AukaSprite");
        if(!aukaLeaf) aukaLeaf = GameObject.Find("AukaLeafSprite");
        if(aukaLeaf) aukaLeaf.SetActive(false);
    }
    
    void Update()
    {
        if(observer) LookForObserver();
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
            if (distance <= detectionRange)
            {
                aukaBody.SetActive(false);
                aukaLeaf.SetActive(true);
            }
            else
            {
                aukaBody.SetActive(true);
                aukaLeaf.SetActive(false);
            }
        }
    }
}
