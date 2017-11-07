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

    private SphereCollider sC;

    private Rigidbody rb;
    private MeshRenderer mR;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        sC = GetComponent<SphereCollider>();
        mR = GetComponent<MeshRenderer>();
        speed = speed + (Random.Range(-2.0f, 2.0f));

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (chasePlayer && player != null)
        {
            ChasePlayer();
        }
	}

    public void ChasePlayer()
    {
        transform.LookAt(player.transform);
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
    	if (hp <= 0)
    	{
            scoreManager.AddScore(150);
    		Destroy(this.gameObject);
    	}
    }

    public void ResetMat()
    {
    	mR.material = normalMat;
    }
}
