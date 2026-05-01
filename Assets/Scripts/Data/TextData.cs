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
    Toyotomi, // 1
    Rogue, // 2
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
        new TextEntry("There have been rumours of a healer visiting the shrine. I have to try to get a cure for her there.", Mood.Default, Mood.None, Speaking.Player),
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
        new TextEntry("I'm ready to head out to the shrine.", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Go outside.", -1, ChoiceAction.LoadScene, "HouseOutside"),
            new DialogueChoice("2. Stay.", -1)})
        }},
        // ID 5, Interacting with the painting
        {5, new ArrayList {
        new TextEntry("That painting has been there for all my life. I've always found it very calming to look at.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID 6, Interacting with the flower
        {6, new ArrayList {
        new TextEntry("I really should water it more.", Mood.Default, Mood.None, Speaking.Player),
        }},

        // ID: 21, Entering outdoors for the first time
        {21, new ArrayList {
            new TextEntry("Hmm, there seems to be a samurai in town. I should ask him if he knows anything about the healer.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 22, Interacting with the path forward or the house door before talking to Toyotomi
        {22, new ArrayList {
            new TextEntry("I should try talking to the samurai first.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 23, Interacting with Toyotomi Hideyoshi
        {23, new ArrayList {
        new TextEntry("Hello, young samurai. Would you happen to know anything about the healer in town?", Mood.Smile, Mood.Default, Speaking.Player),
        new TextEntry("Hello, young lady. Yes, I have indeed. Although I am no samurai, just an adventurer on his journey.", Mood.Smile, Mood.Default, Speaking.Toyotomi),
        new TextEntry("I see. Well then adventurer, what have you heard of this healer?", Mood.Smile, Mood.Default, Speaking.Player),
        new TextEntry("He is said to have a cure that can heal the sickness plaguing the people of these territories.", Mood.Smile, Mood.Default, Speaking.Toyotomi),
        new TextEntry("However, ever since the betrayal of the Daimyo's son, the healer has been wary of agitators seeking to use his services for their own gain.", Mood.Smile, Mood.Default, Speaking.Toyotomi),
        new TextEntry("Ah, you are talking of the son who feigned illness in order to kill his brothers and " + 
            "who is now attempting to usurp the Daimyo. My father and brother are fighting in the war against him", Mood.Upset, Mood.Default, Speaking.Player),
        new TextEntry("Yes, the very same one. This land never seems to have a shortage of people who commit heinous deeds " +
            "for their own gain.", Mood.Upset, Mood.Upset, Speaking.Toyotomi),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. I wish there was real change...", 24),
            new DialogueChoice("2. I hope that father and brother are okay...", 25)})
        }},
        // ID: 24, First conversation option with Hideyoshi, +1 points awarded
        {24, new ArrayList {
        new TextEntry("I agree. This land needs proper reforms if we are to ever have lasting peace and prosperity. " +
            "We can't always be at war.", Mood.Default, Mood.Upset, Speaking.Toyotomi, ChoiceAction.GivePoints, "1"),
        new TextEntry("Anyways, why is it that you are looking to meet the healer? Surely your father " + 
            "and brother aren't back from the war yet.", Mood.Default, Mood.Default, Speaking.Toyotomi),
        new TextEntry("It's my mother...she is deeply sick...I'm not sure that she can survive much longer.", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("I see. Well, then we have to arrange a meeting for you with the healer, although that will require " + 
            "help from someone with a skill set that is different to mine.", Mood.Default, Mood.Default, Speaking.Toyotomi),
        new TextEntry("Head into that abandoned building over there and tell the person inside that you are a friend of mine and need to " + 
            "meet with the healer. The name is Hideyoshi.", Mood.Smile, Mood.Smile, Speaking.Toyotomi),
        new TextEntry("A thousand thanks, adventurer. I will head there right away. Oh, and I'm Hikaru.", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("It was a pleasure talking to you, Hikaru.", Mood.Happy, Mood.Smile, Speaking.Toyotomi),
        }},
        // ID: 25, Second conversation option with Hideyoshi, this will not change the accrued points
        {25, new ArrayList {
        new TextEntry("I'm sure they are. The war should be over soon.", Mood.Upset, Mood.Default, Speaking.Toyotomi),
        new TextEntry("Anyways, why is it that you are looking to meet the healer though? Surely your father " + 
            "and brother aren't back from the war yet.", Mood.Default, Mood.Default, Speaking.Toyotomi),
        new TextEntry("It's my mother...she is deeply sick...I'm not sure that she can survive much longer.", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("I see. Well, then we have to arrange a meeting for you with the healer, although that will require " + 
            "help from someone with a skill set that is different to mine.", Mood.Default, Mood.Default, Speaking.Toyotomi),
        new TextEntry("Head into that abandoned building over there and tell the person inside that you are a friend of mine and need to " + 
            "meet with the healer. The name is Hideyoshi.", Mood.Smile, Mood.Smile, Speaking.Toyotomi),
        new TextEntry("A thousand thanks, adventurer. I will head there right away. Oh, and I'm Hikaru.", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("It was a pleasure talking to you, Hikaru.", Mood.Happy, Mood.Smile, Speaking.Toyotomi),
        }},
        // ID 26: Summarised version of the conversation with Hideyoshi
        {26, new ArrayList {
        new TextEntry("Make sure to head into that abandoned building to meet my friend. " + 
        "You need her help in order to meet with the healer.", Mood.Default, Mood.Default, Speaking.Toyotomi),
        }},
        // ID: 27, Interacting with the path towards the fourth scene before heading into the new building
        {27, new ArrayList {
        new TextEntry("I need to see the person that Hideyoshi told me about.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 28, Interacting with the tree
        {28, new ArrayList {
        new TextEntry("It shouldn't take too long now for the snow to start melting.", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID: 29, Interacting with the birds
        {29, new ArrayList {
        new TextEntry("At least the birds get to be at peace despite the war.", Mood.Smile, Mood.None, Speaking.Player),
        }},
        // ID 30: Interacting with the entrance to the 3rd scene once Hideyoshi has been talked to
        {30, new ArrayList {
        new TextEntry("I am ready to meet Hideyoshi's friend.", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Go inside.", -1, ChoiceAction.LoadScene, "AbandonedHouse"),
            new DialogueChoice("2. Stay.", -1)})
        }},
        // ID 31: Talking with Hideyoshi after coming back from the Abandoned House and/or Shrine, this conversation will/can give +1 points
        {31, new ArrayList {
        // Note to remember to finish this!!!!!
        new TextEntry("Hideyoshi.", Mood.Default, Mood.Default, Speaking.Player),
        }},
        // ID 32: Interacting with the sign post in order to go to the shrine after picking up the Kagura suzu in Scene 3
        {32, new ArrayList {
        new TextEntry("I'm ready to meet with the miko. The shrine is further out from the village but walking there shouldn't take too long.", Mood.Smile, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Head out towards the shrine.", -1, ChoiceAction.LoadScene, "Shrine"),
            new DialogueChoice("2. Stay.", -1)})
        }},

        // ID: 51: Entering Scene 3 for the first time
        {51, new ArrayList {
        new TextEntry("I always wondered about this house, it has been abandoned for as long as I can remember.", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("It feels eerie even on the inside.", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("Anyways, time to see what Hideyoshi's friend has to say. Her clothes seem really odd though...", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("She couldn't be a ninja, could she?!?", Mood.Surprised, Mood.None, Speaking.Player),
        }},
        // ID: 52: Interacting with the Kagura suzu before interacting with the Rogue
        {52, new ArrayList {
        new TextEntry("It looks beautiful...I wonder what it is for.", Mood.Smile, Mood.None, Speaking.Player),
        }},
        // ID: 53: Talking to the Rogue
        {53, new ArrayList {
        new TextEntry("Hello...Hideyoshi told me to come talk to you...", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Hey.", Mood.Default, Mood.Default, Speaking.Rogue),
        new TextEntry("What do you need?.", Mood.Default, Mood.Default, Speaking.Rogue),
        new TextEntry("Umm...Hideyoshi told me that you could help me.", Mood.Surprised, Mood.Default, Speaking.Player),
        new TextEntry("With what?", Mood.Surprised, Mood.Default, Speaking.Rogue),
        new TextEntry("My mother is sick...and I heard that there is a healer in town. Please...I need your help in order to reach her...", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Oh. You should've said so immediately. Of course I'll help you!", Mood.Default, Mood.Smile, Speaking.Rogue),
        new TextEntry("Thank you so much.", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("So, reaching the healer is not the problem. Convincing her to help you is.", Mood.Smile, Mood.Default, Speaking.Rogue),
        new TextEntry("However, I have something here that will help with that. It's a Kagura suzu. And not just any Kagura suzu, " +
            "but the one that was once used by the celestial goddess Amenouzume.", Mood.Smile, Mood.Upset, Speaking.Rogue),
        // Note that the upset expression is based on the expectation that the upset expression is the one in the fourth picture on Discord, which is fitting of the mood here due to its seriousness
        new TextEntry("That is if you believe in such myths. I certainly don't.", Mood.Smile, Mood.Smile, Speaking.Rogue),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. I don't believe in them either.", 54),
            new DialogueChoice("2. The Goddesses are worthy of respect even if you don't personally believe in them.", 55)})
        }},
        // ID: 54, First conversation option with the Rogue, does not award a point
        {54, new ArrayList {
        new TextEntry("I'm glad we agree.", Mood.Smile, Mood.Smile, Speaking.Rogue),
        new TextEntry("Anyways, you should take the Kagura suzu and present it to the healer who is at the shrine. She is normally very " +
            "apprehensive of strangers but this should change things since she is a miko and will certainly appreciate it.", Mood.Smile, Mood.Default, Speaking.Rogue),
        new TextEntry("Thank you so much for your help!", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("You're very welcome. Good luck on your journey and I hope your father and brother are able to return safely as well!", Mood.Happy, Mood.Default, Speaking.Rogue),
        new TextEntry("...?", Mood.Surprised, Mood.Default, Speaking.Player),
        }},
        // ID: 55, Second conversation option with the Rogue, +1 points awarded
        {55, new ArrayList {
        new TextEntry("I suppose you are right. I should do better than to be disrespectful towards the Goddesses.", Mood.Smile, Mood.Default, Speaking.Rogue, ChoiceAction.GivePoints, "1"),
        new TextEntry("Anyways, you should take the Kagura suzu and present it to the healer who is at the shrine. She is normally very " +
            "apprehensive of strangers but this should change things since she is a miko and will certainly appreciate it.", Mood.Smile, Mood.Default, Speaking.Rogue),
        new TextEntry("Thank you so much for your help!", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("You're very welcome. Good luck on your journey and I hope your father and brother are able to return safely as well!", Mood.Happy, Mood.Default, Speaking.Rogue),
        new TextEntry("...?", Mood.Surprised, Mood.Default, Speaking.Player),
        }},
        // ID: 56, Summarised conversation with the Rogue after the fact
        {56, new ArrayList {
        new TextEntry("I truly hope that the shrine maiden will appreciate the Kagura suzu.", Mood.Smile, Mood.Default, Speaking.Rogue),
        }},
        // ID: 57, Interacting with the Kagura suzu after the conversation
        {57, new ArrayList {
        new TextEntry("It truly is stunningly beautiful. I wonder if it truly could be Amenouzume's.", Mood.Smile, Mood.None, Speaking.Player),
        new TextEntry("Well time to take it with me and head to the shrine.", Mood.Smile, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Pick up the Kagura suzu.", -1, ChoiceAction.GiveItem, "Bell"),
            new DialogueChoice("2. Leave it be.", -1)})
        }},
        // ID: 58, Interacting with the door in order to leave
        {58, new ArrayList {
        new TextEntry("I should head out.", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Go outside.", -1, ChoiceAction.LoadScene, "HouseOutside"),
            new DialogueChoice("2. Stay.", -1)})
        }},

    };

}

public enum ChoiceAction
{
    None,
    LoadScene,
    GiveItem,
    GivePoints
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