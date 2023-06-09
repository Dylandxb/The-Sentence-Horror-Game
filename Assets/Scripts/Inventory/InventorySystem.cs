using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject soltsHolder;
    //[SerializeField] private Item itemToAdd;
    //[SerializeField] private Item itemToRemove;

    public List<ItemSlots> items = new List<ItemSlots>();

    public GameObject[] slots;

    private void Start()
    {
        //Sets slots amount to be equal of how many there are gameobjects in the parent object.
        slots = new GameObject[soltsHolder.transform.childCount];

        for(int i = 0; i < soltsHolder.transform.childCount; i++)
            slots[i] = soltsHolder.transform.GetChild(i).gameObject;

        RefreshUI();

        //Add(itemToAdd);
        //Remove(itemToRemove);
    }

    public void RefreshUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            //Checks for the slots, if they do have items. If they do the images are enabled and sprites are changed.
            //If the items are stackable, it displays the counter of how many there are.
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;

                if(items[i].GetItem().isStackable)
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity().ToString();
                else
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";

            }
            //If there is no item in available slot, the sprite becomes null, and there's no counter for the items.
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }

        }
    }

    public bool Add(Item _item)
    {
        //Check if inventory contains item.
        ItemSlots slot = Contains(_item);

        //Adds +1 to quantity if the item is stackable.
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(1);
        }
        //Otherwise, adds the item to the free inventory slot.
        else
        {
            if (items.Count < slots.Length)
                items.Add(new ItemSlots(_item, 1));
            else
                return false;
        }

        RefreshUI();
        return true;
    }

    //Removes an item.
    public bool Remove(Item _item)
    {
        //Check if inventory contains item.
        ItemSlots temp = Contains(_item);

        if (temp != null)
        {
            //Checks if the quantity is more than 1, if it is, it subtracts the quantity by 1 before removing the item from the inventory.
            if(temp.GetQuantity() > 1)
            {
                temp.SubQuantity(1);
            }

            else
            {
                ItemSlots slot = new ItemSlots();

                foreach (ItemSlots slots in items)
                {
                    if (slots.GetItem() == _item)
                    {
                        slot = slots;
                        break;
                    }
                }
                items.Remove(slot);
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }

    //Checks if the item is in the inventory.
    public ItemSlots Contains(Item _item)
    {
        foreach (ItemSlots slot in items)
        {
            if (slot.GetItem() == _item)
                return slot;
        }

        return null;
    }
}