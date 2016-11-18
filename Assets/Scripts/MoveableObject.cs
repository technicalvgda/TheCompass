using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour
{

    private Rigidbody2D rb2d;

    //Flame Trail
    GameObject flameTrail;

    //PUBLIC VARIABLES
    public bool drift, rotateActivated, splitactivated;
	public float objectSize = 2; // small 1, medium 2, large 3 
    public GameObject splitter, splitterShard, fuelDrop;
    public float splitShards = 4;
    public float driftSpeed = 100;
    public bool isTractored = false;

    //Object movement variables
    private Vector2 curVelocity;
    private Vector2 curDirection;//< normalized velocity vector
    private float curSpeed;

    //Constant values
    const float MINIMUM_DAMAGE_SPEED = 20f; //Minimum speed at which an object can deal damage

    //amount to multiplay knockback by (currently unused)
    private float knockBackImpact = 1.2f;  // adjust the impact force

	// Use this for initialization
	void Start ()
	{
		//get rigidbody component 
		rb2d = GetComponent<Rigidbody2D>();
        //initialize settings for object
        InitializeObj();
        //initialize flame trail
        /*
        if (gameObject.tag == "Debris" || gameObject.tag == "TetheredPart")
        {InitializeFlameTrail();}
        */
       
    }

	void FixedUpdate()
	{
        if (rotateActivated)
        {RotateLeft();}

        curVelocity = rb2d.velocity;
        curDirection = curVelocity.normalized;
		curSpeed = rb2d.velocity.magnitude;
        //turn flame trail on and off based on asteroid speed
        ToggleFlameTrail(); 
	}

    void RotateLeft()
    {
        transform.Rotate(Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D col)
	{
        
        if (splitactivated)
        {Split();}
        //if this is a debris object and its colliding with player
		if (gameObject.tag == "Debris")
        {
            //handle player damage and knockback
            if(col.gameObject.tag == "Player")
            {
                Player playerObject = col.gameObject.GetComponent<Player>();
                //calculate damage to deal, knockback is true
                playerObject.takeDamage(CalculateAngularDamageAndKnockback(col, true));
            }
            //handled enemy damage and knockback
            if (col.gameObject.tag == "Enemy")
            {
                EnemyCollision enemyObject;
                if (enemyObject = col.gameObject.GetComponent<EnemyCollision>())
                {
                    //calculate damage to deal, knockback is true
                    enemyObject.TakeDamage(CalculateAngularDamageAndKnockback(col, false));
                }
                
               
            }
            

        }
	}

    //initialize the object
    void InitializeObj()
    {
        ///set object size
        if (transform.localScale.x < 0.65)
        { objectSize = 1; }
        else if (transform.localScale.x >= 0.65 && transform.localScale.x <= 1.35)
        { objectSize = 2; }
        else
        { objectSize = 3; }

        // TODO give the object a random sprite

        //give the object a random rotation
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        transform.eulerAngles = euler;

        ///set drift
		if (drift)
        { Drift(); }
    }

    void Split()
    {
         float splitterX, splitterY;
        splitterX = splitter.transform.position.x;
        splitterY = splitter.transform.position.y;

        isTractored = false;
        Destroy(splitter);
        for (int i = 0; i < splitShards; i++)
        {
            Instantiate(splitterShard, new Vector3(Random.Range(splitterX - objectSize * 2, splitterX + objectSize * 2), Random.Range(splitterY - objectSize * 2, splitterY + objectSize * 2), 0), Quaternion.identity);
        }
        if (fuelDrop) //If fuel object was assigned, drop a fuel prefab on collision as well
        {
            Instantiate(fuelDrop, new Vector3(Random.Range(splitterX - objectSize * 2, splitterX + objectSize * 2), Random.Range(splitterY - objectSize * 2, splitterY + objectSize * 2), 0), Quaternion.identity);
        }
    }

    void Drift()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y).normalized;
        rb2d.AddForce(direction * driftSpeed);
    }
    /*
    void InitializeFlameTrail()
    {
        //instantiate flame trail
        flameTrail = Instantiate(Resources.Load("FlameTrail"), transform.position, transform.rotation) as GameObject;
        //attach as child to this game object
        flameTrail.transform.parent = transform;
        //set flame trail inactive
        flameTrail.SetActive(false);
    }
    */

    void ToggleFlameTrail()
    {
        //if the asteroid is moving faster than the minimum damage velocity and the flame trail is not active and the object is not held

        if (flameTrail == null && curSpeed >= MINIMUM_DAMAGE_SPEED  && !isTractored)
        {
            //instantiate flame trail
            flameTrail = Instantiate(Resources.Load("FlameTrail"), transform.position, transform.rotation) as GameObject;
            flameTrail.transform.parent = transform;
           

        }
        if (curSpeed < MINIMUM_DAMAGE_SPEED && flameTrail != null)
        {
           
            flameTrail.transform.parent = null;
            flameTrail = null;
         
        }
    }

    public void DisableFlameTrail()
    {
        flameTrail.transform.parent = null;
        flameTrail = null;
    }

    //calculates proper damage and knockback based on asteroid speed and angle of collision
    float CalculateAngularDamageAndKnockback(Collision2D col, bool knockback)
    {
        Rigidbody2D targetRB = col.gameObject.GetComponent<Rigidbody2D>();
        float _asteroidDamageForce = 0f;
        //if all player components are not null,the object is not tractored, and the object is moving fast enough to deal damage
        if (targetRB && !isTractored && curSpeed >= MINIMUM_DAMAGE_SPEED)
        {
            //non normalized velocity of player
            Vector2 targetVelocity = targetRB.velocity;

            //take dot product of asteroid velocity vector and player velocity vector (normalized)
            float _directionStatus = Vector2.Dot(curDirection, targetVelocity.normalized);

            //direction status is -1 if object and player are colliding head on, 0 is player is not moving, 1 if same heading
            _asteroidDamageForce =  ((curSpeed-MINIMUM_DAMAGE_SPEED) - (_directionStatus*targetVelocity.magnitude));
            
            //prevent the damage from being negative
            if(_asteroidDamageForce < 0)
            { _asteroidDamageForce = 0;}

            //knockback object if knockback is true
            if(knockback)
            {
                //calculate and execute knockback
                CalculateKnockback(col, targetRB, _directionStatus, targetVelocity);
            }
        }
        //return damage
        return _asteroidDamageForce;
    }

    void CalculateKnockback(Collision2D col,Rigidbody2D targetRB, float _directionStatus, Vector2 targetVelocity)
    {
        //get proper direction to knock back target object
        Vector2 knockbackDirection = (curDirection + targetVelocity.normalized).normalized;
        //knock back target scaled by the velocity of both objects
        targetRB.AddForce(knockbackDirection * (curSpeed + (_directionStatus * targetVelocity.magnitude)));
        
    }


}