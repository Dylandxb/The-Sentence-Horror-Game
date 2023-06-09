using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1.5f;
    public Animator anim;

    private int wayPointIndex;
    private float distanceFromPoint; //Check for the distance from 
    void Start()
    {
        wayPointIndex = 0;
        transform.LookAt(waypoints[wayPointIndex].position); //AI faces towards waypoint when moving forward
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        distanceFromPoint = Vector3.Distance(transform.position, waypoints[wayPointIndex].position); //Checks for distance between current position to the waypoints position in world space
        if (distanceFromPoint < 1.5f)
        {
            IncreaseIndex(); //Check if the AI is near to reaching a waypoint then call the function to increase index
        }
        PatrolMovement(); //Begin the next movement
    }

    void PatrolMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);          //Translate the object moving forward by a speed specified
    }

    void IncreaseIndex()
    {
        //anim.SetTrigger("Turn");
        wayPointIndex++; //Increment waypoint index
        if (wayPointIndex >= waypoints.Length) //If index is out of range
        {
            wayPointIndex = 0; //Reset the index back to 0, original waypoint if out of range
            anim.SetTrigger("Turn");
        }
        transform.LookAt(waypoints[wayPointIndex].position);
        if (wayPointIndex == 1)
        {
            anim.SetTrigger("Turn");
        }
        if (wayPointIndex == 2)
        {
            anim.SetTrigger("Turn");
        }
        if (wayPointIndex == 3)
        {
            anim.SetTrigger("Turn");
        }
        StartCoroutine(AnimChange());
        
    }

     IEnumerator AnimChange()
    {
        yield return new WaitForSeconds(1.0f);
        //Wait to trigger animation
        anim.SetTrigger("Walk");
    }
}
