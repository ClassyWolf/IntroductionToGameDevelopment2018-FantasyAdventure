using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpSpeed;

    private void FixedUpdate()
    {
        if (Input.GetButton("Vertical"))
        {
            transform.localPosition += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetButtonDown("Jump") && transform.position.y <= 0.5)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
        }
    }
}
