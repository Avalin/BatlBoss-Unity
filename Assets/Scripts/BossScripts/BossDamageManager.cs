using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BossDamageManager : MonoBehaviour {
    public HealthController bossHealth;
    public BossAI bossAI;
    public Transform bossDamageCenter;
    public float bossHealthPoints = 1000;
    public Animator animator;
    public FireSource fireSource;
    public float fireDamage = 5;
    public float damageMultiplier = 10;

    float timeSinceLastHit = 0;
    float burnTimer = 0;

    void Start () {
        bossHealth.SetHealthPointsCapacity(bossHealthPoints);
    }
	
	void Update () {
        timeSinceLastHit += Time.deltaTime;
        burnTimer += Time.deltaTime;

        if (!animator.GetBool("isDead"))
        {
            CheckBurningDamage();
            IsDead();
        }
	}

    float CalculateDamage(float weaponDmg, Vector3 hit)
    {
        float damage;
        float distanceFromDamageCenter = Vector3.Distance(hit, bossDamageCenter.position);

        if (distanceFromDamageCenter < 0.3)
            distanceFromDamageCenter = 0.3f;


        damage = weaponDmg / distanceFromDamageCenter;

        return damage;
    }

    void CheckBurningDamage()
    {
        if (fireSource.isBurning)
        {
            if (burnTimer >= 1)
            {
                bossHealth.DecreaseCurrentHP(fireDamage);
                burnTimer = 0;
            }
        }
    }

    void IsDead()
    {
        if (bossHealth.healthPointsCurrently <= 0)
        {
                bossAI.IsDead(true);
        }
    }
    
    public void HitRegistered(Collision col)
    {
        if(!animator.GetBool("isCharging"))
        {
            WeaponProperties wp = col.gameObject.GetComponent<WeaponProperties>();

            if (timeSinceLastHit > 0.2f)
            {
                float damage = CalculateDamage(wp.hitDmg, col.transform.position);

                if(col.gameObject.tag == "Sword" || col.gameObject.tag == "Torch")
                {
                    damage *= damageMultiplier;
                }
                 

                if (damage > 200)
                {
                    damage = 200;
                }

                if (damage >= 1)
                {
                    animator.SetTrigger("bodyHit");
                    bossHealth.DecreaseCurrentHP(damage);
                    timeSinceLastHit = 0;
                }

            }
        }

    }
}
