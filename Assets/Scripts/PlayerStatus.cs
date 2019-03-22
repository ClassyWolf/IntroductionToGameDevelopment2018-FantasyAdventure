using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]

public class PlayerStatus : MonoBehaviour
{

    //public float floatHeight;
    //public float liftForce;
    //public float damping;
    //public Rigidbody2D rb2D;


    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private Transform leftGroundCheckRayPoint;
    [SerializeField] private Transform middleGroundCheckRayPoint;
    [SerializeField] private Transform rightGroundCheckRayPoint;



    public bool isGrounded;

    public int hookCounter = 3;



    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }
    private void FixedUpdate()
    {
        // Check with raycast if player touch ground or not

        RaycastHit2D lefGroundChecktHit2D = Physics2D.Raycast(leftGroundCheckRayPoint.position, Vector2.down, groundCheckDistance, groundLayerMask);
        RaycastHit2D middleGroundChecktHit2D = Physics2D.Raycast(middleGroundCheckRayPoint.position, Vector2.down, groundCheckDistance, groundLayerMask);
        RaycastHit2D rightGroundChecktHit2D = Physics2D.Raycast(rightGroundCheckRayPoint.position, Vector2.down, groundCheckDistance, groundLayerMask);
        //RaycastHit2D groundCheckHit2D = Physics2D.BoxCast(leftGroundCheckRayPoint.position, new Vector2(1, 1), 0f, Vector2.down, groundCheckDistance, groundLayerMask);
        if (lefGroundChecktHit2D || middleGroundChecktHit2D || rightGroundChecktHit2D)//groundCheckHit2D)
        {
            isGrounded = true;
            hookCounter = 3;
            Debug.DrawRay(leftGroundCheckRayPoint.position, -leftGroundCheckRayPoint.up * groundCheckDistance, Color.green);
            Debug.DrawRay(middleGroundCheckRayPoint.position, -middleGroundCheckRayPoint.up * groundCheckDistance, Color.green);
            Debug.DrawRay(rightGroundCheckRayPoint.position, -rightGroundCheckRayPoint.up * groundCheckDistance, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(leftGroundCheckRayPoint.position, -leftGroundCheckRayPoint.up * groundCheckDistance, Color.red);
            Debug.DrawRay(middleGroundCheckRayPoint.position, -middleGroundCheckRayPoint.up * groundCheckDistance, Color.red);
            Debug.DrawRay(middleGroundCheckRayPoint.position, -middleGroundCheckRayPoint.up * groundCheckDistance, Color.red);
        }

    }












    /*void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }*/

    /*void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;
            float force = liftForce * heightError - rb2D.velocity.y * damping;
            rb2D.AddForce(Vector3.up * force);
        }
    }*/

    /*public bool isGrounded;

    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float groundCheckDistance = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit;
        if(Physics2D.Raycast(rayPoint.position, -rayPoint.up, out hit, groundCheckDistance, groundLayers))
        {
            isGrounded = true;
            Debug.DrawRay(rayPoint.position, -rayPoint.up * groundCheckDistance, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(rayPoint.position, -rayPoint.up * groundCheckDistance, Color.red);
        }
	}*/
}
