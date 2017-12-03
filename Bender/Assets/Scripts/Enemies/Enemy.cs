using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public enum EnemyType 
	{
		Wood = 0, 
		Earth = 1,
		Water = 2,
		Fire = 3,
		Metal = 4
	};

    public bool isBig = false;
	public EnemyType enemyType;
    public GameObject player;
    public float speed = 5f;
    public float hp = 100f;
    public float damage = 100f;
    public bool chasePlayer = true;
    public float damageRange = 1f;

    public float hitDuration = 0.1f;
    public Material normalMat, hitMat;

    public Score_Manager scoreManager;
	public Stat_Manager statManager;
    public float randomWait = 3f;
    public bool shootPlayerStarted = false;

    private SphereCollider sC;

    private Rigidbody rb;
    private MeshRenderer mR;

    public GameObject bulletPrefab;
    public float bulletLifespan = 3f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        sC = GetComponent<SphereCollider>();
        mR = GetComponent<MeshRenderer>();
        speed = speed + (Random.Range(-2.0f, 2.0f));
        randomWait = 3f;

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (chasePlayer && player != null)
        {
            ChasePlayer();
        }
        if (isBig && player != null && !shootPlayerStarted)
        {
            shootPlayerStarted = true;
            StartCoroutine("ShootPlayer");
        }
        if (isBig)
            randomWait -= Time.deltaTime;
	}

    public void ChasePlayer()
    {
        Vector3 directionVector = player.transform.position - transform.position;
        transform.position += directionVector.normalized * speed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
        	if (!collision.transform.GetComponent<Player_Manager>().invincible)
        	{
            	collision.transform.GetComponent<Player_Manager>().TakeDamage();
                if (!isBig)
                	Destroy(this.gameObject);
        	}
        	else
        	{
        		sC.isTrigger = true;
        		rb.useGravity = false;
        		Invoke("TurnTriggerOff", 1.0f);
        	}
        }
    }

    public void TurnTriggerOff()
    {
    	sC.isTrigger = false;
    	rb.useGravity = true;
    }

    public virtual void BulletHit(int type)
    {
        if (mR != null)
    	   mR.material = hitMat;
    	Invoke("ResetMat", 	hitDuration);

    	int upperRange, lowerRange;
    	if (type == (int)enemyType)
    	{
    		upperRange = 85;
    		lowerRange = 65;
    	}
    	else
    	{
    		upperRange = 25;
    		lowerRange = 15;
    	}
    	hp -= Random.Range(lowerRange, 	upperRange);
		statManager.shotsHit++;
    	if (hp <= 0)
    	{
            if (!isBig)
                scoreManager.AddScore(150);
			else
                scoreManager.AddScore(350);
                
            statManager.kills++;
    		Destroy(this.gameObject);
    	}
    }

    public void ResetMat()
    {
        if (mR != null)
    	   mR.material = normalMat;
    }

    public IEnumerator ShootPlayer()
    {
        while (true)
        {
            Vector3 from = -transform.up;
            Vector3 to = (player.transform.position - transform.position).normalized;

            float rot = Mathf.Acos(((from.x * to.x) + (from.y * to.y) + (from.z * to.z)) / (from.magnitude * to.magnitude));

            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.Euler(new Vector3(90, rot, 0)));
            //bullet.GetComponent<Bullet>().bulletType = bulletType;
            Destroy(bullet, bulletLifespan);
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
