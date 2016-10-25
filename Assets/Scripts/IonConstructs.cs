/* Script manages the properties for three different types of Nebulas
*  Nebulas are classified into type 1, type 2, and type 3 Nebulas
*/
using UnityEngine;
using System.Collections;

public class IonConstructs : MonoBehaviour
{
    public int type;
    // type 1 - POSITIVE stat boost
    // type 2 - NEGATIVE stat hit
    public bool active;
    // true means active, false means inactive
    private float speedMultiplier;
    // alter speed change of object going through NebulaGel, depends on type

    private float _dmgPerLoop = 25; //Deals 25 damage when in contact with pain-nebula over time
    private float _dmgRate = .1f; //Rate of damage
    private bool _coroutineActivated = false;

    private GameObject _player;
    private Player _playerCont; //References the player script
    private Vector3 _playerPos;

    public bool checkTrigger1 = false;  //boolean value to check if player collides with speed up ion construct
    public bool checkTrigger2 = false;  //boolean value to check if player collides with slow down ion construct
    public Vector2 myVector; //stores the vector when the player collides with either the speed or slow ion construct
    public Vector2 original; //stores the vector before the player collides with the ion constructs, for comparison testing

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCont = _player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //check triggers to execute either acceleration or deceleration
        if (checkTrigger1 && !checkTrigger2)
        {
            original = (myVector /= speedMultiplier);  //player's original velocity before touching the ion constructs
            float currentSpeed = myVector.magnitude;  //magnitude of the player's current velocity vector
            float newSpeed = currentSpeed - 10 * Time.deltaTime;  //new speed to attain after touching speed up ion construct
            if (newSpeed < 0)
            {
                newSpeed = 0;  //do not let the new speed become negative if current speed is too low
            }
            if (myVector.magnitude > original.magnitude)
            {
                myVector = myVector.normalized * newSpeed;  //decelerate player every frame after getting a speed boost
            }
            else
            {
                checkTrigger1 = false;  //when player no longer has the speed boost, revert boolean for future collisions
            }
        }
        else if (checkTrigger2 && !checkTrigger1)
        {
            original = (myVector /= speedMultiplier);
            float currentSpeed = myVector.magnitude;
            float newSpeed = currentSpeed + 10 * Time.deltaTime;  //increase speed to make it normal speed after slowing down
            if (newSpeed < 0)
            {
                newSpeed = 0;   //do not let speed become negative
            }
            if (myVector.magnitude < original.magnitude)
            {
                myVector = myVector.normalized * newSpeed;  //accelerate player every frame until its velocity returns to normal speed
            }
            else
            {
                checkTrigger2 = false;  //when player no longer has speed impediment, revert boolean for future collisions
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "PlayerPlaceholder" && isActive())
        {
            if (type == 1) // POSITIVE SPD BUFF NEBULA
            {
                checkTrigger1 = true;
                Debug.Log("GelType 1 Detected, positive stat boost");
                speedMultiplier = 1.5f;
                myVector = coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
            }
            else if (type == 2) // NEGATIVE SPD DEBUFF NEBULA
            {
                Debug.Log("GelType 2 Detected, negative stat boost");
                speedMultiplier = 0.5f;
                coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
            }
            else if (type == 3) // DAMAGING NEBULA, block executes when the Nebula is active & the player collides with it
            {
                if (coll.gameObject.tag == "Player" && coll.gameObject.activeSelf)
                {
                    GameObject player = coll.gameObject;
                    //  _playerPos = player.transform.position; //Saves the point of collision
                    StartCoroutine(dmgOverTime(player)); //Begin damaging method
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll) // revert boost/antiboost effects
    {
        if (coll.gameObject.name == "PlayerPlaceholder" && isActive())
        {
            if (type == 1)
            {
                Debug.Log("Reset");
                speedMultiplier = 1.5f;
            }
            else if (type == 2)
            {
                Debug.Log("Reset");
                speedMultiplier = 0.5f;
            }
            else if (type == 3)
            {
                _coroutineActivated = false;  //Ends damage over time effect
            }
        }
    }

    IEnumerator dmgOverTime(GameObject player)    //Damage over time function
    {
        _coroutineActivated = true;

        while (_coroutineActivated)
        {
            _playerCont.takeDamage(_dmgPerLoop);   //Player receives this much damage per iteration
            Debug.Log("CURRENT HP: " + _playerCont.playerHealth);

            if (_playerCont.playerHealth <= 0)
            {
                _coroutineActivated = false;
                Debug.Log("PLAYER RAN OUT OF HP");

                player.SetActive(false);
                player.transform.position = new Vector3(0, 0, 0); //Temporary code for respawning, resets the player obj to origin
                player.SetActive(true);

                _playerCont.playerHealth = 100; //Reset Hp, Placeholder code for testing
            }

            yield return new WaitForSeconds(_dmgRate); //Wait .25 seconds and then loop
        }

    }
    /*
    void resetPosition()//Will spawn the player outside of the pain Nebula
    {

    }
    */
    bool isActive()
    {
        return active;
    }
}