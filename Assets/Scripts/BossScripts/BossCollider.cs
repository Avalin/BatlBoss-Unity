using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BossCollider : MonoBehaviour {

    BossDamageManager bossDM;

    void Start()
    {
        bossDM = GameObject.Find("Boss").GetComponent<BossDamageManager>();
    }

    void OnCollisionEnter(Collision col)
    {

        //if colliding with other boss's colliders
        if (col.transform.IsChildOf(transform.parent.transform.parent))
        {
            //do nothing
        }
        else if ((col.transform.tag == "Torch"  || col.transform.tag == "Sword") && col.gameObject.GetComponentInParent<Hand>() != null)
        {
            bossDM.HitRegistered(col);
        }
        else if(col.transform.tag == "projectile")
        {
            bossDM.HitRegistered(col);

        }
    }
}
