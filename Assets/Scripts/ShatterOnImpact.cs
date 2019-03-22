using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnImpact : MonoBehaviour
{

    [SerializeField] private float maximumForce;
    private Vector2 lastSpeed;
    private new Rigidbody2D rigidbody;

    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();

        if (maximumForce == 0) { maximumForce = Physics2D.gravity.magnitude*rigidbody.mass; }

    }

    void FixedUpdate()
    {

        float appliedForce = (rigidbody.velocity-lastSpeed).magnitude*rigidbody.mass;

        //print(appliedForce);

        if (appliedForce > maximumForce)
        {

            Destroy(gameObject);

        }

        else
        {

            lastSpeed = rigidbody.velocity;

        }

    }

}
