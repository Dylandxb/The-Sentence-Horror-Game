using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    public GhostAI ghostAI;

    private Light[] lights;

    private float timer;
    private float intensity;
    private AudioSource flickeringLightAudio;
    private void Start()
    {
        lights = gameObject.GetComponentsInChildren<Light>();
        timer = Random.Range(minTime, maxTime);
        flickeringLightAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //If the ghost is active in the scene, the lights will start to flicker.
        if (ghostAI.ghostActivate.activeSelf)
        {
            FlickerLights();
        }
        else
        {
            foreach (var light in lights)
            {
                light.enabled = true;
                light.intensity = 2.5f;
                //Disable audio when lights are not flickering
                flickeringLightAudio.Stop(); 
            }
        }

    }

    private void FlickerLights()
    {
        
        //Lowers the timer count.
        if (timer > 0)
            timer -= Time.deltaTime;

        //After timer hits 0, sets random intensity float which is applied to light's intensity.
        //Resets timer to a random number between minimum and maximum set times.
        if(timer <= 0)
        {
            intensity = Random.Range(0.1f, 1f);

            //Changes all the lights to flicker in random intensity. 
            foreach (var light in lights)
            {
                light.enabled = !light.enabled;
                light.intensity = intensity;
                flickeringLightAudio.Play();
                timer = Random.Range(minTime, maxTime);
            }
        }
    }

    
}
