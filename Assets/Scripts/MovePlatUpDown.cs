using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatUpDown : MonoBehaviour {

    public float speed;
    public float yCenter = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, yCenter + Mathf.PingPong(Time.time * speed, 15) - 15 / 2f, transform.position.z);

    }
}
