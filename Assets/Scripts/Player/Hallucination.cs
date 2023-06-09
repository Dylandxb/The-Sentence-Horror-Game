using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallucination : MonoBehaviour
{

    public static Hallucination instance;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

    }

    public void HallucinationTrigger()
    {

    }
    //Function to spawn white haze/cloudy sense in player view
    //Increment the number of spots per stress level above 5
    //Coroutine to add spots every few seconds
    //Camera Fade in and Out to signal disorientation
    //Heavy blinking feature
    //Camera shake
}
