using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollider : MonoBehaviour
{
    public ParticleSystem particleLauncer;
    public ParticleSystem splatterParticle;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        particleLauncer = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        splatterParticle = transform.GetChild(1).GetComponentInChildren<ParticleSystem>();

        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncer, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++) 
        {
            EmitAtLocation(collisionEvents[i]);    
        }

        Debug.Log("Particle Collision: " + other.name);
    }

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticle.transform.position = particleCollisionEvent.intersection;
        splatterParticle.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        splatterParticle.Emit(1);
    }
}
