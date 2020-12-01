using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDanceBehaviour : StateMachineBehaviour {

    GameObject boss;
    float timer = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPlayingIntro", true);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        boss = GameObject.Find("Boss");

        if (timer > 1f)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Idle", boss.transform.position);
            timer = 0;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPlayingIntro", false);
    }
}
