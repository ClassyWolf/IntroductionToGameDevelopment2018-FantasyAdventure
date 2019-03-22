using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private Rigidbody2D playerCharacter;
    [SerializeField] private Transform spawn;

    public void respawn()
    {
        playerCharacter.transform.position = spawn.position;
    }
}
