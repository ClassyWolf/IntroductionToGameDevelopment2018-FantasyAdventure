using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMerger : MonoBehaviour {

    List<Switch> triggers = new List<Switch>();
    public bool isActivated = false;
    private GameObject resetter;

    // Use this for initialization
    void Start () {

        Switch[] komponentit = GetComponentsInChildren<Switch>(true);
        
        foreach (Switch a in komponentit)
        {
            if (a.tag != "Trigger_reset") //this guy is handling the resetting of the system if timer runs out or something else takes place which resets puzzle
            {
                triggers.Add(a);
            }
            else
            {
                resetter = a.gameObject;
            }
        }

        //print(triggers.Count);

	}

    public void PerformResetOnTriggers()
    {
        foreach (Switch a in triggers)
        {
            a.isActivated = false;
        }

        resetter.GetComponent<Switch>().isActivated = false;

    }

    // Update is called once per frame
    void FixedUpdate () {

        isActivated = true;

        foreach(Switch a in triggers)
        {
            if (a.isActivated == false) { isActivated = false; }
        }

        

	}
}
