using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    [SerializeField] private int movementType;
    [SerializeField] private float incrementAmount;
    [SerializeField] private GameObject activator;
    public Vector2 offsetVector;
    private Vector2 origin;

    private float pathCompletionPercentage;
    public bool isActivated { get; private set; }

    // Use this for initialization
    void Start () {

        isActivated = false;

        origin = this.transform.position;

        if (incrementAmount <= 0){ incrementAmount = 0.05f; }
        else if(incrementAmount > 1) { incrementAmount = 1; }

        pathCompletionPercentage = 0;

        if (movementType <= 0 || movementType > 2) { movementType = 1; }
        //1: Linear onetime deal with holdvalue; 3: Back and forth Linear movement; 

        if (activator == null) { isActivated = true; }

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (activator != null)
        {
            isActivated = activator.GetComponent<TriggerMerger>().isActivated;
        }

        if (isActivated == true)
        {

            if (movementType == 1)
            {

                pathCompletionPercentage += incrementAmount;

                if (pathCompletionPercentage >= 1)
                {
                    pathCompletionPercentage = 1;
                }

                transform.position = origin + offsetVector * pathCompletionPercentage;

            }

            else if (movementType == 2)
            {

                pathCompletionPercentage += incrementAmount;

                if (pathCompletionPercentage >= 1||pathCompletionPercentage<=0)
                {
                    if (pathCompletionPercentage > 1)
                    {
                        pathCompletionPercentage = 1;
                    }
                    else if (pathCompletionPercentage < 0)
                    {
                        pathCompletionPercentage = 0;
                    }
                    
                    incrementAmount *= -1;
                }

                transform.position = origin + offsetVector * pathCompletionPercentage;

            }
            
        }

        else if(isActivated==false && pathCompletionPercentage>0)
        {
            pathCompletionPercentage -= incrementAmount;

            if (pathCompletionPercentage < 0)
            {
                pathCompletionPercentage = 0;
            }

            transform.position = origin + offsetVector * pathCompletionPercentage;

        }

	}
}
