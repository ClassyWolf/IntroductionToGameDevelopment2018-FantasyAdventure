using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLength : MonoBehaviour {

    public float climbSpeed = 3f;
    public HookJoint hookjoint;

    private bool isColliding;

    public void HandleRopeLength()
    {
        //looks for vertical axis input and flags either increase of decrease if the playerJoint distance
        if (Input.GetAxis("Vertical") >= 1f && hookjoint.attached && !isColliding)
        {
            hookjoint.joint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0f && hookjoint.attached)
        {
            hookjoint.joint.distance += Time.deltaTime * climbSpeed;
        }
    }

    void onTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }
}
