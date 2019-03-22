using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(RigidBody2D))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private float jumpForce;
    [SerializeField] private float pullForce;
    //[SerializeField] private DistanceJoint2D anchor;
    private float moveInput;
    public float airDrag = 10;
    public float hookDrag = 1;
    public float normalDrag = 20;

    private PlayerStatus playerStatus;
    public HookJoint hook;

    public bool isSwinging;

    public AudioClip moveAudio;
    public AudioClip jumpAudio;

    private Animator animator;
    //private SpriteRenderer spriteRenderer;

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
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    public void FixedUpdate()
    {
        if(playerStatus.isGrounded == false && hook.attached == false)
        {
            playerBody.drag = airDrag;
        }

        else if(playerStatus.isGrounded == false && hook.attached == true)
        {
            playerBody.drag = hookDrag;
        }

        else if (playerStatus.isGrounded == true)
        {
            playerBody.drag = normalDrag;
        }
    }

    // Give movement for player with horizontal axel
    public void PlayerMove()
    {
        //Vector2 move = Vector2.zero;
        //move.x = Input.GetAxis("Horizontal");
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            //transform.eulerAngles = Vector3.zero;
            if (playerStatus.isGrounded == true)
            {
                SoundManager.instance.PlayMove(moveAudio);
                animator.SetTrigger("MCWalking");
                if (moveInput < 0)
                {
                    animator.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (moveInput > 0)
                {
                    animator.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
        //playerBody.velocity = new Vector2(speed * moveInput, playerBody.velocity.y);
        playerBody.AddForce(new Vector2(speed * moveInput, 0f));

        /*bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }*/
    }

    // Make player jump once
    public void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerStatus.isGrounded == true)
        {
            //playerBody.AddForce(new Vector2(0f, jumpForce));
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
            animator.SetTrigger("MCJump");
            SoundManager.instance.PlayJump(jumpAudio);
        }
    }

    /*private void Hanging()
    {
        if(hookSystem.attached == true)
        {
            animator.SetTrigger("MCHanging");
        }
    }

    public void PullPlayer()
    {
        print("pull");
        //anchor.distance = 0.005f;
        StartCoroutine(UpdateLast());
    }

    IEnumerator UpdateLast()
    {
        yield return new WaitForEndOfFrame();
        anchor.distance = 0.005f;
    }*/

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
