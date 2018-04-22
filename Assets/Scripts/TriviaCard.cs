using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaCard : MonoBehaviour {

    public struct CardMessage
    {
        public GameObject gameObject;
        public bool answer;

        public CardMessage(GameObject game_object, bool answer)
        {
            this.answer = answer;
            gameObject = game_object;
        }
    }
    public int offsetDistanceBetweenCards = 148;
    Text AnswerText1;
    Text Question;
    Text AnswerText2;
    int CorrectAnswer;
    Button[] answers;
	// Use this for initialization
	void Awake () {
        answers = gameObject.GetComponentsInChildren<Button>();
        answers[0].onClick.AddListener(clickOnAnswer1);
        answers[1].onClick.AddListener(clickOnAnswer2);
        Question = gameObject.GetComponentInChildren<Text>();
        AnswerText1 = answers[0].gameObject.GetComponentInChildren<Text>();
        AnswerText2 = answers[1].gameObject.GetComponentInChildren<Text>();
        /*
        print("button answers retrived: " + answers.Length); 
        print("text answer[0]: " + AnswerText1.text);
        print("text answer[1]: " + AnswerText2.text);
        print("question text retrived: " + Question.text);
         */
	}

    public void setupTriviaCardData(string questionText, string answerText1,
                                    string answerText2, int correctanswer)
    {
        Question.text = questionText;
        AnswerText1.text = answerText1;
        AnswerText2.text = answerText2;
        CorrectAnswer = correctanswer;
    }

    void clickOnAnswer1()
    {   
        handleAnswerEvent(1);
    }

    void clickOnAnswer2()
    {
        handleAnswerEvent(2);
    }

    void handleAnswerEvent(int answer)
    {
        disableButtons();
        if(checkIfCorrectAnswer(answer))
        {
            OnAnswerClicked(true);
        }
        else
        {
            OnAnswerClicked(false);
        }
    }
    void disableButtons()
    {
        answers[0].enabled = false;
        answers[1].enabled = false;
    }

    bool checkIfCorrectAnswer(int answerNumber)
    {
        return (answerNumber == CorrectAnswer)? true: false;
    }
	
    void OnAnswerClicked(bool answer)
    {
        CardMessage cardMessage; 
        TriviaCardsManager triviaCardsManager = gameObject.GetComponentInParent<TriviaCardsManager>();
        if(answer)
        {
            cardMessage = new CardMessage(gameObject, true);
            triviaCardsManager.SendMessage("CardMessageWhenClicked", cardMessage);
        }
        else if(!answer)
        {
            cardMessage = new CardMessage(gameObject, false);
            triviaCardsManager.SendMessage("CardMessageWhenClicked", cardMessage);
            // send message to triviacardmanager object and send the gameObject 
        }
    }
}
