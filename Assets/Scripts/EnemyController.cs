using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;

    private NavMeshAgent agent;
    private bool isNearPlayer;
    private bool isAttack = false;
    private Transform target;

    private Animator enemyAnimator;
    private Rigidbody enemyRigidbody;

    void Start()
    {
        agent           = GetComponent<NavMeshAgent>();
        enemyRigidbody  = GetComponent<Rigidbody>();
        enemyAnimator   = GetComponentInChildren<Animator>();

        target  = SceneController.instance.Player.transform;

        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
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
        Debug.Log("UpdateAnimation: " + actualMoveSpeed);

        enemyAnimator.SetFloat("MoveSpeed", actualMoveSpeed);
        if (isAttack)
        {
            //enemyAnimator.SetTrigger("MainAttack");
        }
    }

    private void UpdateLookSide()
    {
        //if (rawMoveInput.x != 0)
            //transform.localScale = new Vector3(rawMoveInput.x, 1, 1);
    }


    // -------------- Combat logic --------------
    private void Attack()
    {
        Debug.Log("Attack!");
        isAttack = true;
    }

    // --------------- Dev ---------------
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
