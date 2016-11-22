using UnityEngine;
using System.Collections;

/**Volatile Wreck - Joel Lee
 * An Enemy that explodes when the player enters its radius.
 * damage is the damage dealt to the player
 * pushforce is how hard the blast will move objects and the player (3x for the player)
 * explDiameter is the blast diameter
 **/
public class Proximity_Mine : MonoBehaviour {
	public float damage, pushForce, explDiameter;
	public Sprite spriteMat;

	private SpriteRenderer _sprite;
	private CircleCollider2D _col;
	private Player _player;

	void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		_sprite = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        /*if (Vector2.Distance(_player.transform.position, transform.position) < 7 )
        {
            Destroy(this.gameObject);
            _sprite.sprite = spriteMat;
            _player.takeDamage(damage);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce((_player.transform.position - transform.position).normalized * pushForce * 3f, ForceMode2D.Impulse);
            
        }*/
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		//adds all objects inside the wreck's collider into an array
		Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, explDiameter);
		if(col.gameObject == _player.gameObject)
		{
			//change sprite into an explosion effect
			_sprite.sprite = spriteMat;
			_player.takeDamage(damage);
			//essentially a for loop
			foreach(Collider2D hit in colliders)
			{
				//calculate the direction away from the center of the blast
				float dx = hit.transform.position.x - transform.position.x;
				float dy = hit.transform.position.y - transform.position.y;
				Vector2 vect = new Vector2(dx, dy);

				if(hit.gameObject == _player.gameObject)
				{
					//Apply a stronger force to the player, so they are actually moved rather than nudged for a small amount
					hit.attachedRigidbody.AddForce(vect.normalized * pushForce * 3.0f, ForceMode2D.Impulse);
				}
				else
				{
					//objects behave to force at full force
					//hit.attachedRigidbody.AddForce(vect.normalized * pushForce, ForceMode2D.Impulse);
				}
			}
            //delayed destroy for purpose of explosive effect
            //Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this.gameObject, 0.1f);
           
          
		}
	}
}
