using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    public bool readyToStart = true;
    public bool gameOver = false;

    public Spawner spawner;
    public Player_Manager playerManager;
    public Score_Manager scoreManager;

    public Text screenOverlayText;
    public Text livesText;
    public Transform smallEnemies;

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 0;
        
        TurnOnText("Press 'Space' To Start!");
        livesText.gameObject.SetActive(true);
        livesText.text = "Lives: 3";
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (readyToStart && Input.GetKeyDown("space"))
        {
            Time.timeScale = 1;
            StartGame();
        }

        if (gameOver && Input.GetKeyDown("r"))
        {
            ResetGame();
        }
	}

    public void StartGame()
    {
        spawner.StartAllSpawners();
        playerManager.EnablePlayer();
        screenOverlayText.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        TurnOnText("Press 'Space' To Start!");
        readyToStart = true;
        scoreManager.ResetScore();
        playerManager.ResetPlayer();
        livesText.text = "Lives: 3";
        ResetEnemies();

    }

    public void GameOver()
    {
        Time.timeScale = 0;
        spawner.StopAllSpawners();
        playerManager.DisablePlayer();
        TurnOnText("Game Over!!\nPress 'R' To Restart Game");
        gameOver = true;

    }

    public void LostLife(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void TurnOnText(string msg)
    {
        screenOverlayText.gameObject.SetActive(true);
        screenOverlayText.text = msg;
    }

    public void ResetEnemies()
    {
        int totalChildren = smallEnemies.childCount;
        for (int i = 0; i < totalChildren; i++)
        {
            Destroy(smallEnemies.GetChild(i).gameObject);
        }
    }
}
