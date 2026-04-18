using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Image> slots = new List<Image>();
    public List<Item> inventoryItems = new List<Item>();

    void Awake()
    {
        foreach (var item in slots)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void FindSlots()
    {
        slots = new List<Image>();

        foreach (Transform child in GameObject.Find("Slots").transform)
        {
            slots.Add(child.gameObject.GetComponent<Image>());
            child.gameObject.SetActive(false);
        }

        int i = 0;
        foreach (Item item in inventoryItems)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].sprite = item.itemImage;
            i++;
        }
    }

    public void AddItem(string name)
    {
        if (inventoryItems.Count < slots.Count)
        {
            inventoryItems.Add(Items.items[name]);
            int imageID = inventoryItems.Count - 1;
            slots[imageID].sprite = Items.items[name].itemImage;
            slots[imageID].gameObject.SetActive(true);

            // Set state
            if (name == "Healing Gourd")
            {
                InteractExceptions.Instance.pickedUpGourd = true;
            }
        }
    }


}
