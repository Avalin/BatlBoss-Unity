using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehaviour : StateMachineBehaviour {

    GameObject boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDead", true);
        boss = GameObject.Find("Boss");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Die", boss.transform.position);
    }
}
