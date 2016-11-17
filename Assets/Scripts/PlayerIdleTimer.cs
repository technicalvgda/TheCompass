using UnityEngine;
using System.Collections;

public class PlayerIdleTimer : MonoBehaviour {
	private GameObject _player;
	private Rigidbody2D _rb2d;
	private float _idleTimer;
	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
		_rb2d = _player.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_rb2d.velocity == Vector2.zero) {
			_idleTimer += Time.deltaTime;
		} else
			_idleTimer = 0;
		//Debug.Log ("AFK: " + _idleTimer);
	}
	public float getPlayerIdleTime()
	{
		return _idleTimer;
	}
}
