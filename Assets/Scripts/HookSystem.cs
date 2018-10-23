using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookSystem : MonoBehaviour {

    public GameObject hingeAnchor;
    public DistanceJoint2D playerJoint; //used as the joint
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public PlayerMovement playerMovement;
    private bool attached;
    private Vector2 playerPosition;
    private Rigidbody2D hingeAnchorRb;
    private SpriteRenderer hingeAnchorSprite;

    public LineRenderer ropeRender;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();

    private bool distanceSet;

    private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

    public Vector2 hook;
    public float swingForce = 4f;

    public float climbSpeed = 3f;
    private bool isColliding;

    void Awake()
    {
        //adding and enabling the hook to the player
        playerJoint.enabled = false;
        playerPosition = transform.position;
        hingeAnchorRb = hingeAnchor.GetComponent<Rigidbody2D>();
        hingeAnchorSprite = hingeAnchor.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //how the aiming and crosshair works
        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if(aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        playerPosition = transform.position;

        if(!attached)
        {
            SetCrosshairPosition(aimAngle);

            playerMovement.isSwinging = false;
        }
        else
        {
            playerMovement.isSwinging = true;
            hook = ropePositions.Last();

            crosshairSprite.enabled = false;

            //if the ropepositions has anything stored then do this
            if(ropePositions.Count > 0)
            {
                //fires a raycast form the players position to the last position
                var lastRopePoint = ropePositions.Last();
                var playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint -
                    playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);

                //If the raycast hits something, if it is a polygoncollider then it is returned as a vector
                if (playerToCurrentNextHit)
                {
                    var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
                    if (colliderWithVertices != null)
                    {
                        var closestPointToHit = GetClosestColliderPointFromRayCastHit(playerToCurrentNextHit, colliderWithVertices);

                        //checks that the same position is not wrapped twice
                        if (wrapPointsLookup.ContainsKey(closestPointToHit))
                        {
                            ResetRope();
                            return;
                        }

                        //updates the ropePositions list
                        ropePositions.Add(closestPointToHit);
                        wrapPointsLookup.Add(closestPointToHit, 0);
                        distanceSet = false;
                    }
                }
            }
        }

        HandleInput(aimDirection);

        UpdateRopePositions();

        HandleRopeLength();
    }

    //crosshair enabling and calculation
    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }

    //adding the rope to the hook and calculating the rope position
    private void HandleInput(Vector2 aimDirection)
    {
        if(Input.GetMouseButton(0))
        {
            if (attached) return;
            ropeRender.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            if(hit.collider != null)
            {
                attached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    //small jump after gappling to someting
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    playerJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    playerJoint.enabled = true;
                    hingeAnchorSprite.enabled = true;
                }
            }
            else
            {
                ropeRender.enabled = false;
                attached = false;
                playerJoint.enabled = false;
            }
        }

        if(Input.GetMouseButton(1))
        {
            ResetRope();
        }
    }

    //removing the rope after unattaching
    private void ResetRope()
    {
        playerJoint.enabled = false;
        attached = false;
        playerMovement.isSwinging = false;
        ropeRender.positionCount = 2;
        ropeRender.SetPosition(0, transform.position);
        ropeRender.SetPosition(1, transform.position);
        ropePositions.Clear();
        hingeAnchorSprite.enabled = false;

        wrapPointsLookup.Clear();
    }

    //checking different states of the rope and hook
    private void UpdateRopePositions()
    {
        //returns if the hook is not attached
        if(!attached)
        {
            return;
        }

        //sets the renders positions to the stored number in ropePositions plus one to account for the players position
        ropeRender.positionCount = ropePositions.Count + 1;

        //Loops backwards through ropePositions list except the last one and set the line render to the Vector2
        //position at the current index being looped through
        for (var i = ropeRender.positionCount - 1; i >= 0; i--)
        {
            if(i != ropeRender.positionCount - 1)
            {
                ropeRender.SetPosition(i, ropePositions[i]);

                //sets the anchor to the second to last position where the hinge/anchor should be or if there is only one, 
                //it's set as the anchor point. Configures the distance between player and rope position
                if(i == ropePositions.Count -1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if(ropePositions.Count == 1)
                    {
                        hingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            playerJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        hingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            playerJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }

                //Handels when the anchor is the second to last positin, the poing where the rope attaches to an object
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    hingeAnchorRb.transform.position = ropePosition;
                    if(!distanceSet)
                    {
                        playerJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }

            //Handles setting the ropes last vertex position to the players current position
            else
            {
                ropeRender.SetPosition(i, transform.position);
            }
        }
    }

    //Makes the raycast collder dependet and needs to be adjusted accordingly
    private Vector2 GetClosestColliderPointFromRayCastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {
        //LINQ query, converts the polygon colliders points into vector2 positions
        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));

        //Ordereing the dictionary with the position closest to the players current positon
        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }

    /*void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;
            if (isSwinging)
            {
                animator.SetBool("IsSwinging", true);

                //Normalized direction vector from the player to the point being hooked
                var playerToHookDirection = (hook - (Vector2)transform.position).normalized;

                //direction inversion to fet a perpendicular direction
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                xBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                animator.SetBool("IsSwinging", false);
                if (groundCheck)
                {
                    var groundForce = speed * 2f;
                    xBody.AddForce(new Vector2((horizontalInput * groundForce - xBody.velocity.x) * groundForce, 0));
                    rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
                }
            }
        }
        else
        {
            animator.SetBool("IsSwinging", false);
            animator.SetFloat("Speed", 0f);
        }

        if(!isSwinging)
        {
            if (!groundCheck) return;

            isJumping = jumpInput > 0f;
            if (isJumping)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
            }
        }
    }
    */
    private void HandleRopeLength()
    {
        //looks for vertical axis input and flags either increase of decrease if the playerJoint distance
        if (Input.GetAxis("Vertical") >= 1f && attached && !isColliding)
        {
            playerJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0f && attached)
        {
            playerJoint.distance += Time.deltaTime * climbSpeed;
        }
    }
    
    void onTriggerStay2D(Collider2D colliderStay)
    {
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {
        isColliding = false;
    }
}
 