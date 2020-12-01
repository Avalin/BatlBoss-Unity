using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    public PlayerDamageManager playerDM;


    void OnCollisionEnter(Collision col)
    {
        //if player's colliders hit each other
        if (col.transform.IsChildOf(transform.parent))
        {
            //do nothing
        }
        else if (col.transform.tag == "bossGlove" || col.transform.tag == "projectile")
        {
            playerDM.HitRegistered(col);
        }
    }
}
