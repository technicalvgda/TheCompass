using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    // Health Variables
    private float playerHealth;
    private float shipHealth;
    bool alive= true;

	// Use this for initialization
	void Start () {
        playerHealth = 1000f; // Set health to 1000 for now. This can be changed later
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void gainHealth(float health) {
        playerHealth += health;
    }

    void takeDamage(float damage) {
        playerHealth -= damage;    
    }
}
