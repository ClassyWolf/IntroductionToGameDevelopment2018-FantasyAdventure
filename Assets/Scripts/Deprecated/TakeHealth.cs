using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealth : MonoBehaviour
{

    private new PolygonCollider2D collider;
    public BoxCollider2D playerCollider;

    // Use this for initialization
    void Start()
    {

        collider = this.GetComponent<PolygonCollider2D>();

    }

    public void SetPlayerCollider(string target)
    {
        this.playerCollider = GameObject.Find(target).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerCollider != null)
        {

            if (collider.IsTouching(playerCollider))
            {
                //PlayerManager.takeHealth();

                playerCollider.GetComponent<CharacterStatus>().UpdateHealth(1);
                Destroy(this.gameObject);

            }

        }

    }
}
