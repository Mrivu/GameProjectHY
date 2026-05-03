using UnityEngine;

public class InteractExceptions : MonoBehaviour
{
    public static InteractExceptions Instance { get; private set; }

    public int endingPoints = 0;

    public bool pickedUpGourd = false;
    public bool talkedToyotomi = false;
    public bool pickedUpKaguraSuzu = false;
    public bool talkedRogue = false;
    public bool talkedMiko = false;
    public bool talkedToyotomiAgain = false;

    public bool firstTimeHouseInterior = true;
    public bool firstTimeHouseOutside = true;
    public bool firstTimeAbandonedHouse = true;
    public bool firstTimeShrine = true;

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

        if (id == 31)
        {
            talkedToyotomiAgain = true;
        }

        if (id == 53)
        {
            talkedRogue = true;
        }

        if (id == 72)
        {
            talkedMiko = true;
        }

        if (id == 0)
        {
            firstTimeHouseInterior = false;
        }

        if (id == 21)
        {
            firstTimeHouseOutside = false;
        }

        if (id == 51)
        {
            firstTimeAbandonedHouse = false;
        }

        if (id == 71)
        {
            firstTimeShrine = true;
        }
    }
}
