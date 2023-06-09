using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPickUp : MonoBehaviour
{
    public static bool pickedUpMap;
    public Button mapButton;
    private void Update()
    {
        if (PlayerCast.instance.CheckForObject(gameObject))
        {
            //Shows interactable text on the screen.
            InteractableText.instance.ShowInteractableText("E", "pick up map");

            if(Input.GetKeyDown(KeyCode.E))
            {
                //Plays pick up animation.
                HandAnimation.instance.PickUpAnim();

                //Changes picked up map to true, allows player to interact with the map button in pause menu and destroys the holder.
                pickedUpMap = true;
                mapButton.interactable = true;
                Destroy(gameObject);
            }
        }
    }
}
