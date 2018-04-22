using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorPresets : MonoBehaviour {

	public struct BossBehavior
    {
        public readonly float bossSpeed;
        public readonly float bulletSpeed;
        public readonly float shootingFrequency;
        public readonly float bulletAngle;

        public BossBehavior(float bossspeed, float bulletspeed, 
            float shootingfrequency, float bulletangle)
        {
            bossSpeed = bossspeed;
            bulletSpeed = bulletspeed;
            shootingFrequency = shootingfrequency;
            bulletAngle = bulletangle;
        }
    }
    // reference for how to populate the struct array
    public BossBehavior[] behaviorArray = new BossBehavior[]{
        new BossBehavior(5f, 4f, .3f, 1.4f),
        new BossBehavior(8f, 2f, .1f, 1.6f),
        new BossBehavior(7f, 4f, .2f, 1.55f),
        new BossBehavior(6f, 8f, 0.3f, 1.48f),
        new BossBehavior(10f, 6f, 0.4f, 1.52f),
        new BossBehavior(6f, 10f, 0.2f, 1.5f),
        new BossBehavior(15f, 5f, 0.2f, 1.5f),
        new BossBehavior(15f, 3f, 0.1f, 1.3f)
    };
}
