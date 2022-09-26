using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;

    List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();    
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
            Debug.Log("Particles collision events: " + other.name);

        }

        Debug.Log("Particles collision: " + other.name);
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection; //position in world space 
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal); //rotate 
        splatterParticles.Emit(1);
    }

}
