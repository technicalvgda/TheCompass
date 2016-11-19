using UnityEngine;
using System.Collections;

public class RigidSentry : MonoBehaviour {
	private LinearPatrol _patrolpattern;
	private Vision _vision;
	private bool _canFire;
	private float _bulletVelocity, _spray;
    public int _offest;

	public Rigidbody2D bullet;
    //The higher the accuracy value, the more deviation there is from center point
	public float accuracy, fireRate;
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
			Rigidbody2D instantProjectile = Instantiate(bullet, new Vector3(transform.position.x + _offest, transform.position.y, 0),transform.rotation) as Rigidbody2D;
            _spray = Random.Range(-accuracy, accuracy);
            instantProjectile.velocity = transform.TransformDirection(new Vector3(_bulletVelocity, _spray, 0 ));
            instantProjectile.transform.right = instantProjectile.velocity;
            //taking the inverse of the firerate to get projectiles per second
            yield return new WaitForSeconds(1/fireRate);
			_canFire = true;
		}
	}
}
