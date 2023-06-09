using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject pickUpItem;
    public GameObject holdingItem;
    public bool active;
    public AudioSource flashlightAudio;

    private Light flashlight;
    private bool canChangeLight = true;

    
    private void Start()
    {
        flashlight = holdingItem.GetComponentInChildren<Light>();
        holdingItem.SetActive(false);
        flashlightAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Turns on and off the flashlight if player has it in hand. Changes light to be active whenever bool is true.
        if(Input.GetKeyDown(KeyCode.F) && holdingItem.activeSelf)
        {
            if(canChangeLight)
            {
                StartCoroutine(FlashlightSound());
            }
            //active = !active;
            //flashlightAudio.Play();
            //if(!flashlightAudio.isPlaying)
            //{
            //    flashlight.enabled = active;
            //}
        }
        //Checks if player is close to the flashlight object.
        if(PlayerCast.instance.CheckForObject(gameObject))
        {
            //Shows interactable text on the screen.
            InteractableText.instance.ShowInteractableText("E", "pick up flashlight");
            
            if(Input.GetKeyDown(KeyCode.E))
            {
                Destroy(pickUpItem);
                //Changes animation to hold the flashlight.
                HandAnimation.instance.HoldItem();
                holdingItem.SetActive(true);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                QuestManager.instance.questNumber++;
                //Change model of arms to one holding flashlight.
            }
        }
    }

    private IEnumerator FlashlightSound()
    {
        canChangeLight = false;
        active = !active;
        flashlightAudio.Play();
        yield return new WaitForSeconds(0.5f);
        flashlight.enabled = active;
        canChangeLight = true;
    }
}
