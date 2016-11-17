using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour 
{
    //Items for the enemy to drop
	public GameObject Items;
    //Starting health of enemy
    public float InitialHealth = 20f;
	private float Health;

    //TODO add instantiation and handler for health bars
    float barDisplay = 0;
    Vector2 size  = new Vector2(60,20);
    Texture2D progressBarEmpty;
    Texture2D progressBarFull;

    void OnGUI()
    {
        progressBarEmpty = Resources.Load("Texture2") as Texture2D;
        progressBarFull = Resources.Load("Texture2B") as Texture2D;
        // draw the background:
        GUI.BeginGroup(new Rect(transform.position.x, transform.position.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarEmpty);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarFull);
        GUI.EndGroup();

        GUI.EndGroup();
    }

    void Start()
    {
        //set starting health
        Health = InitialHealth;
        barDisplay = Health;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        barDisplay = Health;
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

	
}