using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSystem : MonoBehaviour
{
    public Consumables consumables;
    public Keys keys;
    public QuestItem questItem;
    public int requiredItemID;
    private InventorySystem inventory;
    void Awake()
    {
        //Automatically sets up inventory system and player cast scripts to avoid null reference errors.
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventorySystem>();
       
    }

    void Update()
    {
        //Checks if the object is a key or consumable.
        if (consumables != null)
        {
            //Is player in range to pick up the item?
            if(PlayerCast.instance.CheckForObject(gameObject))
            {
                //Shows text to indicate player can pick up an item.
                InteractableText.instance.ShowInteractableText("E", "pick up consumable");
                //Turn on the UI to tell player, they can click { Button } to pick up the object. Set up the button as E for the time being, can change it any time.
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //Player picks up the object and it goes to inventory, destroying the object as it was picked up.
                    inventory.Add(consumables);
                    HandAnimation.instance.PickUpAnim();
                    Debug.Log("Got consumable item");
                    Destroy(gameObject);
                }
            }
        }
        if(keys != null)
        {
            //Is player in range to pick up the item?
            if(PlayerCast.instance.CheckForObject(gameObject))
            {
                //Shows text to indicate player can pick up an item.
                InteractableText.instance.ShowInteractableText("E", "pick up an ID Card");
                //Turn on the UI to tell player, they can click { Button } to pick up the object. Set up the button as E for the time being, can change it any time.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(keys.itemID == requiredItemID)
                    {
                        QuestManager.instance.questNumber++;
                    }
                    //Player picks up the object and it goes to inventory, destroying the object as it was picked up.
                    inventory.Add(keys);
                    HandAnimation.instance.PickUpAnim();
                    Debug.Log("Got key item");
                    Destroy(gameObject);
                }
            }
        }
        if(questItem != null)
        {
            //Is player in range to pick up the item?
            if (PlayerCast.instance.CheckForObject(gameObject))
            {
                //Shows text to indicate player can pick up an item.
                InteractableText.instance.ShowInteractableText("E", "pick up cutters");

                //Turn on the UI to tell player, they can click { Button } to pick up the object. Set up the button as E for the time being, can change it any time.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (questItem.itemID == requiredItemID)
                    {
                        QuestManager.instance.questNumber++;
                    }
                    //Player picks up the object and it goes to inventory, destroying the object as it was picked up.
                    inventory.Add(questItem);
                    HandAnimation.instance.PickUpAnim();
                    Debug.Log("Got quest item");
                    Destroy(gameObject);
                }
            }
        }
    }
}
