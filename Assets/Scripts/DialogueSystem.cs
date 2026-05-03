using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [HideInInspector] public bool dialogueRunning = false;

    [Header("Animation")]
    public float speakingBigSize = 1.2f;
    public float speakingSmallSize = 0.8f;
    public float speakingAnimationTime = 0.4f;
    private Coroutine speakingAnimation;
    public RectTransform player;
    public Image playerMood;
    private Vector2 playerGoal;
    private Vector2 playerStart;

    public RectTransform talkTarget;
    public Image talkTargetMood;
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

    public List<GameObject> charactersInScene = new List<GameObject>();


    [Header("Text")]
    public TMP_FontAsset font;
    public TextMeshProUGUI textField;
    public float scrollSpeed = 1.0f;
    private Coroutine ScrollAnimation;
    private int currentText = 0;
    private int conversationId = 0;
    private string textToDisplay = "This is a placeholder";
    private bool choiceToMake = false;
    public TextMeshProUGUI guideText;

    [Header("Choices")]
    public TextMeshProUGUI[] choices;
    private List<(int, ChoiceAction, string)> newDialogues;


    private void Awake()
    {
        player.gameObject.SetActive(true);

        TargetDim = BGDim.color;
        playerGoal = player.anchoredPosition;
        talkTargetGoal = talkTarget.anchoredPosition;
        textBoxGoal = textBox.anchoredPosition;

        playerStart = new Vector2(playerGoal.x - 300, playerGoal.y);
        talkTargetStart = new Vector2(talkTargetGoal.x + 300, talkTargetGoal.y);
        textBoxStart = new Vector2(textBoxGoal.x, textBoxGoal.y - 200);
    }

    public void ApplyFont()
    {
        foreach (TextMeshProUGUI choice in choices)
        {
            choice.font = font;
        }
        textField.font = font;
        guideText.font = font;
    }

    public void StartDialogueAnimation(bool fadeIn, int conversationID)
    {
        ApplyFont();

        if (fadeIn)
        {
            textField.text = "";
            talkTarget.gameObject.SetActive(false);

            foreach (GameObject character in charactersInScene)
            {
                character.SetActive(false);
            }

            player.anchoredPosition = playerStart;
            talkTarget.anchoredPosition = talkTargetStart;
            textBox.anchoredPosition = textBoxStart;
            dialogueRunning = true;
        }
        else
        {
            foreach (GameObject character in charactersInScene)
            {
                character.SetActive(true);
            }

            player.anchoredPosition = playerGoal;
            talkTarget.anchoredPosition = talkTargetGoal;
            textBox.anchoredPosition = textBoxGoal;   
            dialogueRunning = false;
        }

        if (DialogueAnimation != null)
        {
            StopCoroutine(DialogueAnimation);
        }
        DialogueAnimation = StartCoroutine(FadeDialogue(fadeIn, conversationID));
    }

    private IEnumerator FadeDialogue(bool fadeIn, int conversationID)
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
        else { StartConversation(conversationID); }
    }

    private void Update()
    {
        if (InputControls.Instance.advance.WasPressedThisFrame() && DialogueAnimation == null && !choiceToMake && !GameManager.Instance.pauseManager.gamePaused)
        {
            if (ScrollAnimation != null)
            {
                StopCoroutine(ScrollAnimation);
                ScrollAnimation = null;
                textField.text = textToDisplay;
            }
            else
            {
                GameManager.Instance.audioManager.PlayTextSFX();
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

        InteractExceptions.Instance.CheckDialogueState(conversationId);

        newDialogues = new List<(int, ChoiceAction, string)>();
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
            StartDialogueAnimation(false, 0);
            return;
        }

        // Text Entry
        if (TextData.textData[conversationId][currentText].GetType() == typeof(TextEntry))
        {
            TextEntry textEntry = (TextEntry)TextData.textData[conversationId][currentText];
            textToDisplay = textEntry.text;

            // Handle choice action
            if (textEntry.action != ChoiceAction.None)
            {
                HandleChoiceEffect(textEntry.action, textEntry.actionValue);
            }

            if (textEntry.talker != Speaking.Player) 
            {
                currentTarget = AssetData.characters[(int)textEntry.talker];
                talkTarget.gameObject.SetActive(true);
                talkTargetWho.text = currentTarget.name;
            }

            // Set Moods and who talks
            playerMood.sprite = AssetData.player.moods[(int)textEntry.playerMood];
            if (currentTarget != null)
            {
                if (textEntry.talkTargetMood == Mood.None)
                {
                    currentTarget = null;
                    talkTarget.gameObject.SetActive(false);
                }
                else
                {
                   talkTargetMood.sprite = currentTarget.moods[(int)textEntry.talkTargetMood];
                }
            }

            if (speakingAnimation != null)
            {
                StopCoroutine(speakingAnimation);
            }

            if (textEntry.talker == Speaking.Player)
            {
                StartCoroutine(AnimateSpeaking(true));
            }
            else
            {
                StartCoroutine(AnimateSpeaking(false));
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
                if (choice.pointsValue <= InteractExceptions.Instance.endingPoints)
                {
                    choices[choiceID].gameObject.SetActive(true);
                    newDialogues.Add((choice.nextDialogueID, choice.action, choice.actionValue));
                    StartCoroutine(ScrollText(choice.text, choices[choiceID]));

                    choiceID++;
                }
            }

        }

        if (ScrollAnimation != null)
        {
            StopCoroutine(ScrollAnimation);
        }
        ScrollAnimation = StartCoroutine(ScrollText(textToDisplay, textField));
    }

    private IEnumerator AnimateSpeaking(bool playerSpeaking)
    {
        float time = 0;
        while (time < speakingAnimationTime)
        {
            time += Time.deltaTime;
            float t = time / speakingAnimationTime;

            if (playerSpeaking)
            {
                float big = Mathf.Lerp(player.localScale.x, speakingBigSize, t);
                float small = Mathf.Lerp(talkTarget.localScale.x, speakingSmallSize, t);

                player.localScale = new Vector2(big, big);
                talkTarget.localScale = new Vector2(small, small);
            }
            else
            {
                float big = Mathf.Lerp(talkTarget.localScale.x, speakingBigSize, t);
                float small = Mathf.Lerp(player.localScale.x, speakingSmallSize, t);

                player.localScale = new Vector2(small, small);
                talkTarget.localScale = new Vector2(big, big);
            }

            yield return null;
        }

        speakingAnimation = null;
    }

    private IEnumerator ScrollText(string text, TextMeshProUGUI target)
    {
        float time = 0;
        int textLen = text.Length;

        while (time < scrollSpeed)
        {
            if (!GameManager.Instance.pauseManager.gamePaused)
            {
                time += Time.deltaTime;
                float t = time / scrollSpeed;

                target.text = text[0..(int)(textLen * t)];
            }

            yield return null;
        }

        ScrollAnimation = null;
    }

    public void ChoiceMade(int buttonID)
    {
        if (newDialogues[buttonID].Item2 != ChoiceAction.None)
        {
            HandleChoiceEffect(newDialogues[buttonID].Item2, newDialogues[buttonID].Item3);
        }
        if (newDialogues[buttonID].Item1 >= 0)
        {
            StartConversation(newDialogues[buttonID].Item1);
        }
        else
        {
            // end
            StartDialogueAnimation(false, 0);
        }
        choiceToMake = false;
    }

    public void HandleChoiceEffect(ChoiceAction action, string actionValue)
    {
        switch (action)
        {
            case ChoiceAction.LoadScene:
                UnityEngine.SceneManagement.SceneManager.LoadScene(actionValue);
                return;
            case ChoiceAction.GiveItem:
                GameManager.Instance.inventory.AddItem(actionValue);
                break;
            case ChoiceAction.GivePoints:
                InteractExceptions.Instance.endingPoints += Int32.Parse(actionValue);
                break;
        }
    }
}


