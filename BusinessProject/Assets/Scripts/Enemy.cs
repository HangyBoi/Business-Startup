using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int speed = 2;
    [NonSerialized] public MoveBehaviour moveBehaviour;

    private void Start()
    {
        if (!GetComponent<MoveBehaviour>()) moveBehaviour = gameObject.AddComponent(typeof(MoveBehaviour)) as MoveBehaviour;
        else
        {
            moveBehaviour = GetComponent<MoveBehaviour>();
        }
        if (!moveBehaviour) Debug.Log("Enemy has no move behaviour!");
    }

    public void Spawn(Vector3 position)
    {
        gameObject.transform.position = position;
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
    public int GetSpeed()
    {
        return speed;
    }
}
