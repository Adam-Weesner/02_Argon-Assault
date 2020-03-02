using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle_explosion;
    [SerializeField] private float destructTime = 2.5f;
    private bool isDying = false;

    private void OnTriggerEnter(Collider other)
    {
        SendMessage(nameof(StartDeathSequence));
    }

    private void OnParticleCollision(GameObject other)
    {
        SendMessage(nameof(StartDeathSequence));
    }

    private void StartDeathSequence()
    {
        if (!isDying)
        {
            // Change C
            isDying = true;
            Instantiate(particle_explosion, gameObject.transform);
            GetComponent<MeshRenderer>().enabled = false;
            Invoke(nameof(SelfDestruct), destructTime);
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
