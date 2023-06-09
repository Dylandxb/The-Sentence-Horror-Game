using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public NavMeshAgent Ghost;
    public Transform Prisoner;
    public StressScript Player;
    public GameObject ghostActivate;
    public float rotateSpeed = 10.0f;
    void Update()
    {
        if (ghostActivate.activeSelf) 
        {
            Ghost.isStopped = false;
            Ghost.SetDestination(Player.transform.position);
        }
    }

    public void RotateGhost()
    {
        //Returns the vector3 of the difference between the player transform and ghost transform
        Vector3 direction = (Player.transform.position - Ghost.transform.position).normalized; 
        //Stores the lookRotation variable to face the Player with 'direction' as its parameter
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //Alters the transform rotation of the Ghost to rotate from 0 to its lookRotation value by a rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }
}
