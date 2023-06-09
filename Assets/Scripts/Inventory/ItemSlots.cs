using System.Collections;
using UnityEngine;

[System.Serializable]
public class ItemSlots
{
    [SerializeField] private Item item;
    [SerializeField] private int itemQuantity;

    public ItemSlots()
    {
        item = null;
        itemQuantity = 0;
    }

    public ItemSlots(Item _item, int _itemQuantity)
    {
        item = _item;
        itemQuantity = _itemQuantity;
    }

    public Item GetItem() { return item; }
    public int GetQuantity() { return itemQuantity; }
    public void AddQuantity(int _quantity) { itemQuantity += _quantity; }
    public void SubQuantity(int _quantity) { itemQuantity -= _quantity; }
}
