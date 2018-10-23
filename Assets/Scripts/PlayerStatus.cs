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
    [SerializeField] private float groundCheckdistance = 1f;
    [SerializeField] private Transform raypoint;

    public bool isGrounded;



    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Check with raycast if player touch ground or not
    private void FixedUpdate()
    {
        //RaycastHit2D hit2D = Physics2D.Raycast(raypoint.position, Vector2.down, groundCheckdistance, groundLayerMask);
        RaycastHit2D hit2D = Physics2D.BoxCast(raypoint.position, new Vector2 (1, 1), 0f, Vector2.down, groundCheckdistance, groundLayerMask);
        if (hit2D)
        {
            isGrounded = true;
            Debug.DrawRay(raypoint.position, -raypoint.up * groundCheckdistance, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(raypoint.position, -raypoint.up * groundCheckdistance, Color.red);
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
