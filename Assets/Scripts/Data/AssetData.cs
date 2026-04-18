using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class AssetData
{
    public static Player player = new Player();
    public static Toyotomi toyotomi = new Toyotomi();
    public static NPC2 npc2 = new NPC2();

    public static List<Character> characters = new List<Character>()
    {
        player, toyotomi, npc2
    };
}

public class Character
{
    public string name;


    public Dictionary<int, Sprite> moods = new Dictionary<int, Sprite>() 
    {
        // Default values
        {0, Resources.Load<Sprite>("Characters/MC/MC_base")}, // None
        {1, Resources.Load<Sprite>("Characters/MC/MC_base")}, // Default
        {2, Resources.Load <Sprite>("Characters/MC/MC_surprise")}, // Surprised
        {3, Resources.Load <Sprite>("Characters/MC/MC_smile_small")}, // Smile
        {4, Resources.Load <Sprite>("Characters/MC/MC_smile")}, // Happy
        {5, Resources.Load <Sprite>("Characters/MC/MC_grumpy")}, // Upset
    };

    public Character(string name)
    { 
        this.name = name; 
    }
}

public class Player : Character
{
    public Player() : base("Player")
    {
        // Replace with unique to character
        moods[1] = Resources.Load<Sprite>("Characters/MC/MC_base");
    }
}

public class Toyotomi: Character
{
    public Toyotomi() : base("Toyotomi")
    {
        // Replace with unique to character
        moods[0] = Resources.Load<Sprite>("Characters/ToyotomiHideyoshi/Toyotomi_base");
        moods[1] = Resources.Load<Sprite>("Characters/ToyotomiHideyoshi/Toyotomi_base");
        moods[2] = Resources.Load<Sprite>("Characters/ToyotomiHideyoshi/Toyotomi_base");
        moods[3] = Resources.Load<Sprite>("Characters/ToyotomiHideyoshi/Toyotomi_smile");
        moods[4] = Resources.Load<Sprite>("Characters/ToyotomiHideyoshi/Toyotomi_smile");
        moods[5] = Resources.Load<Sprite>("Characters/ToyotomiHideyoshi/Toyotomi_base");
    }
}

public class NPC2 : Character
{
    public NPC2() : base("NPC2")
    {
        // Replace with unique to character
        moods[1] = Resources.Load<Sprite>("Characters/MC/MC_base");
    }
}
