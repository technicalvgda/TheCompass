using UnityEngine;
using System.Collections;
//This script controls the ship movement through the dialogue exchange scenes
public class DialogueExchangeShipMovement : MonoBehaviour {
	public GameObject player;
	public float speedOfMovement;
	private Rigidbody2D _rb2d;
	// Use this for initialization
	void Start () {
		//Find player and get rigid body
		player = GameObject.FindGameObjectWithTag ("Player");
		_rb2d = player.GetComponent<Rigidbody2D> ();
	}
	void Update()
	{
		//apply force
		_rb2d.AddForce (new Vector2 (speedOfMovement, 0));
	}
}
