using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TetheredObject : MonoBehaviour
{
    bool invincible = false;
    float invincibleTime = 3.0f;

    public bool tetherOn;
    private Color c1 = Color.white;
    private Vector3 playerPosition;
    private LineRenderer lineRen;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRen;
    public int TetheredHealth = 4; //tethered object's health. Gets hit three times and health goes to zero.
    //private string _sceneToLoad;  //holds a specified scene name to load when the player fails this level 
    Player playerScript;

    Sprite[] damageImages;
    int imageIndex = 0;

    // Use this for initialization
    void Start()
    {
        tetherOn = false;
        spriteRen = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        lineRen = gameObject.AddComponent<LineRenderer>();
        lineRen.material = new Material(Shader.Find("Particles/Additive"));
        lineRen.SetWidth(.1f, .1f);
        lineRen.SetColors(c1, c1);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //_sceneToLoad = "MVPScene";  //change this to the specified scene that is to be loaded when the player fails this level
        InitializeSprites();
    }

    // Update is called once per frame
    void Update()
    {
        if (tetherOn == true)
        {
            //get static variable of player position from Player script
            playerPosition = Player.playerPos;
            lineRen.SetPosition(0, playerPosition);
            if ((playerPosition - transform.position).magnitude > 10f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, .5f);
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0;
            }
            if ((playerPosition - transform.position).magnitude < 6f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition * -1, .5f);
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0;
            }
            lineRen.SetPosition(1, transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Debris") && invincible == false) // if tethered object collides with an enemy object
        {
            invincible = true;
            StartCoroutine(DamageDelay());
            Debug.Log("Tethered health is: " + TetheredHealth);
            TetheredHealth -= 1; // tethered health loses 1 health point 

            //change image to show damage
            imageIndex++;
            if(imageIndex < damageImages.Length)
            {
                spriteRen.sprite = damageImages[imageIndex];
            }
            

            if (TetheredHealth <= 0) // if tethered health reaches zero or below
            {
                tetherOn = false;
                Destroy(coll.gameObject); //the collided object goes bye bye
                //FailLevel(); commented out this call so the fail level can be called after commentary
            }
        }
        /*else if (coll.gameObject.name == "PlayerPlaceholder")
		{
			tetherOn = true;
            playerPosition = coll.transform.position;
		} */
        /*
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
        */

    }

    //prevent the tether part from taking damage quickly
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
        yield return null;
    }

    /**
     * When the player fails this level, this method will be invoked to load a different scene
     */
    public void FailLevel()
    {
        //SceneManager.LoadScene(sceneName);

        //set health to 0 to cause game over
        playerScript.setHealth(0);

    }

    public void tether(Vector3 position)
    {
        tetherOn = true;
        playerPosition = position;
    }

    void InitializeSprites()
    {
        if(gameObject.name == "SignalBooster")
        {
            damageImages = Resources.LoadAll<Sprite>("SignalBooster");
            spriteRen.sprite = damageImages[imageIndex];

        }
    }
}
