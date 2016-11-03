using UnityEngine;
using System.Collections;

public class NebulaParticles : MonoBehaviour
{
    ParticleSystem particles;
    int emissionMultiplier = 10;
    // Use this for initialization
    void Start()
    {
        //set emission rate to reflect size of construct
        float emissionRate = emissionMultiplier * transform.lossyScale.magnitude;
        particles = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = particles.emission;//.rate.Equals();
        ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
        rate.constantMax = emissionRate;
        em.rate = rate;

        //set particle size to reflect size of construct
        particles.startSize = particles.startSize + (emissionRate/20);
    } 
	
}
