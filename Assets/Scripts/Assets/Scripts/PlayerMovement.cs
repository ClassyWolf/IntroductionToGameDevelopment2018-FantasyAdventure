using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float frictionMultiplier;
    private Rigidbody2D rigidBody;
    private Vector3 oldScale;
    
    public bool canJump;

    private void Start()
    {
        if (moveSpeed == 0) { moveSpeed = 1; }
        if (jumpForce == 0) { jumpForce = 1; }
        if (frictionMultiplier == 0) { frictionMultiplier = 0.85f; }
        canJump = true;

        oldScale = this.transform.localScale;

        rigidBody = this.GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.D))
        { 
            rigidBody.velocity = new Vector2(rigidBody.velocity.x + moveSpeed, rigidBody.velocity.y);
            if (rigidBody.velocity.x > moveSpeed) { rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y); }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x-moveSpeed, rigidBody.velocity.y);
            if (rigidBody.velocity.x < -moveSpeed) { rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y); }
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * frictionMultiplier, rigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space) && canJump == true)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(
                -Physics2D.gravity * rigidBody.mass
                + new Vector2(0f, rigidBody.velocity.y * rigidBody.mass)
                + Vector2.up * jumpForce);
                canJump = false;

        }


        //Jump landing is checked on triggerentity at playermodel


        if (rigidBody.velocity.x < -0.1f) //to determine the orientation of the sprite
        {
            this.transform.localScale = new Vector3(oldScale.x, oldScale.y, oldScale.z);
        }
        else if (rigidBody.velocity.x > 0.1f)
        {
            this.transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
        }

    }
}
