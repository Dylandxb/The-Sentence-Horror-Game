using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable;

    public abstract Item GetItem();
    public abstract Consumables GetConsumables();
    public abstract Keys GetKeys();
    public abstract QuestItem GetQuestItem();
}
