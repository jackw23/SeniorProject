using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Code heavily inspired by this tutorial: https://www.youtube.com/watch?v=AoD_F1fSFFg


public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InventoryManager Instance;
    public static int numUpgradeCoins;
    public List<Item> Items = new List<Item>();
    public Dictionary<Item, int> ItemAmounts = new Dictionary<Item, int>();
    public GameObject Player;
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
            //TMP_Text tmpugui; 
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            //Debug.Log("Picking up " + Item.name);

            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            
            // = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            //Debug.Log("Printing item name" + tmpugui);


            var itemNumber = obj.transform.Find("ItemNumber").GetComponent<Text>();

            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            //var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            //Debug.Log(itemName);
            //Debug.Log(itemIcon);
            itemName.text = item.itemName;
            obj.name = itemName.text;
            //Debug.Log("itemName.text" + tmpugui.text);

            itemIcon.sprite = item.icon;
            itemNumber.text = "x" + ItemAmounts[item].ToString();

            if (itemName.text == "Upgrade Coin")
            {
                obj.GetComponent<Button>().interactable = false;
            }

            /*if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }*/
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
            InventoryItems[i].setPlayer(Player);

        }

    }

    public void getNumCoins()
    {

    }

    public void PrintInventory()
    {
        foreach (Item i in Items)
        {
            Debug.Log(i.itemName + " has " + ItemAmounts[i]);

        }
    }
}

