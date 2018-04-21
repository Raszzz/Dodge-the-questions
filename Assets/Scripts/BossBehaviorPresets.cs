using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorPresets : MonoBehaviour {

	// Use this for initialization
	public struct BossBehavior
    {
        private readonly float bossSpeed;
        private readonly float bulletSize;
        private readonly float bulletSpeed;
        private readonly float shootingFrequency;
        private readonly float bulletAngle;

        public BossBehavior(float bossspeed, float bulletsize,
                            float bulletspeed, float shootingfrequency,
                            float bulletangle)
        {
            bossSpeed = bossspeed;
            bulletSize = bulletsize;
            bulletSpeed = bulletspeed;
            shootingFrequency = shootingfrequency;
            bulletAngle = bulletangle;
        }
    }
    // reference for how to populate the struct array
    public BossBehavior[] behaviorArray =
                    new BossBehavior[]{
                        new BossBehavior(1, 1, 1, 1, 1) 
                    };
}
