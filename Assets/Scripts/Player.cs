using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

    Rigidbody2D rgbd2D;
    private float speed = 250f;

    bool pausedForCutscene = true;
    TriviaCardsManager triviaCardsManager;
    // temp
    //int cardsOnFace = 0;
    SpriteRenderer spriteRenderer;
    Vector2 velocity;
    Vector2 motionless = new Vector2(0, 0);
	// Use this for initialization
	void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null) { print("sprite render in player is null"); }
        triviaCardsManager = GameObject.FindGameObjectWithTag("tcManager").GetComponent<TriviaCardsManager>();
        if(triviaCardsManager != null){ print("player connected to trivia card manager"); }
        StartCutScene cutScene = GameObject.FindGameObjectWithTag("CutScene").GetComponent<StartCutScene>();
        cutScene.endCutScene += HandleOnEndCutScene;
        StartCoroutine(AnimateSprite());
	}

    void HandleOnEndCutScene(System.Object s, EventArgs e)
    {
        pausedForCutscene = false;
    }
    void GameOver()
    {
        SceneManager.LoadScene("LoseScreen");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            other.gameObject.SendMessage("DestroyBullet");
            triviaCardsManager.SendMessage("PlayerHit");
        }
    }
     
	// Update is called once per frame

	void Update()
    {
        if(pausedForCutscene) { return; }
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

    IEnumerator AnimateSprite()
    {
        for(int i = 0;; i++)
        {
            if(i > 10) { i = 0;}
            spriteRenderer.flipX = i % 2 == 0;
            yield return new WaitForSeconds(.5f);
        }
    }
}