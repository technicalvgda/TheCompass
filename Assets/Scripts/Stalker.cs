using UnityEngine;
using System.Collections;

public class Stalker : MonoBehaviour {
    private GameObject _player;
    private bool _canFire = true;
    private float _bulletVelocity;

    public Rigidbody2D bullet;
    public float detectionRange = 200.0f;
    public float attRange, stopDist; //values to control when the stalker will attack, and when it will stop advancing on the player
    public float fireRate = 0.75f, speed = 7.3f; //how often we can fire, how fast we move

	void Start () {
        //_vision = GetComponent<Vision>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _bulletVelocity = bullet.GetComponent<ImprovedBullet>().speed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.right = _player.transform.position - transform.position;
        //player is within firing range and in sight
        if(Vector2.Distance(transform.position, _player.transform.position) <= attRange)
        {
            StartCoroutine(fire());
        }
        //we aren't too close to the player, so we move
        float distToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        Debug.Log(distToPlayer);
        if (distToPlayer >= stopDist && distToPlayer < detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.smoothDeltaTime);
        }
	}

    private IEnumerator fire()
    {
        if (_canFire)
        {
            _canFire = false;
            Rigidbody2D instantProjectile = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
            instantProjectile.velocity = transform.TransformDirection(new Vector3(_bulletVelocity, 0, 0));
            //taking the inverse of the firerate to get projectiles per second
            yield return new WaitForSeconds(1 / fireRate);
            _canFire = true;
        }
    }
}
