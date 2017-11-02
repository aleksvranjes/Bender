using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int lives;
    public bool invincible;
    public float hitDuration = 3f;
    public float flashLength = 0.5f;

    // Variables for manipulating player when out of bounds
    public float rotationSpeed = 5f;
    public float fallDuration = 2f;
    public bool outOfBounds = false;

    public MeshRenderer mR;

	// Use this for initialization
	void Start ()
    {
        mR = GetComponent<MeshRenderer>();
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
        StartCoroutine(PlayerHit());
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
        StartCoroutine(PlayerFall());
    }

    public IEnumerator PlayerFall()
    {
        float totalTime = 0;
        float currentTime;
        float scaleFactor = 1.0f;
        float time = 0;
        float delta = 0;
        float lastTime = Time.realtimeSinceStartup;

        Vector3 currentRotation;
        Vector3 currentScale;

        while (time < fallDuration)
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
            Debug.Log(currentRotation.y);
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
