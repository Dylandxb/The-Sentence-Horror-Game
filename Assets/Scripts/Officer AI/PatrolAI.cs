using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolAI : MonoBehaviour
{
    NavMeshAgent  agent;
    public Transform[] waypoints;
    int wayPointIndex;

    Vector3 targetPos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 1)
            {
            IncreaseIndex();
            UpdateTargetPos();

            } 
    }

    void UpdateTargetPos()
    {
        targetPos = waypoints[wayPointIndex].position; //Gets pos of current waypoint
        agent.SetDestination(targetPos);
    }

    void IncreaseIndex()
    {
        wayPointIndex++;                        //Increment waypoint index

        if(wayPointIndex == waypoints.Length)   //If index is equal to the length
        {
            wayPointIndex = 0;                  //Set it back to 0 when you reach the last waypoint
        }
    }
}
