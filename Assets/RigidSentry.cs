using UnityEngine;
using System.Collections;

public class RigidSentry : MonoBehaviour {
	private LinearPatrol _patrolpattern;
	private Vision _vision;
	private bool _canFire;
	private float _bulletVelocity;

	public Rigidbody2D bullet;
	public float sprayAngle, fireRate;
	// Use this for initialization
	void Start () {
		_canFire = true;
		_patrolpattern = GetComponent<LinearPatrol>();
		_vision = GetComponent<Vision>();
		_bulletVelocity = bullet.GetComponent<ImprovedBullet>().speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(_vision.sight){
			_patrolpattern.active = false;
			StartCoroutine(fire());
		}else{
			_patrolpattern.active = true;
		}
	}

	private IEnumerator fire(){
		if(_canFire){
			_canFire = false;
			Rigidbody2D instantProjectile = Instantiate(bullet,transform.position,transform.rotation) as Rigidbody2D;
			instantProjectile.velocity = transform.TransformDirection(new Vector3(0, _bulletVelocity , 0 ));

			//taking the inverse of the firerate to get projectiles per second
			yield return new WaitForSeconds(1/fireRate);
			_canFire = true;
		}
	}
}
