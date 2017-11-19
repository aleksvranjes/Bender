using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 10.0f;
	public int bulletType;

	// Use this for initialization
	void Start () {
		//transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += -transform.up * bulletSpeed * Time.deltaTime * 10.0f;
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
