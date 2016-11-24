using UnityEngine;
using System.Collections;

public class LaserBullet : MonoBehaviour
{
    private GameObject player;
    private Player playerscript;
    public float bounceIntensity;  //how much the player gets bounced back upon collision with the laser
    private float calcDamage;
    public float timeLimit = 0.5f;  //amount of time the player is paralyzed for

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<Player>();
        bounceIntensity = 5.0f;

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 laserScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (laserScreenPos.y >= Screen.height || laserScreenPos.y <= 0 || laserScreenPos.x >= Screen.width || laserScreenPos.x <= 0)
        {
            Destroy(gameObject);
        }
        if (timeLimit > 0 && !playerscript.enabled)
        {
            timeLimit -= Time.deltaTime;
        }
        else
        {
            playerscript.enabled = true;
            timeLimit = 0.5f;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            {
                playerscript.takeDamage(calcDamage);

                //applies force to the player in the opposite direction with which it is hit by the laser
                Vector2 bounceBack = playerscript.transform.position - transform.position;
                bounceBack = new Vector2((bounceBack.x > 0) ? 1 : -1, (bounceBack.y > 0) ? 1 : -1);
                playerscript.GetComponent<Rigidbody2D>().AddForce(bounceBack * bounceIntensity, ForceMode2D.Impulse);

                //deactivates the playerscript in order to simulate paralysis
                playerscript.enabled = false;

            }

        }
    }
}
