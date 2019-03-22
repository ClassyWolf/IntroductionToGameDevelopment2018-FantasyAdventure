using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFollow : MonoBehaviour {

    public int rotationOffset = 90;

	void Update () {
        // Subtracting the player from the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        //Finding the right angle between the vector and the x axis and converting it to degrees
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotz + rotationOffset);
    }
}
