using UnityEngine;
using System.Collections;

public class DroneMovementAI : MonoBehaviour
{

    public enum DroneMovementState { Idle, Patrolling, Following };
    public DroneMovementState droneState;

    public float speed; //speed of the drone


    [Header("Following")]
    private RaycastHit2D hit; //raycast hit output
    private GameObject player; // player game object
    private GameObject nonPlayer; // non player game object stored after raycasting
    private bool isFollowing = false; // state of drone

    [Header("Patrolling")]
    public int patrolDistance;
    public enum PatrolStyle { Circular, Square, SideBySide}
    public PatrolStyle style;
    public float circularRotationAngle;
    public enum PatrolDirection { Left, Right};
    public PatrolDirection direction;

    private bool isPatrolling = false; // state of drone
    private Vector3 initialPositionBeforeTurn;


    void Start ()
    {
        //DroneVision.FollowPlayer += StartFollowing;

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
            droneState = DroneMovementState.Following;
            isFollowing = true;
            StartCoroutine(FollowPlayer());
        }
        
    }

    void StartPatrolling()
    {
        droneState = DroneMovementState.Patrolling;
        initialPositionBeforeTurn = transform.position;
        StartCoroutine(Patrolling());
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

    IEnumerator Patrolling()
    {
        if (style == PatrolStyle.Circular)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, circularRotationAngle / 10));
        }

        if (style == PatrolStyle.Square)
        {
            if (Vector3.Distance(initialPositionBeforeTurn, transform.position) < patrolDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * patrolDistance), speed * Time.deltaTime);
            }
            else
            {
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

        if (style == PatrolStyle.SideBySide)
        {
            if (Vector3.Distance(initialPositionBeforeTurn, transform.position) < patrolDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * patrolDistance), speed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, transform.rotation.z + 180 * -1));

                initialPositionBeforeTurn = transform.position;
            }
        }


        yield return new WaitForSeconds(0);

        if(droneState == DroneMovementState.Patrolling)
        {
            StartCoroutine(Patrolling());
        }
        
    }
}
