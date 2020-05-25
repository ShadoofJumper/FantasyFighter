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

    // Start is called before the first frame update
    void Start()
    {
        SetUpCharacterComp();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateAnimation();
        UpdateLookSide();
        Combat();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    // --------------------- Movement logic ---------------------
    private void GetInput()
    {
        rawMoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = CalculateMMoveVelocity(rawMoveInput);

        isMove      = rawMoveInput.magnitude != 0 ? true : false;
        isAttack    = Input.GetMouseButtonDown(0) && !isMove;
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
            characterCombat.Fight();
        }
    }


    // -------------------- Animation logic ---------------------

    private void UpdateAnimation()
    {
        float actualMoveSpeed = moveVelocity.magnitude;
        characterAnimator.SetFloat("MoveSpeed", actualMoveSpeed);
        if (isAttack)
        {
            characterAnimator.SetTrigger("MainAttack");
        }
    }

    private void UpdateLookSide()
    {
        if(rawMoveInput.x!=0)
            transform.localScale = new Vector3(rawMoveInput.x, 1,1);
    }


}
