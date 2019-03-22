using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour {

    public LayerMask pivotPoint;
    public float fireDistance = 100;
    public Transform HookShotPrefab;
    public MoveHook moveHook;
    public float effectSpawnRate = 10;
    //public DistanceJoint2D playerJoint;
    //public LineRenderer ropeRender;

    private Transform firePoint;
    private float timeToSpawnEffect = 0;
    private DistanceJoint2D joint;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint availabe");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (moveHook.attached == true)
        {
            Debug.Log("ATTACHED!");
        }
    }

    void Shoot()
    {
        //Checking the position of the mouse in the context of the world
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, fireDistance, pivotPoint);
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1/effectSpawnRate;
        }      
        Debug.DrawRay(firePointPosition, (mousePosition - firePointPosition), Color.red);
        if(hit.collider != null)
        {
            Debug.DrawRay(firePointPosition, (mousePosition - firePointPosition), Color.green);
        }
    }

    void Effect()
    {
        Transform hookShotClone = Instantiate(HookShotPrefab, firePoint.position, firePoint.rotation);
        /*Vector2 joint = new Vector2(hookShotClone.position.x, hookShotClone.position.y);
        playerJoint.anchor = joint;*/
        //Rigidbody2D hookSotrb = hookShotClone.GetComponent<Rigidbody2D>();
        //rigidBody.AddForce(new Vector2(10.0f, 0f));
    }
}
