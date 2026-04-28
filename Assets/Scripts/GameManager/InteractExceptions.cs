using System.Collections.Generic;
using UnityEngine;

public class InteractExceptions : MonoBehaviour
{
    public static InteractExceptions Instance { get; private set; }

    public int endingPoints = 0;

    public bool pickedUpGourd = false;
    public bool talkedToyotomi = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CheckDialogueState(int id)
    {
        if (id == 23)
        {
            talkedToyotomi = true;
        }
    }
}
