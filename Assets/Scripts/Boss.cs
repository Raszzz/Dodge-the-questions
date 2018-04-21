using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] Bullet bulletPrefab;
    float speed = 3;

    // TODO test presets and put that on to a 
    // presets array and randomly call a new behavior preset at the end
    // of the move coroutine

    // TODO diff types of shooting and moving patterns

    Vector2 currentPosition;

	// Use this for initialization
	void Start()
    {
        SelectPresetBehavior();
	}
    
    void SelectPresetBehavior()
    {
        // randomly select a behavior preset from a behavior
        // preset array here
        StopCoroutine("shoot");
        StartCoroutine(shoot(1f, 4f, .5f, 1.5f));
        StartCoroutine(move(1f));
    }


    IEnumerator move(float bossSpeed)
    {
        while(transform.position.x < -9.5f)
        {
            currentPosition = transform.position;
            currentPosition.x += bossSpeed * Time.deltaTime;
            transform.position = currentPosition;
            yield return null;
        }
        while(transform.position.x > -20f)
        {
            currentPosition = transform.position;
            currentPosition.x -= bossSpeed * Time.deltaTime;
            transform.position = currentPosition;
            yield return null;
        }
        SelectPresetBehavior();
    }

    // bullet angle needs to be between 1 and 2.
	IEnumerator shoot(float bulletSize, float bulletSpeed,
                      float bulletInterval, float bulletAngle)
    {
        while(true)
        {
            // TODO ping pong and change the value of bullet ange
            // at each co routine
            Bullet bullet = Instantiate(bulletPrefab) as Bullet;
            bullet.size = bulletSize;
            bullet.speed = bulletSpeed;
            bullet.angleFactor = bulletAngle;
            yield return new WaitForSeconds(bulletInterval);
        }
    }
}
