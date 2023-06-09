using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Keys keys;
    public Animator doorAnimation;
    private AudioSource doorAudio;
    //private PlayerCast playerCast;
    private InventorySystem inventory;
    private bool isOpen;

    public RequiredItem requiredItem;
    public enum RequiredItem
    {
        ID_CARD_LEVEL_0,
        ID_CARD_LEVEL_1,
        ID_CARD_LEVEL_2,
        NO_KEY
    }

    private void Awake()
    {
        //Automatically sets up inventory system and player cast scripts to avoid null reference errors.
        //playerCast = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCast>();
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventorySystem>();
        doorAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.E))
        {
            //Checks if the key type is the same one as required key type for the door. The Key and Door enums are required to be in the same position as we are checking the ints of enums not the type.
            //It is impossible to compare the enum types.
            if((int)keys.keyType == (int)requiredItem)
            {
                Debug.Log("Used Key, didn't remove yet.");
            }
        }*/
        
        if(PlayerCast.instance.CheckForObject(gameObject))
        {
            //Shows interactable text.
            InteractableText.instance.ShowInteractableText("E", "open the door");
            //Turn on UI showing player what to press to open the door.
            if(Input.GetKeyDown(KeyCode.E))
            {
                //When using Cell ID Card, the door animates to show it's open. Removing the card from inventory and updating the quest manager.
                if (requiredItem == RequiredItem.ID_CARD_LEVEL_0 && inventory.Remove(keys))
                {
                    HandAnimation.instance.UseAnim();
                    doorAnimation.Play("CellDoor");
                    doorAudio.Play();
                    //Set tag to untagged for players to remove usage text.
                    gameObject.tag = "Untagged";
                    Debug.Log("Opened Cell Door");
                    //Update Quest Manager List.
                    QuestManager.instance.questNumber++;
                }
                //If the door doesn't require any key, player can open it without a problem.
                if (requiredItem == RequiredItem.NO_KEY)
                {
                    doorAnimation.Play(doorAnimation.name);
                    //Calls use animation function which makes FPS arms do "Use" animation.
                    HandAnimation.instance.UseAnim();
                    gameObject.tag = "Untagged";
                    //isOpen = !isOpen;
                    //Plays Door Animation depending on the Door's state. True = Open, False = Closed;
                    //doorAnimation.SetBool("DoorAnimation", isOpen);
                    Debug.Log("Opened Door without key");
                }
                if (requiredItem == RequiredItem.ID_CARD_LEVEL_1 && inventory.Remove(keys))
                {
                    HandAnimation.instance.UseAnim();
                    doorAnimation.Play("GuardDoor");
                    doorAudio.Play();
                    //Set tag to untagged for players to remove usage text.
                    gameObject.tag = "Untagged";
                }

                ////If player has the correct key in the inventory, the door will open.
                //else if ((int)keys.keyType == (int)requiredItem && inventory.Remove(keys))
                //{
                //    //Calls use animation function which makes FPS arms do "Use" animation.
                //    HandAnimation.instance.UseAnim();
                //    Debug.Log("Opened the door and removed the key");
                //    //Changes enum for the door to no longer require a key to open it as the key was used and removed from inventory.
                //    requiredItem = RequiredItem.NO_KEY;
                //    //Either open and close the door with bool.
                //    isOpen = !isOpen;
                //    //Plays Door Animation depending on the Door's state. True = Open, False = Closed;
                //    //doorAnimation.SetBool("DoorAnimation", isOpen);
                //}
                else
                {
                    Debug.Log("Wrong key or no key in inventory");
                }
            }
            Debug.Log("Player is close to the door and is looking at it.");
        }
    }
}
