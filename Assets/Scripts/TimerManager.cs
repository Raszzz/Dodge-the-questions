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
        public event EventHandler TooMuchTime;
        public void setTime(int time)
        {
            if(time < 0)
            {
                OnTimeOut();
                return;
            }
            else if(time > 100 * 2)
            {
                OnTooMuchTime();
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

        public void OnTooMuchTime()
        {
            if(TooMuchTime != null)
            {
                TooMuchTime(this, EventArgs.Empty);
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

    void respondToCardAnwser(bool correctAnswer)
    {
        int time = timer.getTime();
        if(correctAnswer)
        {
            int calcTime = time - 1;
            print("time set: " + calcTime);
            timer.setTime(calcTime);          
        }
        else if(!correctAnswer)
        {
            timer.setTime(time + 15);
        }
        handleTimerTextAnimation();
    }

    void handleTimerTextAnimation()
    {
        StopCoroutine("AnimateTextTimer");
        StartCoroutine("AnimateTextTimer");
    }

    private Timer timer;

	// Use this for initialization

	void Start()
    {
        timer = new Timer(initialTime);
        timer.TimeOut += HandleTimeOut;
        timer.TooMuchTime += HandleTooMuchTime;
		timeText = GameObject.Find("timeText").GetComponent<Text>();
        StartCutScene cutScene = GameObject.FindGameObjectWithTag("CutScene").GetComponent<StartCutScene>();
        cutScene.endCutScene += HandleOnEndCutScene;
	}

    void HandleOnEndCutScene(System.Object s, EventArgs e)
    {
        StartCoroutine("timerHandle");
    }
	
    public void HandleTooMuchTime(System.Object s, EventArgs e)
    {
        StopCoroutine("timerHandle");
        SceneManager.LoadScene("LoseTimeScreen");
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
            // TODO handle resetting the timer when WHAT?? OMG WHAT?????
            timer.tickDown();
            timeText.text = timer.timeString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AnimateTextTimer()
    {
        Vector2 initScale = timeText.transform.localScale;
        Vector2 scaleFactor;
        Vector2 scaleSize = initScale;
        for(float f = 1; f >= 0; f -= .1f)
        {
            scaleFactor = initScale * f;
            scaleSize = initScale + scaleFactor;
            timeText.transform.localScale = scaleSize;
            yield return null;
        }
        timeText.transform.localScale = initScale;
    }
}

