using UnityEngine;
using System.Collections;

public class EnemyTractorBeam : MonoBehaviour {
	private Vision _vision;
	private float _tractorlength;
	private RaycastHit2D _tractorStick;
	MoveableObject objectScript;
	private bool _hitPlayer;
	private const float MAX_TRACTOR_LENGTH = 20;
	private const float PULL_SPEED = 1;
	private LineRenderer _tractorLine;
	private Vector3 _playerPos;

	// Use this for initialization
	void Start () 
	{
		_vision = GetComponent<Vision>();
		_tractorLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//renders the line
		Enemy_TractorBeamRender();
		//can see the player
		if(_vision.sight)
		{
			_playerPos = _vision.sightPos;
			Debug.Log(_playerPos);

			//cast a 0 length ray that grows until it either hits the player or the max length
			RaycastHit2D hit = Physics2D.Raycast(transform.position, _playerPos - transform.position, _tractorlength);
			if(_tractorlength < MAX_TRACTOR_LENGTH && !_hitPlayer)
			{
				_tractorlength++;	

			}

			//if another object intersects the beam, it will grab the object instead
			if(!_hitPlayer && hit)
			{
				_tractorStick = hit;
				if(objectScript = _tractorStick.collider.GetComponent<MoveableObject>())
				{
					_hitPlayer = true;
				}
			}

			//uses the initial object hit by the beam
			if(_hitPlayer)
			{
				//Pull in the player here for a short time with increased force
				//_tractorStick.rigidbody.velocity = Vector2.Lerp;
			}


		}
	}

	//Renders a line along the tractor beam
	private void Enemy_TractorBeamRender()
	{
		_tractorLine.enabled = true;
		_tractorLine.SetPosition(0, transform.position);
		_tractorLine.SetColors(Color.red, Color.red);
		Vector2 rayDir = (Vector2)_playerPos - (Vector2)transform.position;
		Vector2 endPoint = (Vector2)transform.position + (rayDir.normalized * MAX_TRACTOR_LENGTH);
		_tractorLine.SetPosition(1, endPoint);
	}



}
