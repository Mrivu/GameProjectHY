using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Settings
{
    public FontAsset gameFont;
}

public class GameManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public Inventory inventory;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
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
        if (scene.name != "MainMenu")
        {
            inventory.FindSlots();
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
    }
}