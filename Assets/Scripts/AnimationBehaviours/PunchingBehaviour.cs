using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBehaviour : StateMachineBehaviour {


    //you have punching sounds
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPunching", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPunching", false);
    }
}
