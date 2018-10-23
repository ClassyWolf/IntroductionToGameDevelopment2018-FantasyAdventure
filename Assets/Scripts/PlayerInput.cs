using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private PlayerMovement playerMovement;



    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Utilize player movements
    private void FixedUpdate()
    {
        playerMovement.PlayerMove();
        playerMovement.Jumping();
    }

}
