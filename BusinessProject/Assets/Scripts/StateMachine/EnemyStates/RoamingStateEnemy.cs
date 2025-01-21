using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingStateEnemy : State
{
    [SerializeField] private WayPoints wayPointsObj;
    private MoveBehaviour moveBehaviour;
    private TargetDetector targetDetector;
    private void Start()
    {
        if (GetComponent<TargetDetector>()) targetDetector = GetComponent<TargetDetector>();
        else
        {
            Debug.LogAssertion("No target detector attached to enemy!");
        }
        if (GetComponent<MoveBehaviour>()) moveBehaviour = GetComponent<MoveBehaviour>();
        else
        {
            Debug.LogAssertion("No move behaviour attached to enemy!");
        }
        if(!wayPointsObj) Debug.Log("no waypoints!");
        
        // targetDetector = GetComponent<TargetDetector>();
        // if (targetDetector == null)
        // {
        //     Debug.LogError("No TargetDetector attached to the enemy!");
        // }
        //
        // // Initialize MoveBehaviour
        // moveBehaviour = GetComponent<MoveBehaviour>();
        // if (moveBehaviour == null)
        // {
        //     Debug.LogError("No MoveBehaviour attached to the enemy!");
        // }
        //
        // // Check WayPoints assignment
        // if (!wayPointsObj)
        // {
        //     Debug.LogError("WayPoints object is not assigned!");
        // }
        // else if (wayPointsObj.wayPoints == null || wayPointsObj.wayPoints.Count == 0)
        // {
        //     Debug.LogError("WayPoints array is null or empty in WayPoints object!");
        // }
    }
    public override void OnEnterState()
    {
        Debug.Log("Roaming");
        targetDetector.TargetDetected += StartChase;
        moveBehaviour.TargetReached += GoToFirstWayPoint;
        GoToFirstWayPoint();
    }

    public override void Handle()
    {
    }

    public override void OnExitState()
    {
        targetDetector.TargetDetected -= StartChase;
        moveBehaviour.TargetReached -= GoToFirstWayPoint;
    }
    
    private void StartChase()
    {
        SM.TransitToState(GetComponent<ChasingStateEnemy>());
    }

    private void GoToFirstWayPoint()
    {
        // if (moveBehaviour == null)
        // {
        //     Debug.LogError("MoveBehaviour is null!");
        //     return;
        // }
        //
        // if (wayPointsObj == null)
        // {
        //     Debug.LogError("WayPointsObj is null!");
        //     return;
        // }
        //
        // if (wayPointsObj.wayPoints == null)
        // {
        //     Debug.LogError("WayPoints array is null!");
        //     return;
        // }
        //
        // if (wayPointsObj.wayPoints.Count == 0)
        // {
        //     Debug.LogError("WayPoints array is empty!");
        //     return;
        // }
        //
        // if (!wayPointsObj.wayPoints[0])
        // {
        //     Debug.LogError("First waypoint is null!");
        //     return;
        // }
        // if (!wayPointsObj.wayPoints[0].transform)
        // {
        //     Debug.LogError("First waypoint transform is null!");
        //     return;
        // }
        
        moveBehaviour.SetTargetPosition(wayPointsObj.wayPoints[0].transform.position);
        wayPointsObj.ThrowWayPointToEnd(wayPointsObj.wayPoints[0]);
    }
}
