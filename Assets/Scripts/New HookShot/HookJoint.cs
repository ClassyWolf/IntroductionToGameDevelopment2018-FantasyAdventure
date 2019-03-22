using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HookJoint : MonoBehaviour {

    //public values
    public float distance = 10f;
    public int hookAmount = 3;
    public LayerMask pivot;
    public DistanceJoint2D joint;
    public bool attached = false;
    public LineRenderer rope;
    public TMPro.TextMeshProUGUI hookText;
    public AudioClip hookAudio;
    public AudioClip releaseAudio;

    //required scripts
    public RopeLength ropeLength;
    public PlayerStatus playerStatus;

    //private values
    private Vector3 targetPos;
    private RaycastHit2D hit;

    private Transform hitTransform;
    private Vector3 hitPoint;

    // Use this for initialization
    void Start () {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (attached == true)
        {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, hitTransform.TransformPoint(hitPoint));
        }

        if (Input.GetMouseButtonDown(1))
        {
            Release();
            
        }

        if(playerStatus.isGrounded == true)
        {
            hookAmount = 3;
        }

        ropeLength.HandleRopeLength();
        hookText.text = "Hooks: " + hookAmount.ToString();
    }

    public void Shoot()
    {
        SoundManager.instance.HookShot(hookAudio);
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, pivot);

        if (hit.collider != null && hookAmount > 0)
        {
            joint.enabled = true;
            joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
            joint.distance = 0.5f;
            attached = true;
            rope.enabled = true;
            hitTransform = hit.collider.transform;
            hitPoint = hitTransform.InverseTransformPoint(hit.point);
            hookAmount -= 1;
            Debug.Log("hit");
        }
    }

    public void Release()
    {
        joint.enabled = false;
        rope.enabled = false;
        attached = false;
        SoundManager.instance.HookRelease(releaseAudio);
    }
}
