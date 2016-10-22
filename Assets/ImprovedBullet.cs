using UnityEngine;
using System.Collections;

public class ImprovedBullet : MonoBehaviour 
{
	private Player player;
	public float damage , lifeTime, speed;
	//DO NOT REMOVE [speed], used by RigidSentry.cs

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		//limits th bullet's lifetime in flight
		destroy();
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		//we hit a smol rock, destroy it
		if(col.tag == "small")
			Destroy (col.gameObject);
		//we hit the player, hurt it
		if(col.tag == "Player")
			player.takeDamage(damage);

	}

	//kills this object after [duration] seconds
	private void destroy(){
		Destroy(this.gameObject, lifeTime);
	}
		
}
