using UnityEngine;
using System.Collections;


//Drone Vision script is used for drones that tracks players who are in range
public class DroneVision : MonoBehaviour {

    public float visionRange; // range that a drone can find the player
    public float visionConeAngle; // cone angle that drone can detect enemies within the range

    private GameObject player; // player game object reference
    private CircleCollider2D droneCollider; // drone collider
    
	// Use this for initialization
	void Start ()
    {
        droneCollider = GetComponent<CircleCollider2D>(); 
        droneCollider.radius = visionRange; // set the radius of the collider to vision range
	}
	

    // When the player enters the trigger of the drone, it calls the coroutine to look for the player
    void OnTriggerEnter2D(Collider2D _player)
    {
        //Debug.Log("Enter");
        if(_player.CompareTag("Player"))
        {
            player = _player.gameObject;
            StartCoroutine(LookForPlayer());
        }
        
    }

    // when the player exit the trigger of the drone, looking for player stops
    void OnTriggerExit2D(Collider2D _player)
    {
        if (_player.CompareTag("Player"))
        {
            player = null;
            StopCoroutine(LookForPlayer());
        }
    }

    // When enemy detected in range, drone keeps looking for the player if it is in sight, then starts rotating to the player until player is out of range
    IEnumerator LookForPlayer()
    {
        Debug.Log(Vector2.Angle(transform.right, player.transform.position - transform.position));

        if (Vector2.Angle(transform.right, player.transform.position - transform.position) < visionConeAngle)
        {
            transform.right = player.transform.position - transform.position;
        }

        yield return new WaitForSeconds(0);

        if (player != null)
        {
            StartCoroutine(LookForPlayer());
        }
        
    }
}
