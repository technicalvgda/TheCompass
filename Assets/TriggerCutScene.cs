using UnityEngine;
using System.Collections;

/*
 * Author: Timothy Touch
 * 
 * This script is used for the player ship movement for cutscenes.
 * Attach this script to a gameObject with a collider with "Is Trigger" checked.
 * Attach "EndCutScene" script to child object.
 * 
 * You can essentially chain a bunch of objects together to create a less linear route by attaching this script to
 * the child as well and an "EndCutScene" script to the child of that child.
 * 	
 * 	target - is the child object that is attached to this parent object and the location of
 * 			where you want the ship to go
 * 	speed - is simply the speed the ship travels to the child object
 */

public class TriggerCutScene : MonoBehaviour {

	public Transform target;
	private Collider2D _player;
	private string _playerName = "PlayerPlaceholder";
	private bool _reachedDestination = false;
	public float speed = 10;
	private bool _wasTriggered = false;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!_reachedDestination && _player != null) {
			_player.SendMessage ("cutSceneMovePlayer", speed);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if( !_wasTriggered){
			if (other.name == _playerName) {
				setReachedDestination (false);
				_player = other;
				_player.SendMessage ("setPlayerDestination", (Vector2)target.position);
				_wasTriggered = true;
			}
		}
	}

	public void setReachedDestination( bool hasReached)
	{
		_reachedDestination = hasReached;
		if (_player != null && _reachedDestination == true) {
			_player.SendMessage ("setDisablePlayerControl", false);
		}
	}
}
