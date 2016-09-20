using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    // health variables
    // two Entities or one
    // Should we have ship and a human player as two seperate entities?
    public float playerHealth;
    public float shipHealth;
    bool alive = true;

    // Use this for initialization
    void Start () {
        playerHealth = 1000f; // Start at 1000 health. This can be changed later depending on how we want to use it.
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
