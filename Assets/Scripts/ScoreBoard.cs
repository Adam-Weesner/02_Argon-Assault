using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObj;
    [SerializeField] private int continualPoints = 1;
    private PlayerController player;
    private bool isPlayerAlive = true;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextObj.GetComponent<Text>().text = score.ToString();

        player = FindObjectOfType<PlayerController>();
        InvokeRepeating(nameof(AddPointsContinually), 1.0f, 1.0f);
    }

    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
        scoreTextObj.GetComponent<Text>().text = score.ToString();
        
    }

    private void AddPointsContinually()
    {
        if (player.isAlive)
        {
            score += continualPoints;
            scoreTextObj.GetComponent<Text>().text = score.ToString();
        }
    }
}
