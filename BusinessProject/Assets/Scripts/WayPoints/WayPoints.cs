using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public List<WayPoint> wayPoints { get; private set; }

    void Awake()
    {
        wayPoints = GetComponentsInChildren<WayPoint>().ToList();
        //Debug.Log(wayPoints.Count);
    }

    public void ThrowWayPointToEnd(WayPoint p)
    {
        wayPoints.Remove(p);
        wayPoints.Add(p);
    }
}
