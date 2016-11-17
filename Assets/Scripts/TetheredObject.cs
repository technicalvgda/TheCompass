using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TetheredObject : MonoBehaviour
{
	private bool tetherOn;
	private Color c1 = Color.white;
	private Vector3 playerPosition;
	private LineRenderer lineRen;
    public int TetheredHealth = 3; //tethered object's health. Gets hit three times and health goes to zero.
    private string _sceneToLoad;  //holds a specified scene name to load when the player fails this level 

    // Use this for initialization
    void Start ()
	{
		tetherOn = false;
		lineRen = gameObject.AddComponent<LineRenderer>();
		lineRen.material = new Material(Shader.Find("Particles/Additive"));
		lineRen.SetWidth(.1f , .1f);
		lineRen.SetColors (c1, c1);
        _sceneToLoad = "MVPScene";  //change this to the specified scene that is to be loaded when the player fails this level
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (tetherOn == true)
		{
            //get static variable of player position from Player script
            playerPosition = Player.playerPos;
			lineRen.SetPosition (0, playerPosition);
			if ((playerPosition - transform.position).magnitude > 10f)
			{
				transform.position = Vector3.MoveTowards (transform.position, playerPosition, .5f);
				GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				GetComponent<Rigidbody2D>().angularVelocity = 0;
			}
			if ((playerPosition - transform.position).magnitude < 6f)
			{
				transform.position = Vector3.MoveTowards (transform.position, playerPosition * -1, .5f);
				GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
				GetComponent<Rigidbody2D> ().angularVelocity = 0;
			}
			lineRen.SetPosition (1, transform.position);
		}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Enemy") // if tethered object collides with an enemy object
        {
            Debug.Log("Tethered health is: " + TetheredHealth);
            TetheredHealth -= 1; // tethered health loses 1 health point 
            if (TetheredHealth <= 0) // if tethered health reaches zero or below
            {
                tetherOn = false;
                Destroy(coll.gameObject); //the collided object goes bye bye
                FailLevel(_sceneToLoad);
            }
        }
		else if (coll.gameObject.name == "PlayerPlaceholder")
		{
			tetherOn = true;
            playerPosition = coll.transform.position;
		} 
		else
		{
            Debug.Log("Tethered health is: " + TetheredHealth);
            TetheredHealth -= 1; // tethered health loses 1 health point 
			if (TetheredHealth <= 0) // if tethered health reaches zero or below
			{
				tetherOn = false;
				Destroy (coll.gameObject); //the collided object goes bye bye
                FailLevel(_sceneToLoad);
			}
		}

    }

    /**
     * When the player fails this level, this method will be invoked to load a different scene
     */
    void FailLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
