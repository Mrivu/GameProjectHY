using System.Collections.Generic;
using UnityEngine;

public static class Items
{
    public static Item healingGourd = new Item(0, "Healing Gourd", Resources.Load<Sprite>("UI/Items/Gourd03"));
    public static Item bell = new Item(1, "Bell", Resources.Load<Sprite>("UI/Items/bell"));

    public static Dictionary<string, Item> items = new Dictionary<string, Item>()
    {
        {healingGourd.name, healingGourd },
        {bell.name, bell },
    };
}

[System.Serializable]
public class Item
{
    public int itemID;
    public string name;
    public Sprite itemImage;

    public Item(int id, string name, Sprite itemImage)
    {
        this.itemID = id;
        this.name = name;
        this.itemImage = itemImage;

    }
}
