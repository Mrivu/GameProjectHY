using System.Collections.Generic;
using UnityEngine;

public static class AssetData
{
    public static Player player = new Player();
    public static NPC1 npc1 = new NPC1();
    public static NPC2 npc2 = new NPC2();

    public static List<Character> characters = new List<Character>()
    {
        player, npc1, npc2
    };
}

public class Character
{
    public string name;
    public Dictionary<int, string> moods = new Dictionary<int, string>() 
    {
        // Default values
        {0, "None"},
        {1, "Default"},
        {2, "Surprised"},
        {3, "Upset"},
        {4, "Angry"},
        {5, "Happy"},
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
        //moods[0] = "a";
    }
}

public class NPC1 : Character
{
    public NPC1() : base("NPC1")
    {
        //moods[0] = "a";
    }
}

public class NPC2 : Character
{
    public NPC2() : base("NPC2")
    {
        //moods[0] = "a";
    }
}
