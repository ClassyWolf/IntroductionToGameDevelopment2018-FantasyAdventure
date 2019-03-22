using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public bool isActivated = false;
    private Vector3 scale;

    void Start()
    {
        scale =this.transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D colliderObject)
    {
        if (colliderObject.tag=="Player")
        {

            isActivated = true;

            if (this.tag == "Trigger_reset")
            {
                GetComponentInParent<TriggerMerger>().PerformResetOnTriggers();
            }

        }
    }

    void FixedUpdate()
    {
        if (isActivated){ this.transform.localScale = new Vector3(-scale.x, scale.y, scale.z); }
        else { this.transform.localScale = scale; }
    }

}
