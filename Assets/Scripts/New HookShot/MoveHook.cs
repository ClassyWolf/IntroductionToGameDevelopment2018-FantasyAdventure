using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHook : MonoBehaviour {

    public int flySpeed = 200;
    public float timeToLive = 1;
    //public Rigidbody2D player;

    /*private DistanceJoint2D joint;
    private Vector3 targetPos;*/
    public bool attached = false;

    void Start()
    {
        /*player = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;*/
    }

    void FixedUpdate () {

        if (attached == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * flySpeed);
            
        }
        
        /*if(transform.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            Destroy(gameObject, timeToLive);
            //Destroy(gameObject);
        }*/

        if (Input.GetMouseButtonDown(1))
        {
            gameObject.transform.SetParent(null);
            Destroy(gameObject);
            attached = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pivot")
        {
            transform.SetParent(collision.transform);
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            attached = true;
            Debug.Log("collision");

            /*joint.connectedBody = player;
            joint.enabled = true;
            joint.distance = 0.5f;*/
        }
        else if(collision.gameObject.tag != "Pivot" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Arm" && collision.gameObject.tag != "Hook")
        {
            Destroy(gameObject);
        }
        /*else
        {
            Destroy(gameObject, timeToLive);
        }*/
    }
}
