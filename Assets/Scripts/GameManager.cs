using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GameManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    void Start()
    {
        //TestDialogue();
    }

    void TestDialogue()
    {
        Invoke(nameof(StartDialogue), 1f);
        Invoke(nameof(StartDialogue), 5f);
        Invoke(nameof(EndDialogue), 7f);
        Invoke(nameof(StartDialogue), 9f);
        Invoke(nameof(StartDialogue), 11f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDialogue()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.StartDialogueAnimation(true);
    }

    void EndDialogue()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.StartDialogueAnimation(false);
    }
}
