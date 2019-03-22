using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    private static GameMaster gm;
    public Vector2 LastCheckPoint;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gm);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
