using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    Rigidbody2D rgbd2D;
    private float speed = 250f;

    int cardsOnFace = 0;
    Vector2 velocity;
    Vector2 motionless = new Vector2(0, 0);
	// Use this for initialization
	void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
	}
	
    void CheckIfGameOver()
    {
        if(cardsOnFace >= 4)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            other.gameObject.SendMessage("DestroyBullet");
            cardsOnFace++;
            CheckIfGameOver();
            // create instance of trivia card
        }
    }
     
	// Update is called once per frame

	void Update()
    {
        MovementInput();
        
	}

    void MovementInput()
    {
        velocity = motionless;
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

    }
}