using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeRunBehaviour : StateMachineBehaviour {

    float timer = 0;
    GameObject boss;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isCharging", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.Find("Boss");
        timer += Time.deltaTime;

        if (timer > 0.3f)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Movement/Walk", boss.transform.position);
            timer = 0;
        }
    }

}
