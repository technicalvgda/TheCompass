using UnityEngine;
using System.Collections;

public class ExplodingDebris : MonoBehaviour 
{
	float distance;
	GameObject player;
	// Use this for initialization
	void Start () 
	{
		distance = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D( Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			player = col.gameObject;
			StartCoroutine (explode ());

		}
	}

	IEnumerator explode()
	{
		yield return new WaitForSeconds (1);
		distance = Vector3.Distance (player.transform.position, transform.position);

		if (distance < 5)
			player.GetComponent<Player> ().playerHealth -= 40;
		else if ( distance >= 5 && distance < 9 )
			player.GetComponent<Player> ().playerHealth -= 20;

		Destroy (gameObject);
	}
}
