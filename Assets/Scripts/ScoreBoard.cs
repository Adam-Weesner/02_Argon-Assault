using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObj;
    [SerializeField] private int scorePerHit = 12;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextObj.GetComponent<Text>().text = score.ToString();
    }

    public void ScoreHit()
    {
        score += scorePerHit;
        scoreTextObj.GetComponent<Text>().text = score.ToString();
    }
}
