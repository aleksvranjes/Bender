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

    public MeshRenderer mR;
    private Player_Mouse_Rotate pMR;
    private Player_Shoot pS;
    private Player_Move pM;


    // Use this for initialization
    void Start ()
    {
        mR = GetComponent<MeshRenderer>();
        pMR = GetComponent<Player_Mouse_Rotate>();
        pS = GetComponent<Player_Shoot>();
        pM = GetComponent<Player_Move>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey("space"))
            TakeDamage();

        if (transform.position.y < 0.9f && outOfBounds == false)
        {
            OutOfBounds();
            outOfBounds = true;
        }
    }

    //
    void TakeDamage()
    {
        if (invincible)
        {
            return;
        }

        // If there are no lives left, destroy the player
        if (lives == 0)
            DestroyPlayer();

        lives--;
        Debug.Log("Player hit. Lives = " + lives);
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
            mR.enabled = currentlyActive;
            yield return new WaitForSeconds(flashLength);
            totalTime += flashLength; 
        }

        mR.enabled = true;
        invincible = false;
    }

    public void OutOfBounds()
    {
        Debug.Log("Out of bounds");
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

        Vector3 currentRotation;
        Vector3 currentScale;

        Debug.Log("While loop starting");
        while (transform.position.y > maxFallDistance)
        {
            currentTime = Time.realtimeSinceStartup;
            delta = currentTime - lastTime;
            lastTime = currentTime;
            time += delta / fallDuration;

            currentScale = transform.localScale;
            scaleFactor = Mathf.Lerp(1.0f, 0.0f, time);
            currentScale *= scaleFactor;
            transform.localScale = currentScale;

            currentRotation = transform.rotation.eulerAngles;
            //Debug.Log(currentRotation.y);
            currentRotation.y += (rotationSpeed % 360) * Time.deltaTime * 100f;
            transform.rotation = Quaternion.Euler(currentRotation);

            yield return null;
        }

        DestroyPlayer();
    }

    public void DestroyPlayer()
    {
        Destroy(this.gameObject);
        Debug.Log("GAME OVER");
    }
}
