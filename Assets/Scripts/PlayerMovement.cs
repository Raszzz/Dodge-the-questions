using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rgbd2D;
    private float speed = 250f;
    Vector2 currentPosition = new Vector2();
    Vector2 velocity = new Vector2();
    Vector2 counterVelocity = new Vector2();
    Vector2 motionless = new Vector2(0, 0);
	// Use this for initialization
	void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
	}
	
    void OnCollisionEnter2D(Collision2D collision)
    {   
        print("collision detected");
    }
	// Update is called once per frame

	void Update()
    {
        MovementInput();
	}

    void MovementInput()
    {
        velocity = motionless;
        //currentPosition = transform.position;
		if(Input.GetKey(KeyCode.D))
        {
            velocity.x += speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            velocity.x -= speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.W))
        {
            velocity.y += speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            velocity.y -= speed * Time.deltaTime;
        }
        rgbd2D.velocity = velocity;
        //transform.position = currentPosition + velocity;
    }
}