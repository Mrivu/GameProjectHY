using System.Collections.Generic;
using UnityEngine;

public class InteractExceptions : MonoBehaviour
{
    public static InteractExceptions Instance { get; private set; }

    public int endingPoints = 0;

    public bool pickedUpGourd = false;
    public bool talkedToyotomi = false;
    public bool pickedUpKaguraSuzu = false;
    public bool talkedRogue = false;


    public bool firstTimeHouseOutside = true;
    public bool firstTimeAbandonedHouse = true;

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

        if (id == 53)
        {
            talkedRogue = true;
        }


        if (id == 21)
        {
            firstTimeHouseOutside = false;
        }

        if (id == 51)
        {
            firstTimeAbandonedHouse = false;
        }
    }
}
