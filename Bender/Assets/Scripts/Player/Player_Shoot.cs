using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour {

	[SerializeField]
	private Player_Mouse_Rotate pMR;

	public GameObject[] bulletPrefabs;

	public float bulletLifespan = 3.0f;
	public float shootRate = 0.3f;
	public bool startedShooting = false;
	public bool currentlyShooting = false;

	public bool allowShooting = true;

	public Bullet_Type_Manager bTM;

	// Use this for initialization
	void Start () {
		pMR = GetComponent<Player_Mouse_Rotate>();
	}
	
	// Update is called once per frame
	void Update () {
		if (allowShooting)
		{
			if (Input.GetMouseButton(0))
			{
				currentlyShooting = true;
			}
			else
			{
				currentlyShooting = false;
			}

			if (currentlyShooting && !startedShooting)
			{
				startedShooting = true;
				Debug.Log("Started shooting");
				StartCoroutine("Shoot");
			}
			if (!currentlyShooting && startedShooting)
			{
				startedShooting = false;
				Debug.Log("Stopped shooting");
				StopCoroutine("Shoot");
			}
		}
	}

	IEnumerator Shoot()
	{
		while (true)
		{
			Quaternion angle = Quaternion.Euler(new Vector3(0.0f, -pMR.angle, 0.0f));
			Vector3 pos = transform.position;

			GameObject bullet = Instantiate(bulletPrefabs[bTM.GetCurrentBulletType()], pos, angle);
			Destroy(bullet, bulletLifespan);

			yield return new WaitForSeconds(shootRate);
		}
	}

	public void StopShooting()
	{
		startedShooting = false;
		allowShooting = false;
		Debug.Log("Stopped shooting");
		StopCoroutine("Shoot");
	}
}
