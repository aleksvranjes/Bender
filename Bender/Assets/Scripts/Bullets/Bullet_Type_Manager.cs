using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Type_Manager : MonoBehaviour {

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
		}
		else if (Input.GetKeyDown("2"))
		{
			currentBulletType = 1;
			TurnOnOutline();
		}
		else if (Input.GetKeyDown("3"))
		{
			currentBulletType = 2;
			TurnOnOutline();
		}
		else if (Input.GetKeyDown("4"))
		{
			currentBulletType = 3;
			TurnOnOutline();
		}
		else if (Input.GetKeyDown("5"))
		{
			currentBulletType = 4;
			TurnOnOutline();
		}

		// Mouse wheel element traversal
		else if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			currentBulletType = (currentBulletType + 1) % 5;
			TurnOnOutline();
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			currentBulletType--;
			if (currentBulletType < 0)
				currentBulletType = 4;
				
			TurnOnOutline();
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
			}
		}
	}

	public int GetCurrentBulletType()
	{
		return currentBulletType;
	}
}
