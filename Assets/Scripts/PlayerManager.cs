using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance = null;
    public UIController uIHandler;

    private CharacterStatus characterStatus;
    private PlayerStatus playerStatus;


    public int healthLeft;
    public int hooksLeft;

    public void Start()
    {
        characterStatus = GetComponent<CharacterStatus>();
        playerStatus = GetComponent<PlayerStatus>();
        uIHandler = GetComponent<UIController>();

        healthLeft = characterStatus.healthPoints;
        hooksLeft = playerStatus.hookCounter;
    }


    private void Awake()
    {

        if (instance == null)
        {

            instance = this;

        }

        else if (instance != this)
        {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);

    }

    public void ManageResources()
    {
        healthLeft = characterStatus.healthPoints;
        hooksLeft = playerStatus.hookCounter;

        uIHandler.UpdateResources(characterStatus.healthPoints, playerStatus.hookCounter);
    }


}
