using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5f;
    Vector2 currentPosition = new Vector2();
    Vector2 velocity = new Vector2();
    Vector2 counterVelocity = new Vector2();
    Vector2 motionless = new Vector2(0, 0);
	// Use this for initialization
	void Start () {

	}
	
    void OnCollisionEnter2D(Collision2D collision)
    {   
        // each collider probably will trigger its own collisionenter event
        Collider2D collider = collision.collider;
        //collider.offset
        //idea : if i know the offset of the collider I can determine
        // the direction in which a counterVelocity should be applied to
        // this counter velocity vector should be calculated with the normal velocity
        print("collision detected");
    }
	// Update is called once per frame

	void Update()
    {
        MovementInput();
	}

    void MovementInput()
    {
        currentPosition = transform.position;
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
        
        transform.position = currentPosition + velocity;
        velocity = motionless;
    }
}
