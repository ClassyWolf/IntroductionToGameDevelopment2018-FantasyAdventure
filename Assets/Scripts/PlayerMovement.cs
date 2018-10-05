using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private float moveInput;

    public float jumpForce;
    private bool isGrounded;
    public Transform feetPos;
    public float circleRadius;
    public LayerMask whatIsGround;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(isJumping == true && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter < 0)
                isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        /*else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }*/
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
    }
}
