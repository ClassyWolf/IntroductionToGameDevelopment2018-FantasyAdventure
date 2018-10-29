using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public bool isActivated;
    
    void start()
    {
        isActivated = false;
    }
	
    void OnTriggerEnter2D(Collider2D colliderObject)
    {
        if (colliderObject.tag=="Player" && isActivated==false)
        {
            isActivated = true;

            Vector3 scale = this.transform.localScale;
            this.transform.localScale = new Vector3(-scale.x,scale.y,scale.z);

            print("hit");

        }
    }

}
