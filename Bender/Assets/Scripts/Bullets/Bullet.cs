using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * bulletSpeed * Time.deltaTime * 10.0f;
	}

	public virtual void BulletContact()
	{
		//This function is purely virtual
	}


}
