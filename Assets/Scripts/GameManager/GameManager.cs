using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public PauseManager pauseManager;
    public DialogueSystem dialogueSystem;
    public Inventory inventory;

    public AudioManager audioManager;

    public Settings settings;

    public GameObject gameCanvas;
    public VideoPlayer videoPlayer;

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

    public void StartIntro()
    {
        gameCanvas.SetActive(false);
        videoPlayer.clip = null;
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
        pauseManager.canTogglePause = false;
        gameCanvas = GameObject.Find("Canvas");

        if (scene.name == "MainMenu")
        {
            videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        }

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

            pauseManager.canTogglePause = true;
        }

        audioManager.PlayMusic(scene.name);
    }
}