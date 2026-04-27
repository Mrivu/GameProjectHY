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
        // Text and exceptions
        if (!dialogueSystem.dialogueRunning && !GameManager.Instance.pauseManager.gamePaused)
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

            if (oneTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
