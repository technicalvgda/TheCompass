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
    private GameObject mapIcon;
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
        mapIcon = transform.FindChild("MapIcon").gameObject;
        //_sceneToLoad = "MVPScene";  //change this to the specified scene that is to be loaded when the player fails this level
        InitializeSprites();
    }

    // Update is called once per frame
    void Update()
    {
        if (tetherOn == true)
        {
            //disable map icon
            if(mapIcon.activeSelf)
            {
                mapIcon.SetActive(false);
            }
            //get static variable of player position from Player script
            playerPosition = Player.playerPos;
            lineRen.SetPosition(0, playerPosition);
            if ((playerPosition - transform.position).magnitude > 10f)
            {
                //transform.position = Vector3.MoveTowards(transform.position, playerPosition, 1.5f);
                transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime);
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0;
            }
            if ((playerPosition - transform.position).magnitude < 6f)
            {
                //transform.position = Vector3.MoveTowards(transform.position, playerPosition * -1, 1.5f);
                transform.position = Vector3.Lerp(transform.position, -playerPosition, Time.deltaTime);
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0;
            }
            lineRen.SetPosition(1, transform.position);
        }
    }

    
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Enemy") // if tethered object collides with an enemy object
        {
            HandleImpact();
        }
       

    }


    public void HandleImpact()
    {
        if(invincible == false)
        {
            invincible = true;
            StartCoroutine(DamageDelay());
            Debug.Log("Tethered health is: " + TetheredHealth);
            TetheredHealth -= 1; // tethered health loses 1 health point 

            //change image to show damage
            imageIndex++;
            if (imageIndex < damageImages.Length)
            {
                spriteRen.sprite = damageImages[imageIndex];
            }


            if (TetheredHealth <= 0) // if tethered health reaches zero or below
            {
                tetherOn = false;
                if(SceneManager.GetActiveScene().name == "Level 4")
                {
                    FailLevel();
                }
                //Destroy(coll.gameObject); //the collided object goes bye bye
                                          //FailLevel(); commented out this call so the fail level can be called after commentary
            }
        }
       
    }
    //prevent the tether part from taking damage quickly
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
        yield return null;
    }

    public void ShrinkAndDestroy()
    {
        StartCoroutine(ShrinkDestroy());
    }
    IEnumerator ShrinkDestroy()
    {
        Vector3 shrinkVec = new Vector3(0.1f, 0.1f, 0);
        while (transform.localScale.x > 0.1)
        {
            transform.localScale -= shrinkVec;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
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
        if(gameObject.name == "TetheredPowerSource")
        {
            damageImages = Resources.LoadAll<Sprite>("PowerCore");
            spriteRen.sprite = damageImages[imageIndex];
        }
    }
}
