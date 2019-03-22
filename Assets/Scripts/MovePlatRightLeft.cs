using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatRightLeft : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        // Set the x position to loop between 0 and 3
        transform.position = new Vector3(PingPong(Time.time * speed, 15, 24), transform.position.y, transform.position.z);
    }

    //function to change the default starting value of (0, 0, 0) to any value
    float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }
}
