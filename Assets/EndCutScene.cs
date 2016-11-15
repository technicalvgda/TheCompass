using UnityEngine;
using System.Collections;

/*
 * Author: Timothy Touch
 * Attach this script to the child object with a collider with "Is Trigger" checked.
 * Parent should have "TriggerCutScene" script
 * 
 * This script is used to signal when the player has reached their destination and effectively
 * reenabling player control.
 * 
 */

public class EndCutScene : MonoBehaviour {

	private string _playerName = "PlayerPlaceholder";
	private GameObject _parent;

	// Use this for initialization
	void Start ()
	{
		_parent = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == _playerName) {
			_parent.SendMessage("setReachedDestination", true);
		}
	}
}
