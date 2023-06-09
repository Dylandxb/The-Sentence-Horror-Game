using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatController : MonoBehaviour
{
    Vector3 target;
    float speed = 1.0f;

    
    void Start()
    {
        SetTarget(new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z));
    }

    void Update()
    {
        Vector3 direction = target - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime, Space.World);            //Move in the specified direction within the world space
    }
                
    //Walk towards a target, defined by a vector3
    void SetTarget(Vector3 setTarget)
    {
        target = setTarget;
        transform.LookAt(target);

    }
}


//Patrolling AI, set trigger to trigger animations at certain waypoints
//Turn AI 180 degrees when hitting a waypooint directly infront, or turn left make it random 
//Coroutine, set speed to 0,trigger animation e.g turn left, wait for anim to finish, set speed back to 1.0
//After you wait for coroutine seconds, change to startwalking animation, wait for 8 frames and change movement speed to 1
//Given the current index, set a Trigger for the animation, e.g setTrigger("leftAnim")