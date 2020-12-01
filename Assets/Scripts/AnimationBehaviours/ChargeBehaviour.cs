using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehaviour : StateMachineBehaviour {


    GameObject boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    animator.SetBool("isCharging", false);
    boss = GameObject.Find("Boss");
    FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Charge", boss.transform.position);

    }

override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isCharging", false);
    }
}
