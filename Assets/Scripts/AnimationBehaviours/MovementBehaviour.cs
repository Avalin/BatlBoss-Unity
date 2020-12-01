using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : StateMachineBehaviour {

    GameObject boss;
    float movementTimer = 0;
    float idleTimer = 0;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementTimer += Time.deltaTime;
        idleTimer += Time.deltaTime;
        boss = GameObject.Find("Boss");

        if(movementTimer > 0.5f)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Movement/Walk", boss.transform.position);
            movementTimer = 0;
        }

        if(idleTimer > 13f)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Idle", boss.transform.position);
            idleTimer = 0;

        }

    }

}
