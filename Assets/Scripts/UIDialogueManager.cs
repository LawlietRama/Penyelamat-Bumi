using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;

public class UIDialogueManager : MonoBehaviour
{
    public bool inDialogue;

    public static UIDialogueManager instance;

    public CanvasGroup canvasGroup;
    public TMP_Animated animatedText;
    public Image nameBubble;
    public TextMeshProUGUI nameTMP;

    [HideInInspector]
    public NPCScript currentNPC;

    public int dialogueIndex;
    public bool canExit;
    public bool nextDialogue;

    [Space]

    [Header("Cameras")]
    public GameObject gameCam;
    public GameObject dialogueCam;

    public ButtonManager interactButton;

    private void Awake()
    {
        instance = this;
        FadeUI(false, .0f, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        animatedText.onDialogueFinish.AddListener(() => FinishDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (interactButton.pressed && inDialogue)
        {
            if (canExit)
            {
                CameraChange(false);
                FadeUI(false, .2f, 0);
                Sequence s = DOTween.Sequence();
                s.AppendInterval(.8f);
                s.AppendCallback(() => ResetState());
                //ResetState();
            }

            if (nextDialogue)
            {
                animatedText.ReadText(currentNPC.dialogue.conversationBlock[dialogueIndex]);
            }
        }
    }

    public void FadeUI(bool show, float time, float delay)
    {
        Sequence s = DOTween.Sequence();
        s.AppendInterval(delay);
        s.Append(canvasGroup.DOFade(show ? 1 : 0, time));
        if (show)
        {
            dialogueIndex = 0;
            s.Join(canvasGroup.transform.DOScale(0, time * 2).From().SetEase(Ease.OutBack));
            s.AppendCallback(() => animatedText.ReadText(currentNPC.dialogue.conversationBlock[0]));
        }
    }

    public void SetCharNameAndColor()
    {
        nameTMP.text = currentNPC.data.npcName;
        nameTMP.color = currentNPC.data.npcNameColor;
        nameBubble.color = currentNPC.data.npcColor;
    }

    public void CameraChange(bool dialogue)
    {
        gameCam.SetActive(!dialogue);
        dialogueCam.SetActive(dialogue);

        //Depth of field modifier
        /*float dofWeight = dialogueCam.activeSelf ? 1 : 0;
        DOVirtual.Float(dialogueDof.weight, dofWeight, .8f, DialogueDOF);*/
    }

    public void ClearText()
    {
        animatedText.text = string.Empty;
    }

    public void ResetState()
    {
        currentNPC.Reset();
        FindObjectOfType<Player>().stopMove = false;    //balik bikin gerak
        inDialogue = false;
        canExit = false;
    }

    public void FinishDialogue()
    {
        if (dialogueIndex < currentNPC.dialogue.conversationBlock.Count - 1)
        {
            dialogueIndex++;
            nextDialogue = true;
        }
        else
        {
            nextDialogue = false;
            canExit = true;
        }
    }


}
