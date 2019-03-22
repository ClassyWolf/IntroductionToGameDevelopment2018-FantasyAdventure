using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

    public SpriteRenderer crossHair;

	void Start () {
        crossHair = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
        crossHair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = new Vector3(crossHair.transform.position.x, crossHair.transform.position.y, 0f);
    }
}
