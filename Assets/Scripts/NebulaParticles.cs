using UnityEngine;
using System.Collections;

public class NebulaParticles : MonoBehaviour
{
    ParticleSystem particles;
    int emissionMultiplier = 10;
    // Use this for initialization
    void Start()
    {
        float emissionRate = emissionMultiplier * transform.lossyScale.magnitude;
        particles = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = particles.emission;//.rate.Equals();
        ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
        rate.constantMax = emissionRate;
        em.rate = rate;
    } 
	
}
