using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    [SerializeField] private Transform obj;
    private List<GameObject> objects;
    [SerializeField] private int objectCount;
    [SerializeField] private GameObject trigger;
    private bool canSpawn;

	void Start () {

        objects = new List<GameObject>();

        if (trigger == null && canSpawn == false) { canSpawn = true; }

    }

    void SpawnNewObject()
    {
        Transform A = Instantiate(obj, new Vector3(3, 5, 0), obj.transform.rotation);
        A.transform.position = transform.position;
        A.transform.localScale = obj.transform.localScale;

        objects.Add(A.gameObject);
    }

    void FixedUpdate()
    {
        if (trigger != null)
        {
            if (trigger.GetComponent<Switch>().isActivated && canSpawn == false) { canSpawn = true; }
        }


        if (canSpawn)
        {

            for (int n = objects.Count - 1; n >= 0; n--)
            {

                GameObject A = objects[n];

                if (A != null)
                {

                    Transform t = A.GetComponent<Transform>();

                    //Check for visibility

                }

                else { objects.RemoveAt(n); }

            }

            if (objects.Count < objectCount)
            {

                for (int n = 0; n < (objectCount - objects.Count); n++)
                {
                    SpawnNewObject();
                }

            }

        }

    }
	
}
