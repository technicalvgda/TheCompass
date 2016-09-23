using UnityEngine;
using System.Collections;
/* LaserEmitter.cs
 * 
 * This script should draw an indefinite line. The line with will have damage falloff for enemies and the environment, but not the player (instant death).
 * When the laser hits an asteroid it will split the asteroid into multiple pieces.
 * 
 */
public class LaserEmitter : MonoBehaviour {
	public LineRenderer line;
	public float distScale;

	private Material _lineMat;
	private float _rayDamage;
	private GameObject _player;
	private RaycastHit _hit;
	private Vector3 _rayDirection, _startVec, _startVecFwd;
	private Player _playerscript;
	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		line.GetComponent<Renderer>().material = _lineMat;
		line.SetVertexCount(2);
		line.SetWidth(0.1f, 0.25f);

		_player = GameObject.FindGameObjectWithTag("Player");
		_playerscript = _player.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {

		_startVec = transform.position;	//ray origins
		_startVecFwd = transform.right; //ray direction
		if(Physics.Raycast(_startVec, _startVecFwd, out _hit, Mathf.Infinity)){
			Debug.DrawRay(_startVec, _startVecFwd, Color.black, 1.0f);
			line.enabled = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, _hit.point - _hit.normal);

			//TODO: Make the damage have a cooldown using the coroutine fire methods.
			//Hitting the player
			if(_hit.collider.gameObject == _player){
				//player takes damage
				Debug.Log("Player Hit");
				_playerscript.takeDamage(10000.0f); //the laser hit is lethal
			}
			//hitting an enemy or the asteroid, change the tags when necessary
			if(_hit.collider.gameObject.tag == "Enemy" || _hit.collider.gameObject.tag == "SplitAsteroid"){
				Debug.Log("Hostile hit");
				//_hit.collider.gameObject.takeDamage(_rayDamage);
			}
		}

	}
}
