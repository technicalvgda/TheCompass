using UnityEngine;
using System.Collections;

public class scr_obj_movement : MonoBehaviour {

	public float movementspeed = 10f; 

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.right * movementspeed * Time.deltaTime);
	}

}
