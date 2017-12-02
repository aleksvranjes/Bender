using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Type_Manager : MonoBehaviour {

	//public List<Texture2D> texList = new List<Texture2D>();
	//public Player_Shoot pS;

	enum BulletType 
	{
		Wood = 0,
		Earth = 1,
		Water = 2,
		Fire = 3,
		Metal = 4
	};

	public int currentBulletType;
	public GameObject[] outlines;
	public Player_Manager playerManager;

	// Use this for initialization
	void Start () {
		currentBulletType = 0;
		TurnOnOutline();
	}
	
	// Update is called once per frame
	void Update () {

		// Press f to cycle through bullet elements
		if (Input.GetKeyDown("f"))
		{
			currentBulletType = (currentBulletType + 1) % 5;
			TurnOnOutline();
		}

		// Press a number for the corresponding element for quick switch
		else if (Input.GetKeyDown("1"))
		{
			currentBulletType = 0;
			TurnOnOutline();
			//pS.ChangeCursor(texList[0]);
		}
		else if (Input.GetKeyDown("2"))
		{
			currentBulletType = 1;
			TurnOnOutline();
			//pS.ChangeCursor(texList[1]);
		}
		else if (Input.GetKeyDown("3"))
		{
			currentBulletType = 2;
			TurnOnOutline();
			//pS.ChangeCursor(texList[1]);
		}
		else if (Input.GetKeyDown("4"))
		{
			currentBulletType = 3;
			TurnOnOutline();
			//pS.ChangeCursor(texList[3]);
		}
		else if (Input.GetKeyDown("5"))
		{
			currentBulletType = 4;
			TurnOnOutline();
			//pS.ChangeCursor(texList[4]);
		}

		// Mouse wheel element traversal
		else if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			currentBulletType = (currentBulletType + 1) % 5;
			TurnOnOutline();
			//pS.ChangeCursor(texList[currentBulletType]);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			currentBulletType--;
			if (currentBulletType < 0)
				currentBulletType = 4;
				
			TurnOnOutline();
			//pS.ChangeCursor(texList[currentBulletType]);
		}
	}

	public void TurnOnOutline()
	{
		for (int i = 0; i < outlines.Length; i++)
		{
			if (i != currentBulletType)
			{
				outlines[i].SetActive(false);
			}
			else
			{
				outlines[i].SetActive(true);
				playerManager.SwitchPlayerType(i);
			}
		}
	}

	public int GetCurrentBulletType()
	{
		return currentBulletType;
	}

	public void Reset() {
		currentBulletType = 0;
		TurnOnOutline();
		//pS.ChangeCursor(texList[0]);
	}
}
