using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Dictionary<Item, int> ItemAmounts = new Dictionary<Item, int>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;
    public InventoryItemController[] InventoryItems;
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        if (ItemAmounts.TryGetValue(item, out int amount))
        {
            ItemAmounts[item]++;
        }
        else
        {
            Items.Add(item);
            ItemAmounts.Add(item, 1);

        }
        /*
        //int containsIndex = Contains(item);
        if (containsIndex == -1)
        {
            Items.Add(item);
            //UpdateNumber(item, 1);

        }
        else
        {
            
            //UpdateNumber(Items[containsIndex], 1);

        }
        */
        PrintInventory();
    }

    //returns index if item already exists, returns -1 if not
    public int Contains(Item item)
    {
        int index = 0;
        foreach (Item i in Items)
        {
            if (i == item)
            {
                return index;
            }
            index++;

        }

        return -1;
    }

    /*
    public void UpdateNumber(Item item, int num)
    {
        item.number = item.number + num;
    }
    */
    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    

    public void ListItem()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            //Debug.Log("Picking up " + Item.name);


            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemNumber = obj.transform.Find("ItemNumber").GetComponent<Text>();

            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            //Debug.Log(itemName);
            //Debug.Log(itemIcon);
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemNumber.text = "x" + ItemAmounts[item].ToString();

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }

        }
        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);

        }

    }

    public void PrintInventory()
    {
        foreach (Item i in Items)
        {
            Debug.Log(i.itemName + " has " + ItemAmounts[i]);

        }
    }
}

