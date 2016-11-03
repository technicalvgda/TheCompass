using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    //Non Rigid-Body version of Bullet

	//private Player _player;
	public float speed, damage, lifetime;
	void Start ()
	{
		//_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //parentDir = transform.parent.transform.right;
        Destroy(this.gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += (transform.right * Time.deltaTime * speed);//( parentDir* Time.deltaTime * speed);
	}
    
	void OnCollisionEnter2D(Collision2D col)
	{
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().takeDamage(damage);
        }
        Destroy(gameObject);	
	}
  
}
