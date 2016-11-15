using UnityEngine;
using System.Collections;

public class GasTrailHandler : MonoBehaviour {

    public Transform GasGiant;
    public Transform BlackHole;

    public GameObject GasTrailObj;

    //linerender attached to this object
    LineRenderer lineRndr;
    //particles attached to child
    ParticleSystem part;

    Vector2 Heading;
    float distance;
    //amount to move particle emission from center of gas giant
    float offset;
    //amount to adjust emitter to match border of gas giant
    float adjustment = 15;

	// Use this for initialization
	void Start ()
    {
        lineRndr = GetComponent<LineRenderer>();

        part = GetComponentInChildren<ParticleSystem>();
       
        //gets radius of gas giant, assumes width and height are the same
        offset = GasGiant.GetComponent<SpriteRenderer>().bounds.size.x / 2 - adjustment;
        StartCoroutine(SpawnTrail());
    }
	
	// Update is called once per frame
	void Update ()
    {
        //get the heading from gas giant to black hole
        Heading = BlackHole.position - GasGiant.position;
        distance = Vector2.Distance(BlackHole.position, GasGiant.position);
       

        lineRndr.SetPosition(0, transform.position);
        lineRndr.SetPosition(1, BlackHole.position);
        SetRotationOfTrail();

        HandleParticleBehavior();

       // Debug.Log(part.startLifetime);
    }

    //Gets proper direction of trail
    void SetRotationOfTrail()
    {
       
       
        //face gas trail towards vector
        transform.rotation = Quaternion.LookRotation(-Heading);
        //offset position
        // = GasGiantPosition + (unitvector in proper direction * offsetMagnitude)
        transform.position = GasGiant.position + ((Vector3)Heading.normalized * offset);
    }

    void HandleParticleBehavior()
    {
        // Vector3 targetLocal = transform.InverseTransformPoint(BlackHole.transform.position - transform.position);


        // initialize an array the size of our current particle count
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[part.particleCount];

        // *pass* this array to GetParticles...
        int num = part.GetParticles(particles);
        //Debug.Log("Found " + num + " active particles.");
        for (int i = 0; i < num; i++)
        {
            // Debug.Log("Part/Gas dist: " + Vector2.Distance(particles[i].position, GasGiant.transform.position));
            // Debug.Log("Hole/ gas dist: "+ distance);
            //Vector3 distToTarget = particles[i].position - targetLocal;

            // if vector from particle source to target points in the same direction as
            // vector from target to particle, it means the particle has passed the target 
            
            if (Vector3.Dot((particles[i].position - transform.InverseTransformPoint(BlackHole.transform.position)).normalized, (Heading).normalized) > 0)
            {
                particles[i].lifetime = 0;
            }
           
           
            /*
            //if the particles are further than the distance between the gas giant and black hole
            if (Vector2.Distance(particles[i].position, transform.position) > distance)
            {
                particles[i].lifetime = 0;
            }
            */
            
        }
        // re-assign modified array
        part.SetParticles(particles, num);
    }

    public IEnumerator SpawnTrail()
    {
        yield return new WaitForSeconds(2f);

        GameObject trail =  Instantiate(GasTrailObj, GasGiant.transform.position, GasGiant.transform.rotation) as GameObject;
        //trail.transform.parent = this.transform;
        //trail.GetComponent<GasTrailMovement>().SetBlackHole(BlackHole.gameObject);
        StartCoroutine(SpawnTrail());

        yield return null;
    }
    
}
