using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int scorePerHit = 10;
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
            isAlive = false;
            print("Enemy hit!");
            scoreBoard.ScoreHit(scorePerHit);
        }
    }
}
