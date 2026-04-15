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

    public bool oneTime = false;

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
            if (conversations[conversationIndex % conversations.Count] == 4 && InteractExceptions.Instance.pickedUpGourd == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 3);
            }
            else
            {
                dialogueSystem.StartDialogueAnimation(true, conversations[conversationIndex % conversations.Count]);
            }

            conversationIndex++;
        }
        if (oneTime)
        {
            gameObject.SetActive(false);
        }
    }
}
