using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mouse_Rotate : MonoBehaviour {

	public Vector3 playerScreenPos;
	public Vector3 mouseScreenPos;

	public Vector2 mouseVector;
	public Vector3 standardVector = new Vector3(1.0f, 0.0f, 0.0f);

	public bool rotateWithMouse = true;

	public float angle;
	
	// Update is called once per frame
	void Update () {

		if (rotateWithMouse)
		{
			playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
			mouseScreenPos = Input.mousePosition;

			mouseVector = new Vector2(mouseScreenPos.x - playerScreenPos.x, mouseScreenPos.y - playerScreenPos.y);

			angle = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler(new Vector3(0.0f, -angle, 0.0f));
		}
	}
}
