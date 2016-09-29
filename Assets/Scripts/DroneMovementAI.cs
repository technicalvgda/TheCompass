using UnityEngine;
using System.Collections;

public class DroneMovementAI : MonoBehaviour {

    

    public float speed; //speed of the drone

    private RaycastHit2D hit; //raycast hit output
    private GameObject player; // player game object
    private GameObject nonPlayer; // non player game object stored after raycasting
    private bool isFollowing = false; // state of drone

    void Start ()
    {
        DroneVision.FollowPlayer += StartFollowing;
	}
	
    void StartFollowing()
    {
        player = DroneVision.GetPlayer();

        if(!isFollowing) // call the movement coroutine if not already following the player
        {
            isFollowing = true;
            StartCoroutine(Movement());
        }
        
    }

    IEnumerator Movement()
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
            if (nonPlayer != null && Vector2.Distance(transform.position, nonPlayer.transform.position) < 10) //constant will be replaced with radius
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
            StopCoroutine(Movement());
        }
        yield return new WaitForSeconds(0);

        if(isFollowing)
        {
            StartCoroutine(Movement());
        }
        
    }
}
