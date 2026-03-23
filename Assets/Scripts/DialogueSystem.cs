using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Animation")]
    public RectTransform mainCharacter;
    private Vector2 mainCharacterGoal;
    private Vector2 mainCharacterStart;

    public RectTransform talkTarget;
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
        mainCharacterGoal = mainCharacter.anchoredPosition;
        talkTargetGoal = talkTarget.anchoredPosition;
        textBoxGoal = textBox.anchoredPosition;

        mainCharacterStart = new Vector2(mainCharacterGoal.x - 300, mainCharacterGoal.y);
        talkTargetStart = new Vector2(talkTargetGoal.x + 300, talkTargetGoal.y);
        textBoxStart = new Vector2(textBoxGoal.x, textBoxGoal.y - 200);
    }


    public void StartDialogueAnimation(bool fadeIn)
    {
        if (fadeIn)
        {
            mainCharacter.anchoredPosition = mainCharacterStart;
            talkTarget.anchoredPosition = talkTargetStart;
            textBox.anchoredPosition = textBoxStart;
        }
        else
        {
            mainCharacter.anchoredPosition = mainCharacterGoal;
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
                mainCharacter.anchoredPosition = Vector2.Lerp(mainCharacterStart, mainCharacterGoal, t);
                talkTarget.anchoredPosition = Vector2.Lerp(talkTargetStart, talkTargetGoal, t);
                textBox.anchoredPosition = Vector2.Lerp(textBoxStart, textBoxGoal, t);
            }
            else
            {
                mainCharacter.anchoredPosition = Vector2.Lerp(mainCharacterGoal, mainCharacterStart, t);
                talkTarget.anchoredPosition = Vector2.Lerp(talkTargetGoal, talkTargetStart, t);
                textBox.anchoredPosition = Vector2.Lerp(textBoxGoal, textBoxStart, t);
            }
            yield return null;
        }
        DialogueAnimation = null;

        if (!fadeIn) { gameObject.SetActive(false); }
        else { StartConversation(0); }
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

            // Set Moods and who talks
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
