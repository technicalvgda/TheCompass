using UnityEngine;
using System.Collections;


public class Teleporter : MonoBehaviour {
    public bool canTeleport, runOnce;
    public GameObject enemy;
    public Random ran;
    public int xPos, yPos;
 
	// Use this for initialization
	void Start () {
        canTeleport = runOnce =  true;
        xPos = Random.Range(-100, 100);
        yPos = Random.Range(-100, 100);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xPos, yPos,0), (20 * Time.deltaTime));
        Debug.Log("Going to: (" + xPos + ", " + yPos + ")");

        if (transform.position == new Vector3(xPos, yPos, 0) && canTeleport && runOnce)
        {
            // Calls subroutine so that the drone can be teleported
            // In this same coroutine, another coroutine is called
            // so that new coordinates are generated
            StartCoroutine(TeleportEnemy(3.0f));
        }
		else if(transform.position == new Vector3(xPos, yPos, 0) && !canTeleport)
        {
            // If the teleporter ended up in another object, generate new Coordinates quicker
            StartCoroutine(GenerateNewCoordinates(2.0f));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the teleporter is colliding with an object,
        // the enemy can not teleport to that location
        canTeleport = false;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // Resets the canTeleport variable so that the drone can spawn
        // Once the destination has been reached
        canTeleport = true;
    }

    /*
        Teleports the enemy to an open spot
     */
    IEnumerator TeleportEnemy(float time)
    {
        runOnce = false;
        bool firstRun = true;
        Debug.Log("Teleporting...");
        yield return new WaitForSeconds(time);

        if (firstRun)
        {
            // Sets the x and y position of the drone to equal the teleporters
            // x and y
            enemy.transform.position = transform.position;

            // Generates new Coordinates
            StartCoroutine(GenerateNewCoordinates(10.0f));
            firstRun = false;
        }
    
    }

    /*
      Generates new coordinates so that the teleporter can 
      contine to teleport the enemy
     */
    IEnumerator GenerateNewCoordinates(float time)
    {
        runOnce = false;
        yield return new WaitForSeconds(time);
        Debug.Log("New Coordinates Received!");

        bool firstRun = true;

        if (firstRun)
        {
            xPos = Random.Range(-100, 100);
            yPos = Random.Range(-100, 100);
            firstRun = false;
            runOnce = true;
        }
           
        
    }
}