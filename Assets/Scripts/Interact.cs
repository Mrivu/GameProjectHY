using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour, IPointerClickHandler
{
    private DialogueSystem dialogueSystem;
    public List<int> conversations = new List<int>() { -1 };
    public int conversationIndex = 0;

    void Awake()
    {
        dialogueSystem = GameManager.Instance.dialogueSystem;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!dialogueSystem.dialogueRunning)
        {
            dialogueSystem.gameObject.SetActive(true);
            dialogueSystem.StartDialogueAnimation(true, conversations[conversationIndex % conversations.Count]);
            conversationIndex++;
        }
    }

    
}
