using UnityEngine;
using System.Collections;

public class RelayStation : MonoBehaviour 
{
	bool relayComplete;

	// Use this for initialization
	void Start () 
	{
		relayComplete = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (relayComplete)
			//Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "RelayPart")
		{
			Debug.Log ("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
			relayComplete = true;
			Destroy (col.gameObject);
		}
			
	}

}
