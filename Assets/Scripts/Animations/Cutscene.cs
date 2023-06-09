using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject wardensCam;
    public GameObject visitorsCam;
    public GameObject cellCam;
    public GameObject crossHair;
    public GameObject swat;
    private AudioSource cutsceneAudio;
    
    
    
    void Start()
    {
        cutsceneAudio = GetComponent<AudioSource>();
    }

    public IEnumerator CamSequence()
    {
        //Disable select game objects and play cutscene audio
        swat.SetActive(false);
        cutsceneAudio.Play();
        crossHair.SetActive(false);

        playerCam.SetActive(false);
        //Wait 'x' amount of seconds before disabling the current camera and swapping to the next
       // yield return new WaitForSeconds(1.0f);
        wardensCam.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        visitorsCam.SetActive(true);
        wardensCam.SetActive(false);


        yield return new WaitForSeconds(3.0f);
        cellCam.SetActive(true);
        visitorsCam.SetActive(false);
        



    }
    
}
