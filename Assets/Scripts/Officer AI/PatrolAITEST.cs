using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAITEST : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;
    public float radius; // radius for  cone vision
    [Range(0, 360)]
    public float angle; // angles for cone vision
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audio;
    public bool alreadyPlayed = false;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutive());
        audio = GetComponent<AudioSource>();
        SoundToPlay = audio.clip;
    }

    private IEnumerator FOVRoutive() 
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true) 
        {
            yield return wait;
            FieldOfViewCheck();
        }

       
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask); 

        if (rangeChecks.Length != 0) 
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) 
                {
                    agent.SetDestination(playerRef.transform.position);
                    float distanceToPlayer = Vector3.Distance(gameObject.transform.position, playerRef.transform.position);
                    if (distanceToPlayer <= 2f && !ImageFade.instance.isPlaying)
                    {
                        StartCoroutine(ImageFade.instance.PlayerTeleport());
                        
                    }
                    canSeePlayer = true;
                    if (!alreadyPlayed) 
                    {
                        audio.PlayOneShot(SoundToPlay, Volume);
                        alreadyPlayed = true;
                    }
                }
                    
                else
                {
                    canSeePlayer = false;
                    
                }
            }
            else 
            {
                canSeePlayer = false;
            }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1) // if the distance between AI and target is less than 1, iterate waypoint , then find position of current waypoint and update it
        {
            iterateWaypointIndex();
            UpdateDestination();
        }
        if (canSeePlayer == false) // if player hides or goes too far away, officer loses interest and goes back to patrolling
        {
            agent.SetDestination(target);
            alreadyPlayed = false;
        }
    }
    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position; // finds position of current waypoint
        agent.SetDestination(target); // set a new destination to target position
    }

    void iterateWaypointIndex() 
    {
        waypointIndex++;


        if (waypointIndex == waypoints.Length) 
        {
            waypointIndex = 0;
        }
    }
    
}
