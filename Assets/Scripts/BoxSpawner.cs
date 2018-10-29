using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    [SerializeField] private Transform obj;
    private List<GameObject> objects;
    [SerializeField] private int objectCount;
    
	void Start () {
        
        objects = new List<GameObject>();

        for (int n = 0; n < objectCount; n++)
        {

            SpawnNewObject();

        }

	}

    void SpawnNewObject()
    {
        Transform A = Instantiate(obj, new Vector3(3, 5, 0), Quaternion.identity);

        objects.Add(A.gameObject);
    }

    void FixedUpdate()
    {
        
        for(int n=objects.Count-1; n>=0;n--)
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
            for(int n=0; n<(objectCount-objects.Count); n++)
            {
                SpawnNewObject();
            }
        }

    }
	
}
