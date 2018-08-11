using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    int score = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        score++;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (score > 0)
            score--;
    }

    public void ShowScore()
    {
        Debug.Log("Score is: " + score);
        score = 0;
    }
}
