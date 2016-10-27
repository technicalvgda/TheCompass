using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneMovementAI : MonoBehaviour {

    public enum DroneMovementState { Idle, Patrolling, Following, ReturnBase };
    public DroneMovementState droneState;

    public float speed; //speed of the drone

    [Header("Following")]
    public int followDistance;
    private RaycastHit2D hit; //raycast hit output
    private GameObject player; // player game object
    private GameObject nonPlayer; // non player game object stored after raycasting
    private bool isFollowing = false; // state of drone

    [Header("Patrolling")]
    public int patrolDistance; // distance the drone will move before making a turn
    public enum PatrolPattern { Circular, Square, SideBySide, Snake, IdleScan} // patrolling patterns
    public PatrolPattern pattern;
    public float circularRotationAngle; // rotation angle for the circular pattern
    public int idleScanAngle;
    public int idleScanSpeed;
    public enum PatrolDirection { Left, Right}; // direction for square patrol
    public PatrolDirection direction;

    [Header("Fleet")]
    public bool isFleet;
    public List<DroneMovementAI> Fleet = new List<DroneMovementAI>();
    public bool isNotified = false;
    

    ///////////////////////////////////////////
    private bool isPatrolling = false; // state of drone
    private bool isTriggerEntered = false;
    private int tempPatrolDistance;
    private Vector3 initialPositionBeforeTurn;
    private Vector3 initialPositionOnStart;
    private Quaternion initialRotationOnStart;
    private DroneMovementState initialDroneMovementState;
    private bool canRaycast = true;
    private float idleScanAngleCounter;


    void Start ()
    {
        //DroneVision.FollowPlayer += StartFollowing;
        tempPatrolDistance = patrolDistance;
        initialPositionOnStart = transform.position;
        initialRotationOnStart = transform.rotation;
        initialDroneMovementState = droneState;
        if(droneState == DroneMovementState.Patrolling)
        {
            StartPatrolling();
        }
	}
	
    public void StartFollowing()
    {
        player = DroneVision.GetPlayer();

        if(!isFollowing) // call the movement coroutine if not already following the player
        {
            if(isFleet)
            {
                isNotified = true;

                for (int i = 0; i < Fleet.Count; i++)
                {
                    Fleet[i].isNotified = true;
                    Fleet[i].StartFollowing();
                }
                /*
                foreach(var drone in Fleet)
                {
                    if(!drone.isNotified)
                    {
                        drone.isNotified = true;
                        drone.StartFollowing();
                    }
                    
                }*/
            }
            droneState = DroneMovementState.Following;
            isFollowing = true;
            StartCoroutine(FollowPlayer());
        }
        
    }

    void StartPatrolling()
    {
        droneState = DroneMovementState.Patrolling;
        initialPositionBeforeTurn = transform.position;
        idleScanAngleCounter = 0;
        StartCoroutine(Patrol());
    }

    void StartReturningBase()
    {
        //GetComponent<CircleCollider2D>().enabled = false;
        droneState = DroneMovementState.ReturnBase;
        StartCoroutine(ReturnBase());
    }


    IEnumerator FollowPlayer()
    {

        //check the distance between the player and the drone to give a stopping distance
        if(Vector2.Distance(transform.position, player.transform.position) > 5) // constant will be replaced with attack range
        {
            
            hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position); // get the raycast

            //Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

            transform.right = player.transform.position - transform.position; // keep rotating the face of the ship to the player since there is no range for following for now

            if(!hit.transform.CompareTag("Player"))
            {
                nonPlayer = hit.transform.gameObject; // store the non player object
            }
            
            // check the distance between the drone and the non player object to avoid
            if (nonPlayer != null && Vector2.Distance(transform.position, nonPlayer.transform.position) < nonPlayer.GetComponent<CircleCollider2D>().radius * 2) //constant will be replaced with radius
            {

                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.up + transform.right) * 10, speed * Time.deltaTime); // move the drone in the y-axis     
            }
            else
            {
                nonPlayer = null;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // after avoiding, keep following the player
            }

            if (Vector2.Distance(transform.position, player.transform.position) > followDistance)
            {
                StartReturningBase();
                isFollowing = false;
            }
        }
        else
        {
            isFollowing = false;
            StopCoroutine(FollowPlayer());
        }
        yield return new WaitForSeconds(0);

        if(isFollowing && droneState == DroneMovementState.Following)
        {
            StartCoroutine(FollowPlayer());
        }
        
    }

    IEnumerator Patrol()
    {
        if (pattern == PatrolPattern.Circular)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, speed * Time.deltaTime); // for circular patrolling, moves the drone 1 unit
            transform.Rotate(new Vector3(0, 0, circularRotationAngle / 10)); // then rotates the object 1/10 of a given angle, this is effected by the speed of the drone in gameplay
        }

        if (pattern == PatrolPattern.Square)
        {
            if (Vector2.Distance(initialPositionBeforeTurn, transform.position) < patrolDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * patrolDistance), speed * Time.deltaTime); // move the drone for given distance
            }
            else
            {
                //then depends on the direction, turn the drone either right or left
                if (direction == PatrolDirection.Left)
                {
                    transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (direction == PatrolDirection.Right)
                {
                    transform.Rotate(new Vector3(0, 0, -90));
                }

                initialPositionBeforeTurn = transform.position;
            }
        }

        if (pattern == PatrolPattern.SideBySide)
        {
            if (Vector2.Distance(initialPositionBeforeTurn, transform.position) < patrolDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * patrolDistance), speed * Time.deltaTime); // move the drone for given distance
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, transform.rotation.z + 180 * -1)); // rotate back the drone

                initialPositionBeforeTurn = transform.position;
            }
        }

        if (pattern == PatrolPattern.Snake) // will have an implementation defining the borders of patrol area
        {
            Debug.DrawRay(transform.right + transform.position, (transform.position + transform.right * tempPatrolDistance) - transform.position);

            if (Vector2.Distance(initialPositionBeforeTurn, transform.position) < patrolDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * patrolDistance), speed * Time.deltaTime); // move the drone for given distance
            }
            else
            {

                PickRandomDirection();

                initialPositionBeforeTurn = transform.position;

                patrolDistance = tempPatrolDistance;
                hit = Physics2D.Raycast(transform.right + transform.position, (transform.position + transform.right * tempPatrolDistance * 2) - transform.position); // get the raycast
                
                if(hit.transform)
                {
                    if (hit.distance < patrolDistance)
                    {
                        tempPatrolDistance = patrolDistance;
                        patrolDistance = Mathf.CeilToInt(hit.distance / 3);
                    }
                }
                
                
            }
        }

        if(pattern == PatrolPattern.IdleScan)
        {

            if(idleScanAngleCounter > 0 && idleScanAngleCounter < idleScanAngle)
            {
                idleScanAngleCounter += idleScanSpeed / 10f;
                transform.Rotate(new Vector3(0, 0, idleScanSpeed / 10f));

                if(idleScanAngleCounter >= idleScanAngle)
                {
                    idleScanAngleCounter = -idleScanAngle;
                }
            }
            else
            {
                idleScanAngleCounter += idleScanSpeed / 10f;
                transform.Rotate(new Vector3(0, 0, -idleScanSpeed / 10f));
            }
            
        }
            yield return new WaitForSeconds(0);

        if(droneState == DroneMovementState.Patrolling)
        {
            StartCoroutine(Patrol());
        }


        
    }

    IEnumerator ReturnBase()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialPositionOnStart, speed * Time.deltaTime); // move the drone for given distance
        transform.right = initialPositionOnStart - transform.position;

        yield return new WaitForSeconds(0);

        if (Vector2.Distance(initialPositionOnStart, transform.position) > 0)
        {
            StartCoroutine(ReturnBase());
        }
        else
        {
            //GetComponent<CircleCollider2D>().enabled = true;
            isNotified = false;
            transform.rotation = initialRotationOnStart;
            StartPatrolling(); // this will change to an event for a dynamic call or a plain if statement
        }
    }

    void PickRandomDirection()
    {
        switch (Mathf.FloorToInt(Random.Range(0, 3)))
        {
            case 0:
                transform.Rotate(new Vector3(0, 0, 90));
                break;
            case 1:
                transform.Rotate(new Vector3(0, 0, -90));
                break;
            case 2:
                break;
        }

        canRaycast = true;
    }


    void RaycastInFront(Transform _object)
    {
        if (canRaycast)
        {
            canRaycast = false;

            hit = Physics2D.Raycast(transform.right + transform.position, (transform.position + transform.right * tempPatrolDistance * 2) - transform.position); // get the raycast
            if (hit.transform)
            {
                if (hit.distance < tempPatrolDistance)
                {
                    PickRandomDirection();
                }
            }

        }
        
        
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        if (!_object.transform.CompareTag("Player"))
        {
            RaycastInFront(_object.transform);
        }
    }

    void OnTriggerStay2D(Collider2D _object)
    {
        if (_object.transform.CompareTag("Debris"))
        {
            if (Vector2.Distance(_object.transform.position, transform.position) < _object.GetComponent<CircleCollider2D>().radius + 1)
            {


                RaycastInFront(_object.transform);

            }

        }
    }

    void OnParticleTrigger()
    {
        Debug.Log("hello");
    }
}
