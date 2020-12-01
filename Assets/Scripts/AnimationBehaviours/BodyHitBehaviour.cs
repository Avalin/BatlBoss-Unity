using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitBehaviour : StateMachineBehaviour {

    
    GameObject boss;
    float timer = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.Find("Boss");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Hit", boss.transform.position);
    }
}
