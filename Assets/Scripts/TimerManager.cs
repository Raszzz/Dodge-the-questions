using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour {

    // maybe have an alarm class/ yes?
    private int initialTime = 100;
    private Text timeText;
    private class Timer
    {
        public int minutes {get; set;}
        public int seconds {get; set;}

        public event EventHandler TimeOut;
        public void setTime(int time)
        {
            if(time < 0)
            {
                OnTimeOut();
                return;
            }
            minutes = time / 60;
            seconds = time % 60;
        }

        public int getTime()
        {
            return seconds + minutes * 60;
        }

        
        public void tickDown()
        {
            if(seconds - 1 <= 0)
            {
                if(minutes - 1 <= 0)
                {
                    OnTimeOut();
                    seconds = 0;
                    minutes = 0;
                }
                else
                {
                    minutes--;
                    seconds = 59;
                }
            }
            else
            {
                seconds--;
            }
        }

        public void OnTimeOut()
        {
            if(TimeOut != null)
            {
                TimeOut(this, EventArgs.Empty);
            }
        }
        public Timer(int time)
        {
            minutes = time / 60;
            seconds = time % 60;
            //print("min and secs: " + minutes + " " + seconds);
        }

        public string timeString()
        {
            return minutes + ":" + seconds;
        }
    }

    private Timer timer;

	// Use this for initialization

	void Start()
    {
        timer = new Timer(initialTime);
        timer.TimeOut += HandleTimeOut;
		timeText = GameObject.Find("timeText").GetComponent<Text>();
        StartCoroutine("timerHandle");
	}
	
    public void HandleTimeOut(System.Object s, EventArgs e)
    {
        StopCoroutine("timerHandle");
        SceneManager.LoadScene("WinScreen");
        // start animation/transition or whatever
        // change scene to win screen
    }

    IEnumerator timerHandle()
    {
        while(true)
        {
            // TODO handle resetting the timer when 
            timer.tickDown();
            timeText.text = timer.timeString();
            yield return new WaitForSeconds(1f);
        }
    }
}

