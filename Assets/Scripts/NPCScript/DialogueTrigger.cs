using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    private UIDialogueManager ui;
    private NPCScript currentNPC;
    private Player movement;
    public CinemachineTargetGroup targetGroup;

    public GameObject jump, interact;
    public ButtonManager jumpButton;
    public ButtonManager interactButton;

    public Animator anim;

    /*[Space]

    [Header("Post Processing")]
    public Volume dialogueDof;*/

    void Start()
    {
        ui = UIDialogueManager.instance;
        movement = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (interactButton.pressed && !ui.inDialogue && currentNPC != null)
        {
            targetGroup.m_Targets[1].target = currentNPC.transform;
            anim.SetFloat("magnitude", 0, 0, Time.deltaTime);
            movement.stopMove = true;
            ui.SetCharNameAndColor();
            ui.inDialogue = true;
            ui.CameraChange(true);
            ui.ClearText();
            ui.dialogueIndex = 0;
            ui.FadeUI(true, .2f, .65f);
            currentNPC.TurnToPlayer(transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            jump.SetActive(false);
            interact.SetActive(true);
            currentNPC = other.GetComponent<NPCScript>();
            ui.currentNPC = currentNPC;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            interact.SetActive(false);
            jump.SetActive(true);
            currentNPC = null;
            ui.currentNPC = currentNPC;
        }
    }

}
