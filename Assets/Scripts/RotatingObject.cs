using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {
       
    float speed = 50.0f;
    public bool direction; 
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (direction == true){transform.Rotate(Vector3.forward * speed * Time.deltaTime);}
        else{transform.Rotate(Vector3.back  * speed  * Time.deltaTime);}
		
	}
}
