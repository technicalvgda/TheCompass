using UnityEngine;
using System.Collections;
//This script will spawn enemies
public class SpawnEnemies : MonoBehaviour
{
    //the enemy to spawn at the point
    public GameObject Enemy;

    //how long to wait before spawning
    public float spawnTime = 3.0f;

    //checks if the spawner should be testing for death
    private bool enemySpawned = false;
    //stores the currently spawned enemy, if the enemy is inactive, spawn another
    private GameObject currentEnemy = null; 


	void Start ()
	{
        //Invoke spawns enemy after 
		Invoke("SpawnEnemy", spawnTime);
	}

    void Update()
    {
        //if the spawner is testing for death and the enemy is dead
        if(enemySpawned == true && currentEnemy == null)
        {
            //Invoke spawns enemy after 
            Invoke("SpawnEnemy", spawnTime);
        }
    }

	public void SpawnEnemy()
	{
        if(currentEnemy == null)
        {
            //Instantiate spawn the enemy, store as currentEnemy
            currentEnemy = Instantiate(Enemy, transform.position, Quaternion.identity) as GameObject;
            enemySpawned = true;
        }
        
	}

    
}
