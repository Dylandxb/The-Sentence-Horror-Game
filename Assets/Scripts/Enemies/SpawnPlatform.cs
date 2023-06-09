using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public GhostAI Ghost; // connects ghost script with this cript

    void OnTriggerEnter(Collider other) // when Player collides with invisible platform, ghost renderer enables and appears in game.
    {
        if (other.gameObject.tag == "Player")
        {
            Ghost.ghostActivate.SetActive(true);
            Debug.Log("E");
        }

    }
}
