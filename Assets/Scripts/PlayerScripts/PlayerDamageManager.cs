using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDamageManager : MonoBehaviour {

    public HealthController playerHealthController;
    public float playerHealthPoints = 1000;
    public VignetteFader healthLossIndicatorVignette;
    float timeSinceLastHit = 0;
    bool sendOnce = true;

    void Start () {
        playerHealthController.SetHealthPointsCapacity(playerHealthPoints);
    }
	
	void Update () {
        timeSinceLastHit += Time.deltaTime;

        if(playerHealthController.healthPointsCurrently <= 0)
        {
            if(sendOnce)
            {
                //PlayerIsDead
                SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
                sendOnce = false;
            }

        }

        if(timeSinceLastHit > 1.5f)
            healthLossIndicatorVignette.FadeOut(0.5f);
    }

    float CalculateDamage(float weaponDmg)
    {
        float damage;

        damage = weaponDmg;

        return damage;
    }
    
    public void HitRegistered(Collision col)
    {
        WeaponProperties wp = col.gameObject.GetComponent<WeaponProperties>();
        if(timeSinceLastHit > 0.3f)
        {
            float damage = CalculateDamage(wp.hitDmg);

            if(damage > 200)
            {
                damage = 200;
            }

            healthLossIndicatorVignette.FadeIn(0.3f);
            playerHealthController.DecreaseCurrentHP(damage);
            timeSinceLastHit = 0;

            if(col.gameObject.tag == "bossGlove")
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/PunchTalk", GetComponent<Transform>().position);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Abilities/Punch", GetComponent<Transform>().position);
                Debug.Log("Sound");
            }
        }

    }
}
