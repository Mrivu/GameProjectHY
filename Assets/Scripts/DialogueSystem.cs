using System.Collections;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

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
        if (InputControls.instance.advance.WasPressedThisFrame())
        {
            Debug.Log("Space pressed");
        }
    }

    void StartConversation(int conversationId)
    {
        int currentText = 0;
        string textToDisplay = "This is a placeholder";

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
            DialogueChoice dialogueChoice = (DialogueChoice)TextData.textData[conversationId][currentText];
            textToDisplay = dialogueChoice.choiceText;
        }

        if (ScrollAnimation != null)
        {
            StopCoroutine(ScrollAnimation);
        }
        ScrollAnimation = StartCoroutine(ScrollText(textToDisplay));

    }

    private IEnumerator ScrollText(string text)
    {
        float time = 0;
        int textLen = text.Length;

        while (time < scrollSpeed)
        {
            time += Time.deltaTime;
            float t = time / scrollSpeed;

            textField.text = text[0..(int)(textLen*t)];

            yield return null;
        }

        ScrollAnimation = null;
    }
}
