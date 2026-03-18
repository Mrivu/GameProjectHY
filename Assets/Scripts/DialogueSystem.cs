using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public RectTransform mainCharacter;
    private Vector2 mainCharacterGoal;

    public RectTransform talkTarget;
    private Vector2 talkTargetGoal;

    public RectTransform textBox;
    private Vector2 textBoxGoal;

    public Image BGDim;
    private Color TargetDim;


    public float AnimationTime = 0.5f;
    private Coroutine DialogueAnimation;

    private void Awake()
    {
        TargetDim = BGDim.color;
        mainCharacterGoal = mainCharacter.anchoredPosition;
        talkTargetGoal = talkTarget.anchoredPosition;
        textBoxGoal = textBox.anchoredPosition;
    }


    public void StartDialogueAnimation()
    {
        mainCharacter.anchoredPosition = new Vector2(mainCharacterGoal.x - 300, mainCharacterGoal.y);
        talkTarget.anchoredPosition = new Vector2(talkTargetGoal.x + 300, talkTargetGoal.y);
        textBox.anchoredPosition = new Vector2(textBoxGoal.x, textBoxGoal.y - 200);
        if (DialogueAnimation != null)
        {
            StopCoroutine(FadeDialogue());
        }
        DialogueAnimation = StartCoroutine(FadeDialogue());
    }

    private IEnumerator FadeDialogue()
    {
        // Exponential animation, not linear. Notice we start lerping from current position.
        float time = 0;
        Color start = BGDim.color;
        start.a = 0f;
        BGDim.color = start;

        while (time < AnimationTime)
        {
            time += Time.deltaTime;
            float t = time / AnimationTime;

            Color c = BGDim.color;
            c.a = Mathf.Lerp(c.a, TargetDim.a, t);
            BGDim.color = c;

            mainCharacter.anchoredPosition = Vector2.Lerp(mainCharacter.anchoredPosition, mainCharacterGoal, t);
            talkTarget.anchoredPosition = Vector2.Lerp(talkTarget.anchoredPosition, talkTargetGoal, t);
            textBox.anchoredPosition = Vector2.Lerp(textBox.anchoredPosition, textBoxGoal, t);

            yield return null;
        }
        DialogueAnimation = null;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
