using UnityEngine;
using System.Collections;

//This script controls the star objects placement 
//If a star background object hits the collider of the invisible object
//then the star background object is moved so that the stars seem unending
public class DialogueExchangeMoveStars : MonoBehaviour {
	public GameObject player,stars1,stars2,stars3;
	private Vector3 _newPos;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//if other collider is a star object
		if (other.gameObject.tag == "DialogueStars") 
		{
			//update the position of the star object
			other.transform.position = new Vector3 (other.transform.position.x + (240), other.transform.position.y, other.transform.position.z);
		}
	}
}
