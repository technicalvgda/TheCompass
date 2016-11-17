using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour 
{
    //Items for the enemy to drop
	public GameObject Items;
    //Starting health of enemy
    public float InitialHealth = 20f;
	private float Health;

    //Canvas to contain health bar
    Canvas healthBarCanvas;
    // Actual health bar slider
    Slider healthBar;

    void Start()
    {
        InitializeHealthBar();
        //set starting health
        Health = InitialHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        UpdateHealthBar();
        //check if the enemy is out of health
        CheckHealth();
    }

    void CheckHealth()
    {
        if (Health <= 0)
        {
            // kill enemy
            Destroy(gameObject);
            // increase kill counter
            Player.increaseKillCount();
            Instantiate(Items, transform.position, Quaternion.identity);
        }
    }

    //returns a value between 0 and 1 based on health remaining (1 is full, 0 is empty)
    void UpdateHealthBar()
    {
        healthBar.value = Health / InitialHealth;
    }

    void InitializeHealthBar()
    {
        //Instantiate the health bar canvas at the location of the enemy
        healthBarCanvas = Instantiate(Resources.Load("HealthBarCanvas"), transform.position, transform.rotation) as Canvas;
        //set the health bar canvas as a child of this enemy
        healthBarCanvas.transform.parent = transform;
        //Get the slider component of the health bar
        healthBar = healthBarCanvas.transform.FindChild("HealthBar").GetComponent<Slider>();
    }

	
}