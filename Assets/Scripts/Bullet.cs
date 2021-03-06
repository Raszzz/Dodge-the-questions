﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float size = 1;
    Vector2 direction = new Vector2();
    public float speed = 5;
    public float angleFactor = 1f; // down 
    Vector2 currentPosition;

	void Start()
    {
		transform.position = GameObject.Find("Boss").transform.position;
        transform.localScale *=  size;
        
        // this resizing code doesn't seem to be working.
        Bounds colliderBound = GetComponent<Collider2D>().bounds;
        
        Vector2 colliderBoundsSize = colliderBound.size;
        Vector2 expandedcolliderBounds = colliderBoundsSize * size;
        print("size of collider in bullet: " + colliderBoundsSize);
        print("expand size of bound: " + expandedcolliderBounds);

        colliderBound.size = expandedcolliderBounds;
        colliderBound.Expand(size);
        print("size of collider after expansion: " + colliderBound.size);


        direction.y = Mathf.Sin(angleFactor * Mathf.PI);
        direction.x = Mathf.Cos(angleFactor * Mathf.PI);  
	}

	void Update()
    {
        bulletMovement();
	}

    void bulletMovement()
    {
        currentPosition = transform.position;
        currentPosition += direction * (speed * Time.deltaTime);
		transform.position = currentPosition;
        cleanUpFromScene();
    }

    void cleanUpFromScene()
    {
        if(transform.position.y < -7f)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        DestroyObject(gameObject);
    }
}
