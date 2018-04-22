using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
	// Use this for initialization
	void Awake()
    {   
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            DestroyObject(gameObject);
        }
        GameObject.DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += HandleSceneLoading;
	}

    void HandleSceneLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "TitleScreen")
        {
            HandleTitleScene();
        }
        else if(scene.name == "Tutorial")
        {
            HandleTutorialScene();
        }
        else if(scene.name != "Main")
        {
            HandleEndGameScenes();
        }
        // else than its the main screen
    }
    

	void HandleTitleScene()
    {
        // get reference to buttons and load scenes main or tutorial
        Button Playbutton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();
        if(Playbutton == null) { print("could not find play button on title screen"); }
        Playbutton.onClick.AddListener(LoadMainScene);
        Button Tutorialbutton = GameObject.FindGameObjectWithTag("TutorialButton").GetComponent<Button>();
        if(Tutorialbutton == null) { print("could not find tutorial button on title screen"); }
        Tutorialbutton.onClick.AddListener(LoadTutorial);
    }

    void HandleTutorialScene()
    {
        Button Backbutton = GameObject.FindGameObjectWithTag("MenuButton").GetComponent<Button>();
        if(Backbutton == null) { print("could not find back button on tutorial screen"); }
        Backbutton.onClick.AddListener(BackToTitle);
    }
    void HandleEndGameScenes()
    {
        Button replaybutton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();
        if(replaybutton == null) { print("could not find play button on end screen"); }
        replaybutton.onClick.AddListener(LoadMainScene);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
