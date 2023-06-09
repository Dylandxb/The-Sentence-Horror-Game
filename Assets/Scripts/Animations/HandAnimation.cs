using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    public static HandAnimation instance;

    private Animator anim;
    public GameObject flashLight;
    public Flashlight flashLightScript;             
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        anim = gameObject.GetComponent<Animator>();
        
    }

    public void PickUpAnim()
    {
        //Trigger equip animation as normal
        anim.SetTrigger("Equip");
        //Accesses flashlight script and checks if holding item is active
        if (flashLightScript.holdingItem.activeSelf)        
        {
            anim.SetTrigger("Equip");
            //Sets the flashlight game obejct to false temporarily during anim interval
            flashLight.SetActive(false);
            //Coroutine to respawn the game object and transition to holding animation
            StartCoroutine(RespawnFlashlight());           
        }
       
    }

    public void UseAnim()
    {
        anim.SetTrigger("Unequip");
        if (flashLightScript.holdingItem.activeSelf)
        {
            anim.SetTrigger("Unequip");
            flashLight.SetActive(false);
            StartCoroutine(RespawnFlashlight());
        }

    }

    public void DeathAnim()
    {
        anim.SetTrigger("DeathHallucination");
    }

    public void HoldItem()
    {
        anim.SetTrigger("FlashlightHold");
    }

    IEnumerator RespawnFlashlight()
    {
        yield return new WaitForSeconds(0.7f);
        HoldItem();
        flashLight.SetActive(true);
        
    }
}
