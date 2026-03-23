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
        // Negatice ID's reserved for tutorial
        // ID: 0
        // -1, Tutorial text
        {-1, new ArrayList {
        new TextEntry("Hello reader! This is a tutorial to the dialogue system. In this short tutorial you will see how" +
            " dialogue affects the UI and how choices work. Press space to advance to the next text.", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("You made it! Space can also be pressed to skip a long text animation. Try it on the next one!", Mood.Default, Mood.Default, Speaking.NPC1),
        new TextEntry("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
            "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
            "AAAAAAAAAAAAAAAAAAAA", Mood.Default, Mood.Default, Speaking.NPC1),
        new TextEntry("Well done, you learnt how to navigate the dialogue system. Now it's time for a choice!" +
            " Click the choice with your mouse to make the choice. There can be up to 3 dialogue choices available.", Mood.Default, Mood.Default, Speaking.NPC1),
        new DialogueChoices(new List<(string, int)> {
        ("1. Choose this if you want to end the dialogue.", -2),
        ("2. Choose this if you want to learn about Moods.", -3),
        ("3. Choose this if you want to repeat this lesson.", -1)})
        }},
        
        // ID: -2, Tutorial text - end dialogue
        {-2, new ArrayList {
        new TextEntry("A conversation ends when there are no dialogue options to choose from." +
             " This is the only text in this dialogue, so it will end after this.", Mood.Default, Mood.Default, Speaking.Player),
        }},

        // ID: -3, Tutorial text - moods
        {-3, new ArrayList {
        new TextEntry("Okay, so moods are reactions characters have while talking.", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Look, now both characters are Angry!", Mood.Angry, Mood.Angry, Speaking.Player),
        new TextEntry("You can switch which character is speaking by changing the Speaking enum.", Mood.Angry, Mood.Angry, Speaking.NPC1),
        new TextEntry("And if a mood is set to None, the character dissapears, " +
            "making it look like the player is talking to themselves.", Mood.Surprised, Mood.None, Speaking.Player),
        new TextEntry("That's all for now...", Mood.Surprised, Mood.None, Speaking.Player),
        new DialogueChoices(new List<(string, int)> {
        ("1. Choose this if you want to end the dialogue.", -2),
        ("2. Choose this if you want to go back.", -1)})
        }},


        // Example
        // ID: ?, <What is this conversation about>
        {0, new ArrayList {
        new TextEntry("A conversation ends when there are no dialogue options to choose from." +
             " This is the only text in this dialogue, so it will end after this.", Mood.Default, Mood.Default, Speaking.Player),
        }},
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

public class DialogueChoices
{
    public List<(string, int)> choices = new List<(string, int)>();

    public DialogueChoices(List<(string, int)> choices) 
    {
        this.choices = choices;
    }
}