using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]

public class patrollingMovement : MonoBehaviour {

    public float patrollingForce;
    public int patrolRange;
    public float minSpeed;
    public float floatHeight;
    public float floatingForce;
    public float dampingForce;

    private Vector2 forward;
    private Vector3 oldScale;
    private Vector2 origin;
    private Vector3 boundSize;

    public Rigidbody2D rb;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        oldScale = this.transform.localScale;

        origin = new Vector2(this.transform.position.x, 0f);

        boundSize = this.GetComponent<SpriteRenderer>().bounds.size;

        rb.velocity=new Vector2(minSpeed, 0f);

    }

    // Update is called once per frame
    void FixedUpdate () {

        float speed = rb.velocity.magnitude;
        Vector2 normalizedVelocity = rb.velocity.normalized;
        Vector3 offsety = new Vector3(0, -boundSize.y / 2, 0);
        //Vector2 forward = new Vector2(rb.velocity.x, 0).normalized;

        if (rb.velocity.x < -0.1f)
        {
            this.transform.localScale = new Vector3(oldScale.x, oldScale.y, oldScale.z);
        }
        else if (rb.velocity.x > 0.1f)
        {
            this.transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
        }



        RaycastHit2D hit = Physics2D.Raycast(transform.position + offsety, -Vector3.up);

        if (hit.distance!=floatHeight && hit.distance!=0f && hit.collider != null)
        {

            float heightDifference = floatHeight - hit.distance;
            
            Vector2 appliedForce = new Vector2(0, heightDifference) * floatingForce - new Vector2(0,rb.velocity.y) * dampingForce;

            rb.AddForce(appliedForce);
            
        }
        else if(hit.collider == null)
        {

            rb.velocity = new Vector2(rb.velocity.x, 0);

        }

        else
        {
            rb.AddForce(new Vector2(0, floatingForce));
        }


        //myGameObject.rigidbody.isKinematic = true; freezing object to still potentially without getting rid of the velocity and angularspeed etc.

        if ((new Vector2(rb.transform.position.x, 0f) - origin).magnitude > patrolRange) //floating direction correction
        {
            Vector2 horizontalForce = ((origin - new Vector2(rb.transform.position.x, 0f)).normalized * patrollingForce + new Vector2(-rb.velocity.x,0)) * rb.mass;
            rb.AddForce(horizontalForce);
        }

        else if (speed != minSpeed)
        {
            float difference = minSpeed - speed;
            rb.AddForce(new Vector2(rb.velocity.normalized.x * rb.mass * difference,0));
        }

    }
}
