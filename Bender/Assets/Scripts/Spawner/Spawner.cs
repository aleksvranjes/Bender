using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject player;
	public Transform enemySmallParent;
	public Transform enemyBigParent;

	public bool spawnSmalls = true;
	public bool smallSpawnerStarted = false;
	public float smallMinDelay = 2f;
	public float smallMaxDelay = 7f;
	public bool spawnBigs = true;
	public bool bigSpawnerStarted = false;
	public float bigMinDelay = 5f;
	public float bigMaxDelay = 15f;

	public GameObject[] smallEnemies;
	public Transform[] smallSpawnLocations;
	public GameObject[] bigEnemies;
	public Transform[] bigSpawnLocations;

	public GameObject[] enemyBullets;

    public Score_Manager scoreManager;
	public Stat_Manager statManager;

    public void Start()
    {
        spawnSmalls = false;
        spawnBigs = false;
    }

    public void Update()
	{
		if (spawnSmalls && !smallSpawnerStarted)
		{
			smallSpawnerStarted = true;
			StartCoroutine ("SmallSpawner");
		}
		if (spawnBigs && !bigSpawnerStarted)
		{
			bigSpawnerStarted = true;
			StartCoroutine ("BigSpawner");
		}
	}

    public void StartAllSpawners()
    {
        spawnSmalls = true;
        spawnBigs = true;
    }

    public void StopAllSpawners()
    {
        spawnSmalls = false;
        StopCoroutine("SmallSpawner");
        smallSpawnerStarted = false;

        spawnBigs = false;
        StopCoroutine("BigSpawner");
        bigSpawnerStarted = false;
    }

	private IEnumerator SmallSpawner()
	{
		while(true)
		{
			float delay = Random.Range (smallMinDelay, smallMaxDelay);
			int numSmalls = Random.Range (2, 5);
			for (int i = 0; i < numSmalls; i++)
			{
				int randomEnemy = Random.Range (0, 5);
				//randomEnemy = 1;
				GameObject newEnemy = Instantiate (smallEnemies[randomEnemy], smallSpawnLocations[i].position, Quaternion.Euler(90, 0, 0), enemySmallParent);
				newEnemy.GetComponent<Enemy>().player = player;
                newEnemy.GetComponent<Enemy>().scoreManager = scoreManager;
				newEnemy.GetComponent<Enemy>().statManager = statManager;
            }
			yield return new WaitForSeconds (delay);
		}
	}

	private IEnumerator BigSpawner()
	{
		yield return new WaitForSeconds(Random.Range(bigMinDelay, bigMaxDelay));
		while(true)
		{
			float delay = Random.Range (bigMinDelay, bigMaxDelay);
			int numBigs = Random.Range (1, 4);
			//numBigs = 1;
			for (int i = 0; i < numBigs; i++)
			{
				int randomEnemy = Random.Range (0, 4);
				//randomEnemy = 0;
				GameObject newEnemy = Instantiate (bigEnemies[randomEnemy], bigSpawnLocations[i].position, Quaternion.Euler(90, 0, 0), enemyBigParent);
				newEnemy.GetComponent<Enemy>().player = player;
                newEnemy.GetComponent<Enemy>().scoreManager = scoreManager;
				newEnemy.GetComponent<Enemy>().statManager = statManager;
				newEnemy.GetComponent<Enemy>().bulletPrefab = enemyBullets[randomEnemy];
            }
			yield return new WaitForSeconds (delay);
		}
	}
}
