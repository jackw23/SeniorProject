using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public Item item;
    public int amount;

    public InventoryItem(Item item)
    {
        this.item = item;
    }

    public void AddAmount(int amount)
    {
        this.amount = this.amount + amount;
    }

    public void RemoveAmount(int amount)
    {
        this.amount = this.amount - amount;
    }
}
