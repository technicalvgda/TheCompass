using UnityEngine;
using System.Collections;


// Drone Vision script is used for drones that tracks players who are in range
public class DroneVision : MonoBehaviour
{

    //public delegate void FollowPlayerAction();
    //public static event FollowPlayerAction FollowPlayer;

    GameObject bullet;
 	public GameObject bulletPrefab;
 	public int bulletSpeed;

    public float visionRange; // range that a drone can find the player
    public float visionConeAngle; // cone angle that drone can detect enemies within the range

    private static GameObject player; // player game object reference
    private CircleCollider2D droneCollider; // drone collider
    private DroneMovementAI droneMovement;
    private bool isLookingForPlayer = false;
    
	// Use this for initialization
	void Start ()
    {
        droneCollider = GetComponent<CircleCollider2D>();
        droneCollider.radius = visionRange; // set the radius of the collider to vision range
        droneMovement = GetComponent<DroneMovementAI>();
	}
	

    // When the player enters the trigger of the drone, it calls the coroutine to look for the player
    void OnTriggerEnter2D(Collider2D _player)
    {
        //Debug.Log("Enter");
        if(_player.CompareTag("Player"))
        {
            player = _player.gameObject;
            if(!isLookingForPlayer)
            {
                isLookingForPlayer = true;
                StartCoroutine(LookForPlayer());
                StartCoroutine(Attack());
                Debug.Log("start Looking");

            }
            
        }
        
    }

    // when the player exit the trigger of the drone, looking for player stops
    void OnTriggerExit2D(Collider2D _player)
    {
        if (_player.CompareTag("Player"))
        {
            //player = null;
            StopCoroutine(LookForPlayer());
            StopCoroutine(Attack());
            isLookingForPlayer = false;
        }
    }

    // When enemy detected in range, drone keeps looking for the player if it is in sight, then starts rotating to the player until player is out of range
    IEnumerator LookForPlayer()
    {

        if (Vector2.Angle(transform.right, player.transform.position - transform.position) < visionConeAngle / 2)
        {
            transform.right = player.transform.position - transform.position;
            if (Vector2.Distance(transform.position, player.transform.position) > 10) // constant will be replaced with attack range
            {
                droneMovement.StartFollowing();
                
            }
                
        }

        yield return new WaitForSeconds(0);

        if (isLookingForPlayer)
        {
            StartCoroutine(LookForPlayer());
        }
        
    }

    IEnumerator Attack()
 	{
 		bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
 		bullet.GetComponent<Bullet> ().parent = gameObject;
 		yield return new WaitForSeconds(1);
 
 		if (player != null)
 		{
 			StartCoroutine(Attack());
 		}
 	}

    public static GameObject GetPlayer()
    {
        return player;
    }
}
