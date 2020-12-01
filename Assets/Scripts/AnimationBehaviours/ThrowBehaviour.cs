using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : StateMachineBehaviour
{
    GameObject boss;
    float timer = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isThrowing", true);
        timer = 0;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.Find("Boss");
        timer += Time.deltaTime;

        if (timer > 1.5f)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Throw", boss.transform.position);
            timer = -100;
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isThrowing", false);
    }

}
