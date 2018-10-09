using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrollingMovement : MonoBehaviour {

    public float patrollingForce;
    public int patrolRange;
    public float minSpeed;
    public float smoothingFactor;

    public float floatHeight;

    private Vector3 oldScale;
    private Vector2 origin;

    public Rigidbody2D rb;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        oldScale = this.transform.localScale;
        origin = new Vector2(this.transform.position.x, 0f);

        rb.AddForce(new Vector2(50f, 0f));

    }

    // Update is called once per frame
    void FixedUpdate () {

        float speed = rb.velocity.magnitude;
        Vector3 scale = rb.transform.localScale;
        Vector2 normalizedVelocity = rb.velocity.normalized;
        Vector3 offset = new Vector3(-scale.x*(normalizedVelocity.x/normalizedVelocity.x),0,0);


        if (rb.velocity.x < 0f) //to determine the orientation of the sprite
        {
            this.transform.localScale = new Vector3(oldScale.x, oldScale.y, oldScale.z);
        }
        else if (rb.velocity.x > 0f)
        {
            this.transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
        }



        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset + new Vector3(0, -scale.y / 2), -Vector3.up);

        if (hit.distance!=floatHeight && hit.collider != null)
        {

            float distance = floatHeight - hit.distance;
            float multiplier = Mathf.Pow(hit.distance / floatHeight,smoothingFactor);

            Vector2 floatingForce = (new Vector2(0, distance) + new Vector2(0, -rb.velocity.y) - Physics2D.gravity) * rb.mass / multiplier;

            rb.AddForce(floatingForce);
            
        }
        else if(hit.collider == null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            Vector2 floatingForce = -Physics2D.gravity * rb.mass;

            rb.AddForce(floatingForce);
        }


        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - offset + new Vector3(0, -scale.y / 2), -Vector3.up); //to calculate the floating forces to correct the height

        if (hit2.distance != floatHeight && hit2.collider != null)
        {

            float distance = floatHeight - hit2.distance;
            float multiplier = Mathf.Pow(hit2.distance / floatHeight, smoothingFactor);

            Vector2 floatingForce = (new Vector2(0, distance) + new Vector2(0, -rb.velocity.y) - Physics2D.gravity) * rb.mass / multiplier;

            rb.AddForce(floatingForce);

        }
        else if (hit2.collider == null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            Vector2 floatingForce =  -Physics2D.gravity * rb.mass;

            rb.AddForce(floatingForce);
        }



        /*if (speed > 0f) //surface height difference prediction by using angled raycast
        {

        }*/

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
