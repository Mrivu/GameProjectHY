using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public PauseManager pauseManager;
    public DialogueSystem dialogueSystem;
    public Inventory inventory;

    public AudioManager audioManager;

    public Settings settings;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        settings.DefaultValues();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HouseInterior");
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        pauseManager.eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        if (scene.name == "HouseInterior")
        {
            dialogueSystem = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>();

            if (dialogueSystem != null)
            {
                // Dialogue at the start of the game
                dialogueSystem.gameObject.SetActive(true);
                dialogueSystem.StartDialogueAnimation(true, 0);
            }
        }

        if (scene.name == "HouseOutside")
        {
            dialogueSystem = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>();

            if (dialogueSystem != null)
            {
                // Dialogue at the start of the game
                dialogueSystem.gameObject.SetActive(true);
                dialogueSystem.StartDialogueAnimation(true, 5);
            }
        }


        if (scene.name != "MainMenu")
        {
            settings.UpdateAll();
            inventory.FindSlots();
        }
    }
}