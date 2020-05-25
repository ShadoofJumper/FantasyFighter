using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;

    private Rigidbody   playerRigidbody;
    private Animator    playerAnimator;

    private Vector3 moveVelocity;
    private bool    isAttack;
    private bool    isMove;
    private Vector3 rawMoveInput;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator  = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateAnimation();
        UpdateLookSide();
    }


    private void FixedUpdate()
    {
        MovePlayer();
        Combat();
    }

    // --------------------- Movement logic ---------------------
    private void GetInput()
    {
        rawMoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = CalculateMMoveVelocity(rawMoveInput);

        isMove          = rawMoveInput.magnitude != 0 ? true : false;
        isAttack        = Input.GetMouseButton(0) && !isMove;
    }

    private Vector3 CalculateMMoveVelocity(Vector3 input)
    {
        Vector3 globalForward   = Camera.main.transform.forward;
        Vector3 globalRight     = Quaternion.Euler(0, 90, 0) * globalForward;

        Vector3 verticalMovement    = globalForward * input.z;
        Vector3 horizontalMovement  = globalRight * input.x;
        return Vector3.Normalize(verticalMovement + horizontalMovement);
    }

    private void MovePlayer()
    {
        playerRigidbody.velocity = moveVelocity * moveSpeed;
    }

    // --------------------- Combat logic -----------------------

    private void Combat()
    {
        if (isAttack)
        {
            //Debug.Log("ATTACK!");
        }
    }


    // -------------------- Animation logic ---------------------

    private void UpdateAnimation()
    {
        float actualMoveSpeed = moveVelocity.magnitude;
        playerAnimator.SetFloat("MoveSpeed", actualMoveSpeed);
        if (isAttack)
        {
            //Debug.Log("Update attack anim!");
            playerAnimator.SetTrigger("MainAttack");
        }
    }

    private void UpdateLookSide()
    {
        if(rawMoveInput.x!=0)
            transform.localScale = new Vector3(rawMoveInput.x, 1,1);
    }


}
