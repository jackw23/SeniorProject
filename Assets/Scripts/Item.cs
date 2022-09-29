using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SOURCE CODE HEAVILY INSPIRED BY: https://github.com/Brackeys/RPG-Tutorial/blob/master/Finished%20Project/Assets/Scripts/Items/Item.cs
// USE https://www.youtube.com/watch?v=AoD_F1fSFFg for clicking to pick up item bc rn you need it to showcase that it works
[CreateAssetMenu (fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int number = -1;
    public new string name = "new item"; // using new keyword hides the object.name??
    public Sprite icon = null;
    public bool showInInventory = false;

    public virtual void Use()
    {
        
    }

    public void RemoveFromInventory()
    {
        //Inventory.instance.Remove(this);
    }
}
