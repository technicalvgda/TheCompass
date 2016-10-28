using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	GameObject player;
	public float speed;

    //Vector3 parentDir;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
        //parentDir = transform.parent.transform.right;
    }
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += (transform.right * Time.deltaTime * speed);//( parentDir* Time.deltaTime * speed);
        StartCoroutine (destroy ());
	}
    
	void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Debris")
        {
            Destroy(gameObject);
        }		
	}
  
    
    

	IEnumerator destroy()
	{
		yield return new WaitForSeconds (1);

		Destroy (gameObject);
	}
}
