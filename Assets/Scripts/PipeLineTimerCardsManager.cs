using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineTimerCardsManager : MonoBehaviour {
    TimerManager timerManager;
    void Start()
    {
        timerManager = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerManager>();
        //if(timerManager != null) { print("pipline : got ref to timermanager component"); }
    }

    void MessageToAlarm(bool correctAnswer)
    {
        //print("received message from cardsmanager");
        if(correctAnswer)
        {
            //print("Subtract TIME TO THE ALARM");
            timerManager.SendMessage("respondToCardAnwser", true);
            // method from alarm that decreases time
        }
        else if(!correctAnswer)
        {
            //print("ADD TIME TO THE ALARM");
            timerManager.SendMessage("respondToCardAnwser", false);
        }
    }
}
