using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
public Transform Enemy;
//This script will spawn enemies
	void Start ()
	{
//InvokeRepeating will spawn the enemy in seconds. Change the number if you want to increase the spawn time or decrease
		InvokeRepeating ("SpawnEnemy", 3F, 3F);
	}
	public void SpawnEnemy()
	{
//Instantiate spawn the enemy
//Random.Range determine the location of the spawn. I set it to spawn in one spot
		Instantiate(Enemy, new Vector3(Random.Range (-2, -2), Random.Range (3, 3), 0), Quaternion.identity);
	}
}
