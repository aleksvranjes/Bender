using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public float playerSpeed;
    private float theta;
    private float x;
    private float z;

    private bool keyPressed;
    public bool allowMovement = true;

    public Vector3 speed;

    void Start()
    {
        theta = 0;
        x = 0;
        z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMovement)
        {
            keyPressed = false;

            // Transform Movement Code
            if (Input.GetKey("d"))
            {
                theta = 0;
                keyPressed = true;
            }
            if (Input.GetKey("w"))
            { 
                theta = 90;
                keyPressed = true;
            }
            if (Input.GetKey("a"))
            {
                theta = 180;
                keyPressed = true;
            }
            if (Input.GetKey("s"))
            {
                theta = 270;
                keyPressed = true;
            }
            if (Input.GetKey("w") && Input.GetKey("d"))
            {
                theta = 45;
                keyPressed = true;
            }
            if (Input.GetKey("w") && Input.GetKey("a"))
            {
                theta = 135;
                keyPressed = true;
            }
            if (Input.GetKey("s") && Input.GetKey("a"))
            {
                theta = 225;
                keyPressed = true;
            }
            if (Input.GetKey("s") && Input.GetKey("d"))
            {
                theta = 315;
                keyPressed = true;
            }

            if (keyPressed)
            {
                x = playerSpeed;
                z = 0;
            }
            else
            {
                x = 0;
                z = 0;
            }

            theta = theta * Mathf.PI / 180;
            speed = new Vector3((x * Mathf.Cos(theta) - (z * Mathf.Sin(theta))), 0.0f, (x * Mathf.Sin(theta)) + (z * Mathf.Cos(theta)));
            transform.position += speed * Time.deltaTime;
        }  
    }
}