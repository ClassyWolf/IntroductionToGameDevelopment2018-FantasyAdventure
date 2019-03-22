using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCleaner : MonoBehaviour {

    public void cleanScene()
    {
        Destroy(GameObject.FindGameObjectWithTag("GM"));
        Destroy(GameObject.FindGameObjectWithTag("Puzzles"));
        Destroy(GameObject.FindGameObjectWithTag("DestroyOnLoad"));
    }
}
