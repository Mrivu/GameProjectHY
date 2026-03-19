using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
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
    }

}
