using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour {

	public float bulletSpeed = 7.0f;
	public Transform player;

	// Use this for initialization
	void Start () {
		//transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
		Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		Vector3 dir = player.GetComponent<Player_Move>().speed.normalized;
		float dist = (player.position - transform.position).magnitude;
		float time = dist / bulletSpeed;
		Vector3 estPos = (player.position) + (dir * time);
		transform.LookAt(estPos);
		transform.rotation = Quaternion.Euler(new Vector3(90, (transform.rotation.eulerAngles.y + 180) % 360, 0));
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
		if (other.transform.tag == "Player")
		{
			other.GetComponentInParent<Player_Manager>().EnemyBulletHit(-transform.up);
			BulletContact();
		}
	}


}
