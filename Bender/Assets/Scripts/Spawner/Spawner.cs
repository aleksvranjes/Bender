using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject player;
	public Transform enemySmallParent;

	public bool spawnSmalls = true;
	public bool smallSpawnerStarted = false;
	public float smallMinDelay = 2f;
	public float smallMaxDelay = 7f;

	public GameObject[] smallEnemies;
	public Transform[] smallSpawnLocations;


	public void Update()
	{
		if (spawnSmalls && !smallSpawnerStarted)
		{
			smallSpawnerStarted = true;
			StartCoroutine ("SmallSpawner");
		}
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
				GameObject newEnemy = Instantiate (smallEnemies[randomEnemy], smallSpawnLocations[i].position, Quaternion.identity, enemySmallParent);
				newEnemy.GetComponent<Enemy>().player = player;
			}
			yield return new WaitForSeconds (delay);
		}
	}
}
