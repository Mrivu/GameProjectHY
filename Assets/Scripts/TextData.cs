using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum Mood
{
    None, // None means don't show this character
    Default,
    Surprised,
    Upset,
    Angry,
    Happy
}

public enum Speaking
{
    Player,
    NPC1,
    NPC2
}

public static class TextData
{
    // ID, list of TextEntries, text or choice
    public static Dictionary<int, ArrayList> textData = new Dictionary<int, ArrayList>()
    {
        // ID: 0, is debug dialogue
        {0, new ArrayList {
        new TextEntry("This is said by the player. This text exist solely to see if the text system works and how scrolling text should look like. " +
            "It has no impact on the game nor has it any relevance other than serving as a placeholder for dialogue.", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("This Text is the next entry in" +
            " text without any actual impact. It just exists to test if the sytem works.", Mood.Default, Mood.Default, Speaking.NPC1),
        new TextEntry("Seriously, There is nothing here.", Mood.Default, Mood.Default, Speaking.NPC1),
        new TextEntry("This Text is the next entry in" +
            " text without any actual impact. It just exists to test if the sytem works.", Mood.Default, Mood.Default, Speaking.NPC1),
        new TextEntry("This is the last line", Mood.Default, Mood.Default, Speaking.NPC1),
        //new DialogueChoice(new List<(string, int)> { ("1. This is a choice you can take in the dialogue, it leads to dialogue 1", 1),
        //("2. This is also choice you can take in the dialogue, it leads to dialogue 2", 2)})
        }},
        
        // ID: 1, <What is this conversation about>
        { 1, new ArrayList {}}
    };

}

public class TextEntry
{
    public string text;
    public Mood playerSprite;
    public Mood talkTargetSprite;
    // Who says the line
    public Speaking talker;

    public TextEntry(string text, Mood platerSprite, Mood talkTargetSprite, Speaking talker)
    {
        this.text = text;
        this.playerSprite = platerSprite;
        this.talkTargetSprite = talkTargetSprite;
        this.talker = talker;
    }
}

public class DialogueChoice
{
    public List<(string, int)> choices = new List<(string, int)>();

    public DialogueChoice(List<(string, int)> choices) 
    {
        this.choices = choices;
    }
}