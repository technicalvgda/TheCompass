using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
public GameObject Enemy;
//This script will spawn enemies
	void Start () 
	{
//Replace the "Sprite" with the actual enemy of the game
//InvokeRepeating will spawn the enemy in seconds. Change the number if you want to increase the spawn time or decrease
		Enemy = GameObject.Find("Sprite");
		InvokeRepeating ("SpawnSprite", 3F, 3F);
	}

	public void SpawnSprite()
	{
//Instantiate spawn the enemy
//Random.Range determine the location of the spawn. I set it to spawn in one spot
		Vector3 position = new Vector3(Random.Range(-8.0F, -8.0F), Random.Range(5F, 5F), 0);
		Instantiate(Resources.Load("Sprite"), position, Quaternion.identity);
	}
}
