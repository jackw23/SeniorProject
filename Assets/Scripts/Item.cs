using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SOURCE CODE HEAVILY INSPIRED BY: https://github.com/Brackeys/RPG-Tutorial/blob/master/Finished%20Project/Assets/Scripts/Items/Item.cs
// USE https://www.youtube.com/watch?v=AoD_F1fSFFg for clicking to pick up item bc rn you need it to showcase that it works
[CreateAssetMenu (fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public int value;
    public string itemName = "new item"; // using new keyword hides the object.name??
    public Sprite icon;
    public ItemType itemType;
   public enum ItemType
    {
        HealthPotion,
        UpgradePoints,
        Key
    }
}
