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
    GameObject healthBarObj;
    // Actual health bar slider
    Slider healthBar;

    void Start()
    {
        InitializeHealthBar();
        //set starting health
        Health = InitialHealth;
        UpdateHealthBar();
    }

    void LateUpdate()
    {
        if(healthBarObj)
        {
            healthBarObj.transform.rotation = Quaternion.identity;
        }
       
    }
    public void TakeDamage(float damage)
    {
        
        if (!healthBarObj.activeSelf)
        {
            //show health bar
            healthBarObj.SetActive(true);
        }
        Health -= damage;
        Debug.Log("Damaged Enemy, health is: "+Health);
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
        healthBarObj = Instantiate(Resources.Load("HealthBarObj"), transform.position, Quaternion.identity) as GameObject;
        //set the health bar canvas as a child of this enemy
        healthBarObj.transform.parent = transform;
        //Get the slider component of the health bar
        healthBar = healthBarObj.GetComponentInChildren<Slider>();
        //hide health bar
        healthBarObj.SetActive(false);
    }

	
}