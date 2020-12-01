using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public float stoppingDistance = 7;
    public float orbitSpeed;

    public Animator animator;
    public Transform target;
    public NavMeshAgent agent;
    public Thrower throwerScript;

    public GameObject leftGlove;
    public GameObject rightGlove;

    float agentSpeed = 0;
    float throwTimer = 5;
    float chargeTimer = 0;
    float distanceFromPlayer;

    bool isOrbitVectorSet = false;
    bool randomVectorValue;


    enum State {Dead, Intro, Throwing, Punching, Charging, Running };
    State bossState;

    void Start()
    {
        bossState = State.Intro;
        agent.speed = agentSpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, target.position);

        if (animator.GetBool("isDead"))
            bossState = State.Dead;

        if (bossState != State.Dead)
        {
            throwTimer += Time.deltaTime;
            chargeTimer += Time.deltaTime;
            BossSequence();
            SetAgentSpeed();
        }

        if(distanceFromPlayer >  5)
        {
            leftGlove.SetActive(false);
            rightGlove.SetActive(false);
        }
    }

    void BossSequence()
    {

        if (bossState != State.Charging && distanceFromPlayer <= 4f)
        {
            agent.stoppingDistance = 2;
            PunchAttack();
            FaceTarget();
        }
        else if (throwTimer >= 10 && bossState != State.Charging)
        {
            ThrowAttack();
            isOrbitVectorSet = false;
            throwTimer = 0;
        }
        else if (chargeTimer >= 18 && bossState != State.Throwing)
        {
            ChargeRun();
            chargeTimer = 0;
        }

        else if (distanceFromPlayer < 8 && bossState == State.Charging)
        {
            ChargeAttack();
        }

        SetState();

        if (bossState == State.Running && distanceFromPlayer > 4)
            if (distanceFromPlayer >= agent.stoppingDistance)
            {
                GoTowardsThePlayer();
                FaceTarget();
            }
            else
            {
                if (!isOrbitVectorSet)
                {
                    randomVectorValue = CoinFlip();
                    isOrbitVectorSet = true;
                }

                RotateAroundThePlayer(randomVectorValue);
                FaceTarget();
            }
    }


    ///////////////////////// BOSS SEQUENCES START HERE /////////////////////////

    void RotateAroundThePlayer(bool vectorUp)
    {
        orbitSpeed = 150 / distanceFromPlayer;
        Vector3 orbitVector;
        float timeMultiplier = 5;
        float newValueX = 0;
        float newValueZ = 0;

        if (vectorUp)
        {
            newValueX = Mathf.Lerp(animator.GetFloat("velocityX"), -(orbitSpeed/13), Time.deltaTime * timeMultiplier);
            newValueZ = Mathf.Lerp(animator.GetFloat("velocityZ"), -(orbitSpeed/66), Time.deltaTime * timeMultiplier);

            orbitVector = Vector3.up;
        }
        else
        {
            newValueX = Mathf.Lerp(animator.GetFloat("velocityX"), orbitSpeed / 13, Time.deltaTime * timeMultiplier);
            newValueZ = Mathf.Lerp(animator.GetFloat("velocityZ"), orbitSpeed / 66, Time.deltaTime * timeMultiplier);

            orbitVector = Vector3.down;
        }

        animator.SetFloat("velocityX", newValueX);
        animator.SetFloat("velocityZ", newValueZ);
        transform.RotateAround(target.position, orbitVector, orbitSpeed * Time.deltaTime);
    }

    void ThrowAttack()
    {
        throwerScript.InstantiateObjectToThrow();
        animator.SetTrigger("throw");
    }

    void GoTowardsThePlayer()
    {
        agent.SetDestination(target.position);
        animator.SetFloat("velocityX", 0);
        animator.SetFloat("velocityZ", agentSpeed);
    }

    void ChargeRun()
    {
        throwerScript.InstantiateObjectToSmash();
        animator.SetTrigger("runCharge");
        agent.stoppingDistance = 4;
        GoTowardsThePlayer();
    }

    void ChargeAttack()
    {
        animator.SetTrigger("charge");
    }

    void PunchAttack()
    {
        animator.SetTrigger("punch");
        leftGlove.SetActive(true);
        rightGlove.SetActive(true);

    }

    ///////////////////////// BOSS SEQUENCES END HERE /////////////////////////


    void FaceTarget()
    {
        transform.LookAt(target);
    }

    public void IsDead(bool isDead)
    {
        bossState = State.Dead;
        animator.StopPlayback();
        animator.SetTrigger("die");
    }

    bool CoinFlip()
    {
        int random = UnityEngine.Random.Range(0, 2);
        return random == 1;
    }

    void SetAgentSpeed()
    {
        agentSpeed = (float)Math.Log(distanceFromPlayer);

        if (agentSpeed > 4)
            agentSpeed = 4;

        if (agentSpeed < 2)
        {
            agentSpeed = 2;
        }

        if (bossState != State.Charging)
        {
            agentSpeed = 4;
        }

        agent.speed = agentSpeed;
    }

    void SetState()
    {
        bool isThrowing = animator.GetBool("isThrowing");
        bool isPlayingIntro = animator.GetBool("isPlayingIntro");
        bool isCharging = animator.GetBool("isCharging");
        bool isPunching = animator.GetBool("isPunching");

        if (bossState != State.Dead)
        {
            if (isPunching) bossState = State.Punching;
            else if (isThrowing) bossState = State.Throwing;
            else if (isCharging) bossState = State.Charging;
            else if (isPlayingIntro) bossState = State.Intro;
            else bossState = State.Running;
        }

    }
}
