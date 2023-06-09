using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key Type", menuName = "Item/Keys")]
public class Keys : Item
{
    [Header("Key Type")]
    public KeyType keyType;

    //Level 0 key opens cells
    //Level 1 key opens Guard's office
    //Level 2 key opens Warden's office
    public enum KeyType
    {
        ID_CARD_LEVEL_0,
        ID_CARD_LEVEL_1,
        ID_CARD_LEVEL_2
    }
    public override Item GetItem() { return this; }
    public override Consumables GetConsumables() { return null; }
    public override Keys GetKeys() { return this; }
    public override QuestItem GetQuestItem() { return null; }
}
