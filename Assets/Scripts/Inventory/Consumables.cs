using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Type", menuName = "Item/Consumables")]
public class Consumables : Item
{
    [Header("Consumables")]
    public float decreaseStress; //Place-holder;
    public ConsumableType consumableType;

    public enum ConsumableType
    {
        SANITY_PILLS,
        MARIHUANA,
        COCAINE
    }

    public override Item GetItem() { return this; }
    public override Consumables GetConsumables() { return this; }
    public override Keys GetKeys() { return null; }
    public override QuestItem GetQuestItem() { return null; }
}
