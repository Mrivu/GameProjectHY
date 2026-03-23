using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Animation")]
    public RectTransform player;
    public TextMeshProUGUI playerMood;
    private Vector2 playerGoal;
    private Vector2 playerStart;

    public RectTransform talkTarget;
    public TextMeshProUGUI talkTargetMood;
    public TextMeshProUGUI talkTargetWho;
    public Character currentTarget;
    private Vector2 talkTargetGoal;
    private Vector2 talkTargetStart;

    public RectTransform textBox;
    private Vector2 textBoxGoal;
    private Vector2 textBoxStart;

    public Image BGDim;
    public float DimIntensity = 190f;
    private Color TargetDim;


    public float AnimationTime = 0.5f;
    private Coroutine DialogueAnimation;


    [Header("Text")]
    public TextMeshProUGUI textField;
    public float scrollSpeed = 1.0f;
    private Coroutine ScrollAnimation;
    private int currentText = 0;
    private int conversationId = 0;
    private string textToDisplay = "This is a placeholder";
    private bool choiceToMake = false;

    [Header("Choices")]
    public TextMeshProUGUI[] choices;
    private List<int> newDialogues;


    private void Awake()
    {
        TargetDim = BGDim.color;
        playerGoal = player.anchoredPosition;
        talkTargetGoal = talkTarget.anchoredPosition;
        textBoxGoal = textBox.anchoredPosition;

        playerStart = new Vector2(playerGoal.x - 300, playerGoal.y);
        talkTargetStart = new Vector2(talkTargetGoal.x + 300, talkTargetGoal.y);
        textBoxStart = new Vector2(textBoxGoal.x, textBoxGoal.y - 200);
    }


    public void StartDialogueAnimation(bool fadeIn)
    {
        if (fadeIn)
        {
            player.anchoredPosition = playerStart;
            talkTarget.anchoredPosition = talkTargetStart;
            textBox.anchoredPosition = textBoxStart;
        }
        else
        {
            player.anchoredPosition = playerGoal;
            talkTarget.anchoredPosition = talkTargetGoal;
            textBox.anchoredPosition = textBoxGoal;   
        }

        if (DialogueAnimation != null)
        {
            StopCoroutine(DialogueAnimation);
        }
        DialogueAnimation = StartCoroutine(FadeDialogue(fadeIn));
    }

    private IEnumerator FadeDialogue(bool fadeIn)
    {
        float time = 0;

        Color start = BGDim.color;
        TargetDim = BGDim.color;

        if (fadeIn) { start.a = 0f; TargetDim.a = DimIntensity / 255; }
        else { start.a = DimIntensity / 255; TargetDim.a = 0f;}
        BGDim.color = start;

        while (time < AnimationTime)
        {
            time += Time.deltaTime;
            float t = time / AnimationTime;

            Color c = BGDim.color;
            c.a = Mathf.Lerp(start.a, TargetDim.a, t);
            BGDim.color = c;

            if (fadeIn)
            {
                player.anchoredPosition = Vector2.Lerp(playerStart, playerGoal, t);
                talkTarget.anchoredPosition = Vector2.Lerp(talkTargetStart, talkTargetGoal, t);
                textBox.anchoredPosition = Vector2.Lerp(textBoxStart, textBoxGoal, t);
            }
            else
            {
                player.anchoredPosition = Vector2.Lerp(playerGoal, playerStart, t);
                talkTarget.anchoredPosition = Vector2.Lerp(talkTargetGoal, talkTargetStart, t);
                textBox.anchoredPosition = Vector2.Lerp(textBoxGoal, textBoxStart, t);
            }
            yield return null;
        }
        DialogueAnimation = null;

        if (!fadeIn) { gameObject.SetActive(false); }
        else { StartConversation(-1); }
    }

    private void Update()
    {
        if (InputControls.instance.advance.WasPressedThisFrame() && DialogueAnimation == null && !choiceToMake)
        {
            if (ScrollAnimation != null)
            {
                StopCoroutine(ScrollAnimation);
                ScrollAnimation = null;
                textField.text = textToDisplay;
            }
            else
            {
                currentText++;
                NewText();
            }
        }
    }

    void StartConversation(int cID)
    {
        foreach (var choice in choices)
        {
            if (choice != null)
            {
                choice.gameObject.SetActive(false);
            }
        }
        newDialogues = new List<int>();
        currentTarget = null;
        talkTarget.gameObject.SetActive(false);

        conversationId = cID;
        currentText = 0;
        NewText();
    }

    void NewText()
    {
        if (currentText >= TextData.textData[conversationId].Count)
        {
            // end
            StartDialogueAnimation(false);
            return;
        }

        // Text Entry
        if (TextData.textData[conversationId][currentText].GetType() == typeof(TextEntry))
        {
            TextEntry textEntry = (TextEntry)TextData.textData[conversationId][currentText];
            textToDisplay = textEntry.text;

            if (textEntry.talker != Speaking.Player) 
            {
                currentTarget = AssetData.characters[(int)textEntry.talker];
                talkTarget.gameObject.SetActive(true);
                talkTargetWho.text = currentTarget.name;
            }

            // Set Moods and who talks
            playerMood.text = AssetData.player.moods[(int)textEntry.playerMood];
            if (currentTarget != null)
            {
                if (textEntry.talkTargetMood == Mood.None)
                {
                    currentTarget = null;
                    talkTarget.gameObject.SetActive(false);
                }
                else
                {
                   talkTargetMood.text = currentTarget.moods[(int)textEntry.talkTargetMood];
                }
            }
        }

        // Dialogue Choice
        else
        {
            choiceToMake = true;
            DialogueChoices dialogueChoices = (DialogueChoices)TextData.textData[conversationId][currentText];
            //textToDisplay = dialogueChoice.choiceText;
            textToDisplay = "";

            int choiceID = 0;
            foreach (var choice in dialogueChoices.choices)
            {
                choices[choiceID].gameObject.SetActive(true);
                newDialogues.Add(choice.Item2);
                StartCoroutine(ScrollText(choice.Item1, choices[choiceID]));

                choiceID++;
            }

        }

        if (ScrollAnimation != null)
        {
            StopCoroutine(ScrollAnimation);
        }
        ScrollAnimation = StartCoroutine(ScrollText(textToDisplay, textField));
    }

    private IEnumerator ScrollText(string text, TextMeshProUGUI target)
    {
        float time = 0;
        int textLen = text.Length;

        while (time < scrollSpeed)
        {
            time += Time.deltaTime;
            float t = time / scrollSpeed;

            target.text = text[0..(int)(textLen*t)];

            yield return null;
        }

        ScrollAnimation = null;
    }

    public void ChoiceMade(int buttonID)
    {
        StartConversation(newDialogues[buttonID]);
        choiceToMake = false;
    }
}
