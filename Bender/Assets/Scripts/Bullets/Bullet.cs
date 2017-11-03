using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 10.0f;
	public int bulletType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * bulletSpeed * Time.deltaTime * 10.0f;
	}

	public virtual void BulletContact()
	{
		Destroy(this.gameObject);
	}

	public void OnTriggerEnter(Collider other) 
	{
		if (other.transform.tag == "Enemy")
		{
			other.GetComponentInParent<Enemy>().BulletHit(bulletType);
			BulletContact();
		}
	}


}
