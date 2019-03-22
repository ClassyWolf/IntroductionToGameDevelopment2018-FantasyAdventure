using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{

    // Use this for initialization

    [SerializeField] private Vector3 velocity;
    private bool moving=false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moving = true;
            collision.gameObject.transform.SetParent(transform);  
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moving = false;
            collision.gameObject.transform.SetParent(null);
        }

    }

    void FixedUpdate()
    {
        if (moving)
        {
            transform.position += velocity * Time.deltaTime;
        }

    }

}
