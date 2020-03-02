using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private int scorePerHit = 10;
    [SerializeField] private ParticleSystem particle_explosion;
    private ScoreBoard scoreBoard;
    private bool isAlive = true;

    private void Awake()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void StartDeathSequence()
    {
        if (isAlive)
        {
            health--;

            if (health <= 0)
            {
                isAlive = false;
                Instantiate(particle_explosion, gameObject.transform);
                scoreBoard.ScoreHit(scorePerHit);
            }
            else
            {
                // TODO consider hit FX
            }
        }
    }
}
