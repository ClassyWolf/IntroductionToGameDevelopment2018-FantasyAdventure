using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    private bool isActivated;

    void start()
    {
        isActivated = false;
    }
	
    void OnTriggerEnter2D(Collider2D colliderObject)
    {
        if (colliderObject.tag=="Player" && isActivated==false)
        {
            isActivated = true;

            print("hit");

        }
    }

}
