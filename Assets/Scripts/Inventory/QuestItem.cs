using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Item", menuName = "Item/Quest")]
public class QuestItem : Item
{
    [Header("Quest Item Type")]
    public QuestItemType questItemType;

    public enum QuestItemType
    {
        CUTTERS
    }

    public override Item GetItem() { return this; }
    public override Consumables GetConsumables() { return null; }
    public override Keys GetKeys() { return null; }
    public override QuestItem GetQuestItem() { return this; }
}
