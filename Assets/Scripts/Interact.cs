using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour, IPointerClickHandler
{
    [Header("Dialogue")]
    private DialogueSystem dialogueSystem;
    public List<int> conversations = new List<int>() { -1 };
    public int conversationIndex = 0;

    [Header("Interacting")]
    public List<int> items = new List<int>();


    void Start()
    {
        dialogueSystem = GameManager.Instance.dialogueSystem;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Text and exceptions
        if (!dialogueSystem.dialogueRunning)
        {
            dialogueSystem.gameObject.SetActive(true);

            // Interacting with the door before picking up the gourd
            if (conversations[conversationIndex] == 4 && InteractExceptions.Instance.pickedUpGourd == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 3);
            }


            else
            {
                dialogueSystem.StartDialogueAnimation(true, conversations[conversationIndex % conversations.Count]);
            }

            conversationIndex++;
        }

        // Items and exceptions
        //if (items.Count > 0) 
        //{
            // Exceptions
       //     if (Items.items[items[0]].itemID == 0)
        //    {
       //         InteractExceptions.Instance.pickedUpGourd = true;
       //     }
        //    Inventory.Instance.AddItem(items[0]);
        //    items.Remove(0);

       // }
    }

    
}
