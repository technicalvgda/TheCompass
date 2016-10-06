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

    private float _dmgPerLoop = 10; //Deals 10 damage
    private float _TestHp = 100;   //Temporary Health stat for testing
    private bool _coroutineActivated = false;

	void Start ()
	{

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
				Debug.Log ("GelType 1 Detected, positive stat boost");
				speedMultiplier = 2f;
				coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
			} 
			else if (type == 2) // NEGATIVE SPD DEBUFF NEBULA
			{
				Debug.Log ("GelType 2 Detected, negative stat boost");
				speedMultiplier = 0.5f;
				coll.gameObject.GetComponent<Rigidbody2D>().velocity *= speedMultiplier;
			}
			else if (type == 3) // DAMAGING NEBULA, block executes when the Nebula is active & the player collides with it
            { 
                if (coll.gameObject.name == "PlayerPlaceholder" && coll.gameObject.activeSelf) 
                {
					GameObject player = coll.gameObject;
                    StartCoroutine(dmgOverTime(player)); //Begin damaging method
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) // revert boost/antiboost effects
	{
		if (coll.gameObject.name == "PlayerPlaceholder" && isActive ())
		{
			if (type == 1)
			{
				Debug.Log ("Reset");
				speedMultiplier = 2f;
			} 
			else if (type == 2) 
			{
				Debug.Log ("Reset");
				speedMultiplier = 0.5f;
			}
            else if (type == 3)
            {
                _coroutineActivated = false;  //Ends damage over time effect
            }
			coll.gameObject.GetComponent<Rigidbody2D>().velocity /= speedMultiplier;
			//remove speed buff/debuff
		}
	}

    IEnumerator dmgOverTime(GameObject player)    //Damage over time function
    {
        _coroutineActivated = true;

        while(_coroutineActivated)
        {
            _TestHp -= _dmgPerLoop;   //Player receives this much damage per iteration
            Debug.Log("CURRENT HP: " + _TestHp);

            if (_TestHp <= 0)
            {
               _coroutineActivated = false;
               Debug.Log("PLAYER RAN OUT OF HP");

               player.SetActive(false);
               player.transform.position = new Vector3(0, 0, 0); //Temporary code for respawning, resets the player obj to origin
               player.SetActive(true);

               _TestHp = 100; //Reset Test Hp, Placeholder code
}

            yield return new WaitForSeconds(.5f); //Wait 1 second and then loop
        }

    }
    bool isActive()
	{
		return active;
	}
}