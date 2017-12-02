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

	public Stat_Manager statManager;

	//public Texture2D currentTexture;

	// Use this for initialization
	void Start () {
		pMR = GetComponent<Player_Mouse_Rotate>();
		//Cursor.SetCursor(currentTexture, Vector2.zero, CursorMode.ForceSoftware);
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
				StartCoroutine("Shoot");
			}
			if (!currentlyShooting && startedShooting)
			{
				startedShooting = false;
				StopCoroutine("Shoot");
			}
		}
	}

	IEnumerator Shoot()
	{
		while (true)
		{
			Debug.Log(pMR.angle);
			Quaternion angle = Quaternion.Euler(new Vector3(90, -pMR.angle - 90, 0.0f));
			Vector3 pos = transform.position;

			int bulletType = bTM.GetCurrentBulletType();
			GameObject bullet = Instantiate(bulletPrefabs[bulletType], pos, angle);
			statManager.shotsFired++;
			//bullet.GetComponent<Bullet>().bulletType = bulletType;
			Destroy(bullet, bulletLifespan);

			yield return new WaitForSeconds(shootRate);
		}
	}

	public void StopShooting()
	{
		startedShooting = false;
		allowShooting = false;
		StopCoroutine("Shoot");
	}

//	public void ChangeCursor(Texture2D tex) {
//		Cursor.SetCursor(tex, Vector2.zero, CursorMode.ForceSoftware);
//	}

//	public void OnMouseEnter() {
//		Cursor.SetCursor(currentTexture, Vector2.zero, CursorMode.ForceSoftware);
//	}

//	public void OnMouseExit() {
//		Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
//	}
}
