using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class TakeDamage : MonoBehaviour {

    //only use polygoncolliders for the damaging entities with this code

    PolygonCollider2D Collider;
    [SerializeField] private BoxCollider2D playerCollider;

    void Start () {

        Collider = this.GetComponent<PolygonCollider2D>();

    }
	
	void FixedUpdate () {

        if (playerCollider != null)
        {

            if (Collider.IsTouching(playerCollider))
            {

                //PlayerManager.takeDamage();

                playerCollider.GetComponent<CharacterStatus>().UpdateHealth(-1);

            }

        }

    }
}
