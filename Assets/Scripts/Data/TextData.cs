using System.Collections;
using System.Collections.Generic;

public enum Mood
{
    None, // None means don't show this character - 0
    Default, // 1
    Surprised, // 2
    Smile,
    Happy,
    Upset
}

public enum Speaking
{
    Player, // 0
    Toyotomi, // 1
    Rogue, // 2
    Maiden
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

        // ID 7, Clicking on the mother after coming back from the Shrine, the final conversation
        {7, new ArrayList {
        new TextEntry("Back here at last...", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("I should let her drink from the gourd.", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("...", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. [Bad Ending] I am so sorry that I could not do more for you...", 9), 
            // This is the only option available if the player is at less than 4/5 points
            new DialogueChoice("2. [Good Ending] It is time for me to accept that she might not make it.", 8, ChoiceAction.None, "", 4)})
            //This would require 4/5 or more of the points to be able to be chosen, it could be highlighted in some way to make it clear if possible
        }},
        // ID 8, Good ending dialogue
        {8, new ArrayList {
        new TextEntry("And even if she won't, I will be okay, eventually..", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("My journey has shown to me that despite all the pain and suffering that this " +
            "land has endured, its people still continue to live on and seek their purpose in life.", Mood.Smile, Mood.None, Speaking.Player),
        new TextEntry("And maybe I should do the same. Maybe I could join Hideyoshi on his adventures, and find a purpose for myself too.", Mood.Smile, Mood.None, Speaking.Player),
        new TextEntry("This land needs change and maybe we can do something about that.", Mood.Smile, Mood.None, Speaking.Player), 
        new TextEntry("Maybe we can enact real change, together.", Mood.Smile, Mood.None, Speaking.Player),
        }},
        // End?

        // ID 9, Bad ending dialogue
        {9, new ArrayList {
        new TextEntry("I don't think that she will make it...", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("If only I would've done something differently...If I would've looked for a cure earlier, maybe I could've found " +
            "someone who could actually help her...", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("What will I do once she's gone...if father and brother won't make it back from the war I will have nothing left...", Mood.Default, Mood.None, Speaking.Player),
        new TextEntry("Please, all the Goddesses and Gods of this land, save her, please...", Mood.Default, Mood.None, Speaking.Player),
        }},
        // End?

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
        new TextEntry("Hello, young samurai. Would you happen to know anything about the healer in town?", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Hello, young lady. Yes, I have indeed. Although I am no samurai, just an adventurer on his journey.", Mood.Default, Mood.Default, Speaking.Toyotomi),
        new TextEntry("I see. Well then adventurer, what have you heard of this healer?", Mood.Smile, Mood.Default, Speaking.Player),
        new TextEntry("People claim that she has the cure that can heal the sickness plaguing the people of these territories. " +
            "Whether that is true, who can say.", Mood.Smile, Mood.Default, Speaking.Toyotomi),
        new TextEntry("There is a lot that can be attributed to myths, but from my experience it is rare for the ill to be miraculously cured.", Mood.Smile, Mood.Upset, Speaking.Toyotomi),
        new TextEntry("In any case, ever since the betrayal of the Daimyo's son, the healers have been wary of various agitators and thus do not offer their services to anyone but the nobles.", Mood.Default, Mood.Default, Speaking.Toyotomi),
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
            "help from someone with a skill set that is different to mine, since far as I can see, you are no noble.", Mood.Default, Mood.Default, Speaking.Toyotomi),
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
            "help from someone with a skill set that is different to mine, since far as I can see, you are no noble.", Mood.Default, Mood.Default, Speaking.Toyotomi),
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
        // ID 31: Talking with Hideyoshi after picking up the Bell, but before talking to the Maiden. This will give the player +2 points
        {31, new ArrayList {
        new TextEntry("Hideyoshi! Your friend gave me this Kagura suzu that could get the shrine maiden to help me.", Mood.Smile, Mood.Default, Speaking.Player),
        new TextEntry("That is wonderful to hear, Hikaru. I knew she would be of use in this matter.", Mood.Happy, Mood.Smile, Speaking.Toyotomi),
        new TextEntry("So how do you know her? ...And is she really a ninja? She almost dresses like one.", Mood.Smile, Mood.Smile, Speaking.Player),
        new TextEntry("She is not a ninja although her skillset is...of the roguish type. And I met her while I was travelling the country.", Mood.Smile, Mood.Default, Speaking.Toyotomi),
        new TextEntry("Me and her being here at the same time is actually a coincidence. She is looking for something within this province and ended up taking shelter " +
            "within the abandoned house.", Mood.Smile, Mood.Default, Speaking.Toyotomi),
        new TextEntry("I myself am out here looking for a purpose. Someone to serve. This land really needs needs change and I want to be a part in enacting that.", Mood.Smile, Mood.Upset, Speaking.Toyotomi),
        new TextEntry("If I won't find anything here I will head home to Owari provice next and see if the Daimyo, Oda Nobunaga, would accept me in his service.", Mood.Smile, Mood.Upset, Speaking.Toyotomi),
        new TextEntry("I hope you succeed in finding your purpose, wherever that may end up being, Hideyoshi.", Mood.Smile, Mood.Upset, Speaking.Player, ChoiceAction.GivePoints, "2"),
        new TextEntry("Thank you. I hope you succeed in your quest as well, Hikaru. " +
            "And if you ever find yourself wanting to travel the land and seek your purpose as a fellow adventurer, just say the word.", Mood.Smile, Mood.Happy, Speaking.Toyotomi),
        new TextEntry("I'm always glad to have a friend travelling alongside me.", Mood.Smile, Mood.Happy, Speaking.Toyotomi),
        }},
        // ID 32: Interacting with the sign post in order to go to the shrine after picking up the Kagura suzu in Scene 3
        {32, new ArrayList {
        new TextEntry("I'm ready to meet with the miko. The shrine is further out from the village but walking there shouldn't take too long.", Mood.Smile, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Head out towards the shrine.", -1, ChoiceAction.LoadScene, "Shrine"),
            new DialogueChoice("2. Stay.", -1)})
        }},
        // ID 33: Interacting with Hideyoshi after conversation 31 or after talking to the Maiden. The purpose here is to be Hideyoshi's final conversation and 
        // for the player to be locked out of 31 after either condition
        {33, new ArrayList {
        new TextEntry("I hope you find what you need, Hikaru.", Mood.Smile, Mood.Smile, Speaking.Toyotomi),
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
        new TextEntry("My mother is sick...and I heard that there is a healer in town. Please...I need your help in order to get her to help me...", Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Oh. You should've said so immediately. Of course I'll help you!", Mood.Default, Mood.Smile, Speaking.Rogue),
        new TextEntry("Thank you so much.", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("The issue is that the healer is apparently only offering her services to the nobles.", Mood.Smile, Mood.Default, Speaking.Player),
        new TextEntry("Well, I happen to have something here that will help with that. It's a Kagura suzu. And not just any Kagura suzu, " +
            "but the one that was once used by the celestial goddess Amenouzume.", Mood.Smile, Mood.Upset, Speaking.Rogue),
        new TextEntry("That is if you believe in such myths. I certainly don't.", Mood.Smile, Mood.Smile, Speaking.Rogue),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. I don't believe in them either.", 54),
            new DialogueChoice("2. The Goddesses are worthy of respect even if you don't personally believe in them.", 55)})
        }},
        // ID: 54, First conversation option with the Rogue, does not award a point
        {54, new ArrayList {
        new TextEntry("I'm glad we agree.", Mood.Smile, Mood.Smile, Speaking.Rogue),
        new TextEntry("Anyways, you should take the Kagura suzu and present it to the Shrine maiden. I'm sure a religious object of this " +
            "significance will make her change her mind about helping a peasant girl.", Mood.Smile, Mood.Default, Speaking.Rogue),
        new TextEntry("Thank you so much for your help! But where did you even get such a thing?", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("You're very welcome. And it is better that you don't know. Just tell the miko that a roguish friend gave it to you.", Mood.Happy, Mood.Default, Speaking.Rogue),
        new TextEntry("I will. However, is there anything that I can do for you in return? What is it that you are even here for?", Mood.Happy, Mood.Default, Speaking.Player),
        new TextEntry("I am here on a mission to find something dear to me. And it is better for you if you don't get involved." +
            "I suppose you could say that I am seeking a purpose for myself through this mission.", Mood.Happy, Mood.Upset, Speaking.Rogue),
        new TextEntry("I understand. Good luck with finding whatever it is that you are after", Mood.Happy, Mood.Upset, Speaking.Player),
        new TextEntry("Good luck on your journey and I hope your father and brother are able to return safely as well!", Mood.Happy, Mood.Default, Speaking.Rogue),
        new TextEntry("...?", Mood.Surprised, Mood.Default, Speaking.Player),
        new TextEntry("And do keep your expectations in check. There is only so much a healer can do for someone who is close to death.", Mood.Smile, Mood.Upset, Speaking.Rogue),
        new TextEntry("I see...thank you for the warning. And good luck to you too!", Mood.Default, Mood.Upset, Speaking.Player),
        }},
        // ID: 55, Second conversation option with the Rogue, +1 points awarded
        {55, new ArrayList {
        new TextEntry("I suppose you are right. I should do better than to be disrespectful towards the Goddesses, despite my doubts.", Mood.Smile, Mood.Default, Speaking.Rogue, ChoiceAction.GivePoints, "1"),
        new TextEntry("Anyways, you should take the Kagura suzu and present it to the Shrine maiden. I'm sure a religious object of this " +
            "significance will make her change her mind about helping a peasant girl.", Mood.Smile, Mood.Default, Speaking.Rogue),
        new TextEntry("Thank you so much for your help! But where did you even get such a thing?", Mood.Happy, Mood.Smile, Speaking.Player),
        new TextEntry("You're very welcome. And it is better that you don't know. Just tell the miko that a roguish friend gave it to you.", Mood.Happy, Mood.Default, Speaking.Rogue),
        new TextEntry("I will. However, is there anything that I can do for you in return? What is it that you are even here for?", Mood.Happy, Mood.Default, Speaking.Player),
        new TextEntry("I am here on a mission to find something dear to me. And it is better for you if you don't get involved." +
            "I suppose you could say that I am seeking a purpose for myself through this mission.", Mood.Happy, Mood.Upset, Speaking.Rogue),
        new TextEntry("I understand. Good luck with finding whatever it is that you are after", Mood.Happy, Mood.Upset, Speaking.Player),
        new TextEntry("Good luck on your journey and I hope your father and brother are able to return safely as well!", Mood.Happy, Mood.Default, Speaking.Rogue),
        new TextEntry("...?", Mood.Surprised, Mood.Default, Speaking.Player),
        new TextEntry("And do keep your expectations in check. There is only so much a healer can do for someone who is close to death.", Mood.Smile, Mood.Upset, Speaking.Rogue),
        new TextEntry("I see...thank you for the warning. And good luck to you too!", Mood.Default, Mood.Upset, Speaking.Player),
        }},
        // ID: 56, Summarised conversation with the Rogue after the fact
        {56, new ArrayList {
        new TextEntry("I truly hope that the shrine maiden will appreciate the Kagura suzu and that you will get the help you need.", Mood.Smile, Mood.Default, Speaking.Rogue),
        }},
        // ID: 57, Interacting with the Kagura suzu after the conversation
        {57, new ArrayList {
        new TextEntry("It truly is stunningly beautiful. I wonder if it truly could be Amenouzume's.", Mood.Smile, Mood.None, Speaking.Player),
        new TextEntry("Well time to take it with me and head to the shrine.", Mood.Smile, Mood.None, Speaking.Player, ChoiceAction.GiveItem, "Bell"),
        }},
        // ID: 58, Interacting with the door in order to leave
        {58, new ArrayList {
        new TextEntry("I should head out.", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Go outside.", -1, ChoiceAction.LoadScene, "HouseOutside"),
            new DialogueChoice("2. Stay.", -1)})
        }},

        // ID 71, Entering the Shrine for the first time
        {71, new ArrayList {
        new TextEntry("This place is even more beautiful than I expected. And it feels special...", Mood.Smile, Mood.None, Speaking.Player),
        new TextEntry("I really hope that the shrine maiden can help mother...", Mood.Default, Mood.None, Speaking.Player),
        }},
        // ID 72, Talking to the Shrine Maiden
        {72, new ArrayList {
        new TextEntry("Oh, hello. Why are you here?", Mood.Default, Mood.Surprised, Speaking.Maiden),
        new TextEntry("I am here to meet with you. And to hopefully get help for my sick mother.", Mood.Default, Mood.Surprised, Speaking.Player),
        new TextEntry("Oh, I see.", Mood.Default, Mood.Default, Speaking.Maiden),
        new TextEntry("I do not help peasants. I am forbidden from doing so, because of the civil war.", Mood.Default, Mood.Default, Speaking.Maiden),
        new TextEntry("So take your leave. Now.", Mood.Default, Mood.Upset, Speaking.Maiden),
        new TextEntry("I have something with me that might change your mind. Look at this.", Mood.Smile, Mood.Upset, Speaking.Player),
        new TextEntry("Amenouzume's Kagura suzu?!?. But how could you have it?", Mood.Smile, Mood.Surprised, Speaking.Maiden),
        new TextEntry("A roguish friend gave it to me. That is all I can say.", Mood.Smile, Mood.Surprised, Speaking.Player),
        new TextEntry("I see...But do you even understand the significance of this?", Mood.Smile, Mood.Default, Speaking.Maiden),
        new TextEntry("I do not. But I really need your help. And I was told that this would get you to help me.", Mood.Smile, Mood.Default, Speaking.Player),
        new TextEntry("So be it...I will help you. But I need you to answer a question.", Mood.Smile, Mood.Default, Speaking.Maiden),
        new TextEntry("Do you really believe that I have the power to heal people?", Mood.Smile, Mood.Default, Speaking.Maiden),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. I do not, but I wanted to believe it.", 73),
            new DialogueChoice("2. I do. I have heard the myths of what healers like you can do.", 74)})
        }},
        // ID 73, First conversation option, +2 points awarded
        {73, new ArrayList {
        new TextEntry("Then you are wise beyond your years.", Mood.Default, Mood.Smile, Speaking.Maiden, ChoiceAction.GivePoints, "2"),
        new TextEntry("I can only ease her pain, but I cannot cure her if she is too far gone.", Mood.Default, Mood.Default, Speaking.Maiden),
        new TextEntry("Here, let me fill your gourd with a healing mixture that has water from the spring of the mountain.", Mood.Default, Mood.Default, Speaking.Maiden),
        new TextEntry("Thank you...",  Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Now go. I will make some prayers on your mother's behalf. I hope that at least someone in this land " +
        "can avoid the suffering and misery that has befallen on so many.", Mood.Default, Mood.Default, Speaking.Maiden),
        }},
        // ID 74, Second conversation option, no points awarded
        {74, new ArrayList {
        new TextEntry("Then I am afraid that I have to disappoint you.", Mood.Smile, Mood.Upset, Speaking.Maiden),
        new TextEntry("I will do what I can to ease her pain, but I cannot promise you that she will be cured.", Mood.Smile, Mood.Upset, Speaking.Maiden),
        new TextEntry("Here, let me fill your gourd with a healing mixture that has water from the spring of the mountain.", Mood.Default, Mood.Default, Speaking.Maiden),
        new TextEntry("Thank you...",  Mood.Default, Mood.Default, Speaking.Player),
        new TextEntry("Now go. I will make some prayers on your mother's behalf. I hope that at least someone in this land " +
        "can avoid the suffering and misery that has befallen on so many.", Mood.Default, Mood.Default, Speaking.Maiden),
        }},
        // ID 75, Speaking to the Maiden after the conversation
        {75, new ArrayList {
        new TextEntry("...", Mood.Default, Mood.Default, Speaking.Maiden),
        }},
        // ID 76, Heading back from the shrine
        {76, new ArrayList {
        new TextEntry("It's time to head back home...", Mood.Default, Mood.None, Speaking.Player),
        new DialogueChoices(new List<DialogueChoice> {
            new DialogueChoice("1. Head back.", -1, ChoiceAction.LoadScene, "Ending"),
            new DialogueChoice("2. Stay.", -1)})
        }},
        // ID 77, Heading back from the shrine without talking to Miko
        {77, new ArrayList {
        new TextEntry("I need to talk to the miko.", Mood.Default, Mood.None, Speaking.Player),
        }}
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

    public int pointsValue;

    public DialogueChoice(string text, int nextDialogueId, ChoiceAction action = ChoiceAction.None, string actionValue = "", int pointsValue = 0)
    {
        this.text = text;
        this.nextDialogueID = nextDialogueId;
        this.action = action;
        this.actionValue = actionValue;
        this.pointsValue = pointsValue;
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