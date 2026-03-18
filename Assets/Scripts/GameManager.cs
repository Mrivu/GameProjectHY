using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDialogue()
    {
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.StartDialogueAnimation();
    }

    void EndDialogue()
    {
        dialogueSystem.gameObject.SetActive(false);
    }
}
