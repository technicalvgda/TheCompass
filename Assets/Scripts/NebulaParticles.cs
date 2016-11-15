using UnityEngine;
using System.Collections;

public class NebulaParticles : MonoBehaviour
{
    ParticleSystem particles;
    int emissionMultiplier = 2;

    //gizmo code
    Vector3 lowerRight;
    Vector3 upperRight;
    Vector3 lowerLeft;
    Vector3 upperLeft;

    // Use this for initialization
    void Start()
    {
        
        //set emission rate to reflect size of construct
        float emissionRate = emissionMultiplier * transform.lossyScale.magnitude / 4;
        particles = GetComponent<ParticleSystem>();
        /*
        ParticleSystem.EmissionModule em = particles.emission;//.rate.Equals();
        ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
        rate.constantMax = emissionRate;
        em.rate = rate;
        */

        //set particle size to reflect size of construct
        particles.startSize = particles.startSize + (transform.lossyScale.magnitude/4);
        particles.startLifetime = 2;//00000;//Mathf.Clamp(transform.lossyScale.magnitude/20,1,10);
        particles.maxParticles = (int)(transform.lossyScale.magnitude); // should equal 25 for scale 100
    }

    void OnDrawGizmos()
    {

        float xOffset = GetComponent<Collider2D>().bounds.size.x/2;
        float yOffset = GetComponent<Collider2D>().bounds.size.y /2;
        lowerLeft = new Vector3(transform.position.x - xOffset,transform.position.y-yOffset,0);
        upperLeft = new Vector3(transform.position.x - xOffset, transform.position.y + yOffset, 0);
        lowerRight = new Vector3(transform.position.x + xOffset, transform.position.y - yOffset, 0);
        upperRight = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(lowerLeft, lowerRight);
        Gizmos.DrawLine(lowerLeft, upperLeft);
        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);

    }

}
