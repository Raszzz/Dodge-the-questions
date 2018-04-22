using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour {

    [SerializeField] Bullet bulletPrefab;
    BossBehaviorPresets behaviorPresets;

    SpriteRenderer spriteRenderer;

    // TODO diff types of shooting and moving patterns

    Vector2 currentPosition;

	// Use this for initialization
	void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        behaviorPresets = GetComponent<BossBehaviorPresets>();
        if(behaviorPresets == null) { print("couldnt load behavior presets"); }
        StartCutScene cutScene = GameObject.FindGameObjectWithTag("CutScene").GetComponent<StartCutScene>();
        cutScene.endCutScene += HandleOnEndCutScene;

	}
    
    void HandleOnEndCutScene(System.Object s, EventArgs e)
    {
        SelectPresetBehavior(); // handle
    }

    void SelectPresetBehavior()
    {
        // random selection of behavior presets
        
        float max_array = Convert.ToSingle(
            behaviorPresets.behaviorArray.Length) - float.Epsilon;
        int randRange = (int)Random.Range(0, max_array);
        BossBehaviorPresets.BossBehavior behaviorData = behaviorPresets.behaviorArray[randRange];

        StopCoroutine("shoot");
        StartCoroutine("shoot", behaviorData);
        StartCoroutine(move(behaviorData.bossSpeed));
        
        


        // default behavior for testing presets
        /*
        BossBehaviorPresets.BossBehavior behaviorTest = new BossBehaviorPresets.BossBehavior(
            15f, 5f, 0.2f, 1.5f
        );
        StopCoroutine("shoot");
        StartCoroutine("shoot", behaviorTest);
        StartCoroutine(move(behaviorTest.bossSpeed));
         */
        
    }


    IEnumerator move(float bossSpeed)
    {
        while(transform.position.x < -9.1f)
        {
            spriteRenderer.flipX = true;
            currentPosition = transform.position;
            currentPosition.x += bossSpeed * Time.deltaTime;
            transform.position = currentPosition;
            yield return null;
        }
        while(transform.position.x > -21f)
        {
            spriteRenderer.flipX = false;
            currentPosition = transform.position;
            currentPosition.x -= bossSpeed * Time.deltaTime;
            transform.position = currentPosition;
            yield return null;
        }
        SelectPresetBehavior();
    }

    // bullet angle needs to be between 1 and 2.
	IEnumerator shoot(BossBehaviorPresets.BossBehavior behaviorData)
    {
        while(true)
        {
            // TODO ping pong and change the value of bullet ange
            // at each co routine
            Bullet bullet = Instantiate(bulletPrefab) as Bullet;
            bullet.speed = behaviorData.bulletSpeed;
            bullet.angleFactor = behaviorData.bulletAngle;
            yield return new WaitForSeconds(behaviorData.shootingFrequency);
        }
    }
}
