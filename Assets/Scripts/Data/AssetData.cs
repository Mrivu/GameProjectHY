using System.Collections.Generic;
using UnityEngine;

public static class AssetData
{
    public static Player player = new Player();
    public static Toyotomi toyotomi = new Toyotomi();
    public static Rogue rogue = new Rogue();
    public static Miko miko = new Miko();

    public static List<Character> characters = new List<Character>()
    {
        player, toyotomi, rogue, miko
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

public class Rogue : Character
{
    public Rogue() : base("Rogue")
    {
        // Replace with unique to character
        moods[0] = Resources.Load<Sprite>("Characters/Rogue/rogue_basee");
        moods[1] = Resources.Load<Sprite>("Characters/Rogue/rogue_base");
        moods[2] = Resources.Load<Sprite>("Characters/Rogue/rogue_surprised");
        moods[3] = Resources.Load<Sprite>("Characters/Rogue/rogue_happy");
        moods[4] = Resources.Load<Sprite>("Characters/Rogue/rogue_happy");
        moods[5] = Resources.Load<Sprite>("Characters/Rogue/rogue_upset");
    }
}

public class Miko : Character
{
    public Miko() : base("Miko")
    {
        // Replace with unique to character
        moods[0] = Resources.Load<Sprite>("Characters/Miko/miko_base");
        moods[1] = Resources.Load<Sprite>("Characters/Miko/miko_base");
        moods[2] = Resources.Load<Sprite>("Characters/Miko/miko_surprised");
        moods[3] = Resources.Load<Sprite>("Characters/Miko/miko_smile");
        moods[4] = Resources.Load<Sprite>("Characters/Miko/miko_happy");
        moods[5] = Resources.Load<Sprite>("Characters/Miko/miko_angry");
    }
}
