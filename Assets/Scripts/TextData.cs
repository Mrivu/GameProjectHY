using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum Mood
{
    None, // None means don't show this character - 0
    Default, // 1
    Surprised, // 2...
    Smile,
    Happy,
    Upset
}

public enum Speaking
{
    Player, // 0
    NPC1, // 1
    NPC2, // 2...
    NPC3
}

public static class TextData
{
    // ID, list of TextEntries, text or choice
    public static Dictionary<int, ArrayList> textData = new Dictionary<int, ArrayList>()
    {
        // negative ID means there isn't a dialogue to go to 
        
        // ID: 0, Intro dialogue, played directly after the Main Menu
        {0, new ArrayList {
        new TextEntry("Her sickness has spread even further...she cannot survive for much longer like this...", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("There have been rumours of a healer visiting the temple. I have to try to get a cure for her there.", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("Ah, of course. I should take the healing gourd with me in order to have something to carry the cure in.", Mood.Upset, Mood.None, Speaking.Player),
        }},
        // ID: 1, Interacting with the gourd
        {1, new ArrayList {
        new TextEntry("There it is, the family healing gourd.", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("I wonder if it truly has healing powers as mother always told us.", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("It feels special in a way that I can't explain...", Mood.Smile, Mood.None, Speaking.Player, ChoiceAction.GiveItem, "Healing Gourd"),
        }},
        // ID: 2, Interacting with mother's bed
        {2, new ArrayList {
        new TextEntry("I promise that I'll find something that can help you...", Mood.Upset, Mood.None, Speaking.Player),
        }},
        // ID: 3, Interacting with the door before picking up the gourd
        {3, new ArrayList {
        new TextEntry("I should take the healing gourd with me before I head out.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 4, Interacting with the door after picking up the gourd
        {4, new ArrayList {
        new TextEntry("I'm ready to head out to the temple.", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Go outside.", -1, ChoiceAction.LoadScene, "HouseOutside"),
            new DialogueChoice("2. Stay.", -1)})
        }},

        // ID: 5, Entering outdoors for the first time
        {5, new ArrayList {
            new TextEntry("Hmm, there seems to be a samurai in town. I should ask him if he knows anything about the healer.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 6, Interacting with the path or the house door before talking to Toyotomi
        {6, new ArrayList {
            new TextEntry("I should try talking to the samurai first.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 7, Interacting with Toyotomi Hideyoshi
        {7, new ArrayList {
            new TextEntry("Hello, young samurai. Would you happen to know anything about the healer in town?", Mood.Smile, Mood.Default, Speaking.Player),
            new TextEntry("Hello, young lady. Yes, I have indeed. Although I am no samurai, just an adventurer on his journey.", Mood.Smile, Mood.Default, Speaking.NPC1),
            new TextEntry("I see. Well then adventurer, what have you heard of this healer?", Mood.Smile, Mood.Default, Speaking.Player),
            new TextEntry("He is said to have a cure that can heal the sickness plaguing the people of these territories.", Mood.Smile, Mood.Default, Speaking.NPC1),
            new TextEntry("However, ever since the betrayal of the Daimyo's son, the healer has been wary of warmongers seeking to use his services for their own gain.", Mood.Smile, Mood.Default, Speaking.NPC1),
            new TextEntry("Ah, you are talking of the son who feigned illness in order to kill his brothers and " + 
                "who is now attempting to usurp the Daimyo. My father and brother are fighting in the war against him", Mood.Upset, Mood.Default, Speaking.Player),
            new TextEntry("Yes, the very same one. Why is it that you are looking to meet the healer though? Surely your father " + 
                "and brother aren't back from the war yet.", Mood.Default, Mood.Default, Speaking.NPC1),
            new TextEntry("It's my mother...she is deeply sick...I'm not sure that she can survive much longer.", Mood.Default, Mood.Default, Speaking.Player),
            new TextEntry("I see. Well, then we have to arrange a meeting for you with the healer, although that will require " + 
                "help from someone with a skill set that is different to mine.", Mood.Default, Mood.Default, Speaking.NPC1),
            new TextEntry("Head into that abandoned building over there and tell the person inside that you are a friend of mine and need to " + 
                "meet with the healer. The name is Hideyoshi", Mood.Smile, Mood.Smile, Speaking.NPC1),
            new TextEntry("A thousand thanks, adventurer. I will head there right away.", Mood.Happy, Mood.Smile, Speaking.Player),
        }},
        // ID: 8, Interacting with the path forward before heading into the building
        {8, new ArrayList {
            new TextEntry("I need to see the person that Hideyoshi told me about.", Mood.Default, Mood.None, Speaking.Player),
        }},
    };

}

public enum ChoiceAction
{
    None,
    LoadScene,
    GiveItem
}

public class TextEntry
{
    public string text;
    public Mood playerMood;
    public Mood talkTargetMood;
    // Who says the line
    public Speaking talker;

    public ChoiceAction action;
    public string actionValue;

    public TextEntry(string text, Mood playerMood, Mood talkTargetMood, Speaking talker, ChoiceAction action = ChoiceAction.None, string actionValue = "")
    {
        this.text = text;
        this.playerMood = playerMood;
        this.talkTargetMood = talkTargetMood;
        this.talker = talker;
        this.action = action;
        this.actionValue = actionValue;
    }

}


public class DialogueChoice
{
    public string text;
    public int nextDialogueID;
    public ChoiceAction action;
    public string actionValue;

    public DialogueChoice(string text, int nextDialogueId, ChoiceAction action = ChoiceAction.None, string actionValue = "")
    {
        this.text = text;
        this.nextDialogueID = nextDialogueId;
        this.action = action;
        this.actionValue = actionValue;
    }
}

public class DialogueChoices
{
    public List<DialogueChoice> choices = new List<DialogueChoice>();

    public DialogueChoices(List<DialogueChoice> choices) 
    {
        this.choices = choices;
    }
}