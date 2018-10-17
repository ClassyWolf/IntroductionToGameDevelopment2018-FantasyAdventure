using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(RigidBody2D))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private float jumpForce;
    private float moveInput;
    
    private PlayerStatus playerStatus;



    /*
    private bool isGrounded;
    public Transform feetPos;
    public float circleRadius;
    public LayerMask whatIsGround;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;
    */



    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    // Give movement for player with horizontal axel
    public void PlayerMove()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        playerBody.velocity = new Vector2(speed * moveInput, playerBody.velocity.y);
    }

    // Make player jump once
    public void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerStatus.isGrounded == true)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }
    }



    /*private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }

        if(isJumping == true && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter < 0)
                isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;
    }*/

    /*
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        //else if (moveInput < 0)
        //{
        //   transform.eulerAngles = new Vector3(0, 180, 0);
        //}
        playerBody.velocity = new Vector2(speed * moveInput, playerBody.velocity.y);
    }*/

}
