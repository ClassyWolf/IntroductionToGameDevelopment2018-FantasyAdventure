using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepState : MonoBehaviour {

    private static GameObject undestroyableObject;

    void Awake () {

        if (undestroyableObject == null)
        {
            undestroyableObject = this.gameObject;
            DontDestroyOnLoad(undestroyableObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
	
}
