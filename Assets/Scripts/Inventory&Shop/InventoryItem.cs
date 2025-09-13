using UnityEngine;
using System;


public class InventoryItem
{
    public ItemsData itemData;
    public int stackSize;

    public InventoryItem(ItemsData item)
    {
        itemData = item;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
