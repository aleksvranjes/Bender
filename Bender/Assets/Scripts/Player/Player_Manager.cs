using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour {

    enum BulletType 
    {
        Wood = 0,
        Earth = 1,
        Water = 2,
        Fire = 3,
        Metal = 4
    };

    public int lives;
    public bool invincible;
    public float hitDuration = 3f;
    public float flashLength = 0.5f;

    // Variables for manipulating player when out of bounds
    public float rotationSpeed = 5f;
    public float fallDuration = 2f;
    public bool outOfBounds = false;
    public float maxFallDistance = -20;
    public Game_Manager gameManager;

    public SpriteRenderer sR;
    private Player_Mouse_Rotate pMR;
    private Player_Shoot pS;
    private Player_Move pM;

    private Vector3 startPos;
    public Vector3 startScale;
    private Quaternion startRot;

    public List<RuntimeAnimatorController> animControllers = new List<RuntimeAnimatorController>();
    public List<Sprite> sprites = new List<Sprite>();
    public List<float> spriteScales = new List<float>();
    public List<float> colliderScales = new List<float>();
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public float bulletPower = 5;

    public float minX, maxX, minZ, maxZ;
    // Use this for initialization
    void Start ()
    {
        sR = GetComponent<SpriteRenderer>();
        pMR = GetComponent<Player_Mouse_Rotate>();
        pS = GetComponent<Player_Shoot>();
        pM = GetComponent<Player_Move>();
        DisablePlayer();
        startPos = transform.position;
		startScale = new Vector3(1.0f, 1.0f, 1.0f);
        startRot = transform.rotation;
        SwitchPlayerType(0);
       // GetComponent<SpriteRenderer>().Sp
    }
    
    // Update is called once per frame
    void Update ()
    {
        // if (Input.GetKey("space"))
        //     TakeDamage();

        if (transform.position.x < minX || transform.position.x > maxX
            || transform.position.z < minZ || transform.position.z > maxZ)
        {
            GetComponent<Rigidbody>().useGravity = true;
            OutOfBounds();
            outOfBounds = true;
        }
    }

    //
    public void TakeDamage()
    {
        if (invincible)
        {
            return;
        }

        // If there are no lives left, destroy the player
        if (lives == 0)
        {
            DestroyPlayer();
            return;
        }

        lives--;
        gameManager.LostLife(lives);
        //Debug.Log("Player hit. Lives = " + lives);
        invincible = true;
        StartCoroutine("PlayerHit");
    }

    public IEnumerator PlayerHit()
    {
        float totalTime = 0;
        float currentTime;
        bool currentlyActive = true;
        while (totalTime < hitDuration)
        {
            currentTime = Time.time;
            currentlyActive = !currentlyActive;
            sR.enabled = currentlyActive;
            yield return new WaitForSeconds(flashLength);
            totalTime += flashLength; 
        }

        
        sR.enabled = true;
        invincible = false;
    }

    public void DisablePlayer()
    {
        pMR.rotateWithMouse = false;
        pS.StopShooting();
        pM.allowMovement = false;
    }

    public void EnablePlayer()
    {
        pMR.rotateWithMouse = true;
        pS.allowShooting = true;
        pM.allowMovement = true;
        sR.enabled = true;
    }

    public void OutOfBounds()
    {
//        Debug.Log("Out of bounds");
        pMR.rotateWithMouse = false;
        pS.StopShooting();
        pM.allowMovement = false;
        StartCoroutine("PlayerFall");
    }

    public IEnumerator PlayerFall()
    {
        float currentTime;
        float scaleFactor = 1.0f;
        float time = 0;
        float delta = 0;
        float lastTime = Time.realtimeSinceStartup;

        float currentRotation = 0;
        Vector3 currentScale;
        float startScale = transform.localScale.x;

//        Debug.Log("While loop starting");
        while (scaleFactor > 0.001f)
        {
            currentTime = Time.realtimeSinceStartup;
            delta = currentTime - lastTime;
            lastTime = currentTime;
            time += delta / fallDuration;

            currentScale = transform.localScale;
            scaleFactor = Mathf.Lerp(startScale, 0.0f, time);
            currentScale *= scaleFactor;
            transform.localScale = Vector3.one * scaleFactor;

            currentRotation += rotationSpeed * Time.deltaTime * 100f;
            //Debug.Log(currentRotation.y);
            //currentRotation.y += ;
            transform.rotation = Quaternion.Euler(new Vector3(90, (currentRotation % 360) , 0));

            yield return null;
        }

        DestroyPlayer();
    }

    public void DestroyPlayer()
    {
        gameObject.SetActive(false); 
        //Debug.Log("GAME OVER");
        gameManager.GameOver();
    }

    public void ResetPlayer()
    {
        gameObject.SetActive(true);
        transform.position = startPos;
        transform.localScale = startScale;
        transform.rotation = startRot;
        sR.enabled = true;
        lives = 3;
    }

    public void SwitchPlayerType(int type)
    {
        spriteRenderer.sprite = sprites[type];
        anim.runtimeAnimatorController = animControllers[type];
        transform.localScale = Vector3.one * spriteScales[type];
        GetComponent<BoxCollider>().size = Vector3.one * colliderScales[type];
    }

    public void EnemyBulletHit(Vector3 bulletDirection)
    {
        GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * bulletPower, ForceMode.Impulse);
    }
}


// 1.25
// 2.5
// 1.75
// 1.75
// 1.75