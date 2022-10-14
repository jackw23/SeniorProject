using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Code heavily inspired by this tutorial: https://www.youtube.com/watch?v=AoD_F1fSFFg

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
