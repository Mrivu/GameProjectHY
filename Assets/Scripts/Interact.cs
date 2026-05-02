using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour, IPointerClickHandler
{
    [Header("Dialogue")]
    private DialogueSystem dialogueSystem;
    public List<int> conversations = new List<int>() { -1 };
    public int conversationIndex = 0;

    public bool oneTime = false;

    public ParticleSystem clueParticle;

    void Update()
    {
        if (InputControls.Instance.clue.WasPressedThisFrame() && !GameManager.Instance.dialogueSystem.dialogueRunning && !GameManager.Instance.pauseManager.gamePaused)
        {
            clueParticle.Play();
        }
        if (InputControls.Instance.clue.WasReleasedThisFrame() || GameManager.Instance.dialogueSystem.dialogueRunning || GameManager.Instance.pauseManager.gamePaused)
        {
            clueParticle.Stop();
        }
    }

    void Start()
    {
        clueParticle.Stop();
        dialogueSystem = GameManager.Instance.dialogueSystem;
    }

    void OnEnable()
    {
        clueParticle.Stop();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        conversationIndex++;

        // Text and exceptions
        if (!dialogueSystem.dialogueRunning && !GameManager.Instance.pauseManager.gamePaused)
        {
            dialogueSystem.gameObject.SetActive(true);

            // Interacting with the door before picking up the gourd
            if (conversations[conversationIndex % conversations.Count] == 4 && InteractExceptions.Instance.pickedUpGourd == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 3);
            }

            // Talking to Toyotomi more than once
            else if (conversations[conversationIndex % conversations.Count] == 23 && InteractExceptions.Instance.talkedToyotomi == true)
            {
                dialogueSystem.StartDialogueAnimation(true, 26);
            }

            // Interacting with the entrance to the 3rd before Hideyoshi has been talked to
            else if (conversations[conversationIndex % conversations.Count] == 30 && InteractExceptions.Instance.talkedToyotomi == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 22);
            }

            // Interacting with the sign post in order to go to the shrine after picking up the Kagura suzu in Scene 3
            else if (conversations[conversationIndex % conversations.Count] == 32 && InteractExceptions.Instance.talkedToyotomi == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 22);
            }

            // Interacting with the sign post in order to go to the shrine after picking up the Kagura suzu in Scene 3
            else if (conversations[conversationIndex % conversations.Count] == 32 && InteractExceptions.Instance.pickedUpKaguraSuzu == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 27);
            }

            // Interacting with the Kagura suzu before interacting with the Rogue
            else if (conversations[conversationIndex % conversations.Count] == 57 && InteractExceptions.Instance.talkedRogue == false)
            {
                dialogueSystem.StartDialogueAnimation(true, 52);
            }

            // Talking to Toyotomi more than once
            else if (conversations[conversationIndex % conversations.Count] == 53 && InteractExceptions.Instance.talkedRogue == true)
            {
                dialogueSystem.StartDialogueAnimation(true, 56);
            }

            else
            {
                dialogueSystem.StartDialogueAnimation(true, conversations[conversationIndex % conversations.Count]);

                if (oneTime)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
