using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum Mood
{
    Default,
    Surprised,
    Upset,
    Angry,
    Happy
}

public enum Character
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
            "It has no impact on the game nor has it any relevance other than serving as a placeholder for dialogue.", Mood.Default, Mood.Default, Character.Player),
        new TextEntry("This Text is the next entry in" +
            " text without any actual impact. It just exists to test if the sytem works.", Mood.Default, Mood.Default, Character.NPC1),
        new DialogueChoice("1. This is a choice you can take in the dialogue, it leads to dialogue 1", 1) }},
        
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
    public Character talker;

    public TextEntry(string text, Mood platerSprite, Mood talkTargetSprite, Character talker)
    {
        this.text = text;
        this.playerSprite = platerSprite;
        this.talkTargetSprite = talkTargetSprite;
        this.talker = talker;
    }
}

public class DialogueChoice
{
    public string choiceText;
    public int nextDialogueID;

    public DialogueChoice(string choiceText, int nextDialogueID) 
    {
        this.choiceText = choiceText;
        this.nextDialogueID = nextDialogueID;
    }
}