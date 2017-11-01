using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public int playerSpeed = 5;
    Vector3 rbVelocity;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // Transform Movement Code
        //if (Input.GetKey("w"))
        //{
        //    transform.position += new Vector3(0.0f, 0.0f, playerSpeed) * Time.deltaTime;
        //}
        //if (Input.GetKey("s"))
        //{
        //    transform.position += new Vector3(0.0f, 0.0f, -playerSpeed) * Time.deltaTime;
        //}
        //if (Input.GetKey("a"))
        //{
        //    transform.position += new Vector3(-playerSpeed, 0.0f, 0.0f) * Time.deltaTime;
        //}
        //if (Input.GetKey("d"))
        //{
        //    transform.position += new Vector3(playerSpeed, 0.0f, 0.0f) * Time.deltaTime;
        //}

        if (Input.GetKey("w"))
        {
            rbVelocity = new Vector3(0.0f, 0.0f, playerSpeed);
            rb.AddForce(rbVelocity * 0.1f, ForceMode.VelocityChange);
        }
        if (Input.GetKey("s"))
        {
            rbVelocity = new Vector3(0.0f, 0.0f, -playerSpeed);
            rb.AddForce(rbVelocity * 0.1f, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rbVelocity = new Vector3(-playerSpeed, 0.0f, 0.0f);
            rb.AddForce(rbVelocity * 0.1f, ForceMode.VelocityChange);
        }
        if (Input.GetKey("d"))
        {
            rbVelocity = new Vector3(playerSpeed, 0.0f, 0.0f);
            rb.AddForce(rbVelocity * 0.1f, ForceMode.VelocityChange);
        }

        if (rbVelocity.magnitude > 1.0f)
        {
            rb.velocity = (rbVelocity.normalized) * 1.0f;
        }
    }
}
