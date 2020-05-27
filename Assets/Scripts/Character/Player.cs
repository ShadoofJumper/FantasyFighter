using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Vector3 moveVelocity;
    private bool    isAttack;
    private bool    isMove;
    private Vector3 rawMoveInput;

    public bool IsAttack => isAttack;
    public bool IsMove => isMove;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpCharacterComp();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            moveVelocity = Vector3.zero;
            //call last time for stop
            return;
        }

        GetInput();
        UpdateAnimation();
        UpdateLookSide();
        Combat();
    }


    private void FixedUpdate()
    {
        if (isDead)
            return;

        MovePlayer();
    }

    // --------------------- Movement logic ---------------------
    private void GetInput()
    {
        rawMoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = CalculateMMoveVelocity(rawMoveInput);

        // always can attack
        if(Input.GetMouseButtonDown(0))
            isAttack = true;

        //move if not attack
        isMove = rawMoveInput.magnitude != 0 ? true : false;

        if (isAttack)
        {
            isMove = false;
            moveVelocity = Vector3.zero;
        }
    }

    private Vector3 CalculateMMoveVelocity(Vector3 input)
    {
        Vector3 globalForward   = transform.forward;
        Vector3 globalRight     = Quaternion.Euler(0, 90, 0) * globalForward;

        Vector3 verticalMovement    = globalForward * input.z;
        Vector3 horizontalMovement  = globalRight * input.x;
        return Vector3.Normalize(verticalMovement + horizontalMovement);
    }

    private void MovePlayer()
    {
        characterRigidbody.velocity = moveVelocity * moveSpeed;
    }

    // --------------------- Combat logic -----------------------

    private void Combat()
    {
        if (isAttack)
        {
            characterCombat.Fight(delegate {
                isAttack = false;
            });
        }
    }


    // -------------------- Animation logic ---------------------

    private void UpdateAnimation()
    {
        float actualMoveSpeed = moveVelocity.magnitude;
        characterAnimator.SetFloat("MoveSpeed", actualMoveSpeed);
    }

    private void UpdateLookSide()
    {
        if(rawMoveInput.x!=0)
            transform.localScale = new Vector3(rawMoveInput.x, 1,1);
    }


}
