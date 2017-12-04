using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Stat_Manager : MonoBehaviour {

	public float gameTime;
	public int score;
	public int kills;
	public int shotsFired, shotsHit;

	public bool trackTime = false;

	[DllImport("__Internal")]
	private static extern void Hello();

	[DllImport("__Internal")]
	private static extern void Stats(float gT, int s, int k, int sF, int sH);

	// Use this for initialization
	void Start () {
		gameTime = 0;
		score = 0;
		kills = 0;
		shotsFired = 0;
		shotsHit = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (trackTime) {
			gameTime += Time.deltaTime;
		}
	}

	public void StartGame() {
		trackTime = true;
	}

	public void ResetGame() {
		gameTime = 0;
		score = 0;
		kills = 0;
		shotsFired = 0;
		shotsHit = 0;
	}

	public void GameOver() {
		trackTime = false;
		Debug.Log ("Gametime: " + gameTime);
		Debug.Log ("Score: " + score);
		Debug.Log ("Kills: " + kills);
		Debug.Log ("Shots Fired: " + shotsFired);
		Debug.Log ("Shots Hit: " + shotsHit);
		Debug.Log ("Accuracy: " + ((double)shotsHit/(double)shotsFired));

		Stats(gameTime, score, kills, shotsFired, shotsHit);
	}
}
