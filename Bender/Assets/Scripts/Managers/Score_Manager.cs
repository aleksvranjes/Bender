using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour {

    public int score = 0;
    public Text scoreText;

	public Stat_Manager statManager;

	// Use this for initialization
	void Start () {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: 0";
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

		statManager.score += points;
    }
}
