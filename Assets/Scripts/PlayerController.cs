using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health;
    [SerializeField] private int attackDamage;

    private Rigidbody   playerRigidbody;

    private Vector3 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        GetInput();
        MovePlayer();
    }

    // --------------------- Movement logic ---------------------
    private void GetInput()
    {
        moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity.Normalize();
    }

    private void MovePlayer()
    {
        playerRigidbody.velocity = moveVelocity * moveSpeed;
    }



}
