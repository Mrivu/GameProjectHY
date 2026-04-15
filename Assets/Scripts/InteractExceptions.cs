using System.Collections.Generic;
using UnityEngine;

public class InteractExceptions : MonoBehaviour
{
    public static InteractExceptions Instance { get; private set; }

    public bool pickedUpGourd = false;
    public bool talkedToyotomi = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
