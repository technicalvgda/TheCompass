/* Script manages the properties for three different types of Nebulas
*  Nebulas are classified into type 1, type 2, and type 3 Nebulas
*/
using UnityEngine;
using System.Collections;

public class Nebula : MonoBehaviour
{
	public int type;
	// type 1 - POSITIVE stat boost
	// type 2 - NEGATIVE stat hit
	public bool active;
	// true means active, false means inactive
	private float speedMultiplier;
    // alter speed change of object going through NebulaGel, depends on type

    private float _dmgPerLoop = 25; //Deals 25 damage when in contact with pain-nebula over time
    private float _dmgRate = 1f; //Rate of damage
    private bool _coroutineActivated = false;

    private GameObject _player;
    private Player _playerCont; //References the player script
    private Vector3 _playerPos;

	void Start ()
	{
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCont = _player.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.name == "PlayerPlaceholder" && isActive ())
		{
			if (type == 1) // POSITIVE SPD BUFF NEBULA
			{
				//Debug.Log ("GelType 1 Detected, positive stat boost");
				speedMultiplier = 2f;
				coll.GetComponent<Player>().nebulaMultiplier = speedMultiplier;
			} 
			else if (type == 2) // NEGATIVE SPD DEBUFF NEBULA
			{
				//Debug.Log ("GelType 2 Detected, negative stat boost");
				speedMultiplier = 0.5f;
                coll.GetComponent<Player>().nebulaMultiplier = speedMultiplier;
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
        else
        {
            if (coll.GetComponent<Rigidbody2D>() != null)
            {
                if (type == 1) // POSITIVE SPD BUFF NEBULA
                {
                    //Debug.Log ("GelType 1 Detected, positive stat boost");
                    speedMultiplier = 2f;
                    coll.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
                }
                else if (type == 2) // NEGATIVE SPD DEBUFF NEBULA
                {
                    //Debug.Log ("GelType 2 Detected, negative stat boost");
                    speedMultiplier = 0.5f;
                    coll.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
                }
            }
        }
	}

	void OnTriggerExit2D(Collider2D coll) // revert boost/antiboost effects
	{
		if (coll.gameObject.name == "PlayerPlaceholder" && isActive ())
		{
			if (type == 1 || type == 2)
			{
				//Debug.Log ("Reset");
				
                coll.GetComponent<Player>().nebulaMultiplier = 1;
            } 
			
            else if (type == 3)
            {
                _coroutineActivated = false;  //Ends damage over time effect
            }
			
			//remove speed buff/debuff
		}
	}

    IEnumerator dmgOverTime(GameObject player)    //Damage over time function
    {
        _coroutineActivated = true;

        while(_coroutineActivated)
        {
            _playerCont.takeDamage(_dmgPerLoop);   //Player receives this much damage per iteration
            Debug.Log("CURRENT HP: " + _playerCont.playerHealth);

            if (_playerCont.playerHealth <= 0)
            {
               _coroutineActivated = false;
               Debug.Log("PLAYER RAN OUT OF HP");

               /*player.SetActive(false);
               player.transform.position = new Vector3(0, 0, 0); //Temporary code for respawning, resets the player obj to origin
               player.SetActive(true);

               _playerCont.playerHealth = 100; //Reset Hp, Placeholder code for testing*/
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