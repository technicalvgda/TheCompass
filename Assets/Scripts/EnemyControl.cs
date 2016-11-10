using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	public float enemyHealth;
	public float enemyMaxHP;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(float damage){
		enemyHealth -= damage;
	}

	public void healHP(float health){
		enemyHealth += health;
	}
}
