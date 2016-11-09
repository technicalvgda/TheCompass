using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	GameObject bullet, bullet1, player;
	public GameObject bulletPrefab;
	DroneVision dVScript;
	public bool tripleShot, currentlyShooting;

	// Use this for initialization
	void Start () 
	{
		dVScript = gameObject.GetComponent<DroneVision> ();
		currentlyShooting = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (dVScript.playerFound == true) 
		{
			if (currentlyShooting == false)
				StartCoroutine (shoot ());
		}
	}

	IEnumerator shoot()
	{
		currentlyShooting = true;
		Instantiate(bulletPrefab, transform.position+(transform.right*2), transform.rotation);

		if (tripleShot) 
		{
			bullet = (GameObject)Instantiate(bulletPrefab, transform.position+(transform.right*2), transform.rotation);
			bullet.transform.Rotate (0, 0, 45);

			bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position+(transform.right*2), transform.rotation);
			bullet1.transform.Rotate (0, 0, -45);
		}

		//bullet.GetComponent<Bullet> ().parent = gameObject;
		yield return new WaitForSeconds(1);

		if (dVScript.playerFound == true)
		{
			StartCoroutine (shoot ());
		}else
			currentlyShooting = false;
	}
}
