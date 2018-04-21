using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start()
    {
        // maybe i'm going to use any of this? idk
        GameObject.DontDestroyOnLoad(gameObject);
        //Scene currentScene = SceneManager.GetActiveScene();
	}
	
}
