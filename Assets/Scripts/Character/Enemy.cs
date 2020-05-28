using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private float attackRange;

    private NavMeshAgent agent;
    private bool isNearPlayer;
    private bool isAttack = false;
    private Transform target;


    private void Awake()
    {
        SetUpCharacterComp();
    }

    void Start()
    {
        target = SceneController.instance.Player.transform;
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange;
    }

    protected override void SetUpCharacterComp()
    {
        base.SetUpCharacterComp();
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead || IsPause || !SceneController.instance.IsGameProgress)
        {
            agent.isStopped = true;
            agent.ResetPath();
            return;
        }

        // move character to player
        if (!IsNearTarget())
        {
            MoveToTarget();
        }
        else
        {
            Attack();
        }   
    }

    private void Update()
    {
        if (isDead || IsPause)
            return;

        UpdateAnimation();
        UpdateLookSide();
    }

    // -------------- Move logic --------------

    private bool IsNearTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= attackRange;
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
    }

    // ------------- Animation logic --------------
    private void UpdateAnimation()
    {
        float actualMoveSpeed = agent.velocity.magnitude;
        //Debug.Log("UpdateAnimation: " + actualMoveSpeed);
        characterAnimator.SetFloat("MoveSpeed", actualMoveSpeed);
    }

    private void UpdateLookSide()
    {
        Vector3 dir = transform.position - target.position;
        float side  = Helpers.AngleDir(target.forward, dir, Vector3.up) * -1;

        Debug.DrawLine(transform.position, transform.position + target.forward, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.red);

        if (side != 0)
            transform.localScale = new Vector3(side, 1, 1);
    }


    // -------------- Combat logic --------------
    private void Attack()
    {
        //Debug.Log("Attack!");
        isAttack = true;
        characterCombat.Fight();
    }

    // --------------- Dev ---------------
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
