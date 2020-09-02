using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public Animator anim;

    public GameObject victoryZone;
    public float waitToShowExit;

    public enum BossPhase { intro, phase1, phase2, phase3, end};
    public BossPhase currentPhase = BossPhase.intro;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageBoss()
    {
        currentPhase++;

        if(currentPhase != BossPhase.end)
        {
            anim.SetTrigger("Hurt");
        }

        switch(currentPhase)
        {
            case BossPhase.phase1:
                anim.SetBool("Phase1", true);
                break;

            case BossPhase.phase2:
                anim.SetBool("Phase1", false);
                anim.SetBool("Phase2", true);
                break;

            case BossPhase.phase3:
                anim.SetBool("Phase2", false);
                anim.SetBool("Phase3", true);
                break;

            case BossPhase.end:
                anim.SetTrigger("End");
                break;
        }
    }
}
