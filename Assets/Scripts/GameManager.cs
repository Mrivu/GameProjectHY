using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        dialogueSystem.gameObject.SetActive(true);
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dialogueSystem.gameObject.SetActive(false);
    }
}