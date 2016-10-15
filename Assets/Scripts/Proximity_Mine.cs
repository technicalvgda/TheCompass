using UnityEngine;
using System.Collections;

public class Proximity_Mine : MonoBehaviour {
	public float damage, pushForce, explRadius;

	private CircleCollider2D _col;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log("Entered");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, explRadius);
		if(col.gameObject.tag == "Player"){
			Debug.Log("In Range");
			foreach(Collider2D hit in colliders){
				float dx = hit.transform.position.x - transform.position.x;
				float dy = hit.transform.position.y - transform.position.y;
				Vector2 vect = new Vector2(dx, dy);
				hit.attachedRigidbody.AddForce(vect.normalized * pushForce, ForceMode2D.Impulse);
			}
		}
	}
}
