using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TriviaCardsManager : MonoBehaviour {
    
    Image[] questionCards = new Image[3];
    Canvas canvas;
    CardData cardData;

    AudioSource audioSource;

    [SerializeField] AudioClip correctAnswerAudio;
    [SerializeField] AudioClip wrongAnswerAudio;
    PipeLineTimerCardsManager pipeLineToTimer;
    [SerializeField] Image responseToAnswer;
    [SerializeField] Image questionCardPrefab;
	// Use this for initialization
	void Start()
    {
		canvas = GetComponent<Canvas>();
        cardData = GetComponent<CardData>();
        audioSource = GetComponent<AudioSource>();
        pipeLineToTimer = GameObject.FindGameObjectWithTag("PipeLine").GetComponent<PipeLineTimerCardsManager>();
        //if(pipeLineToTimer != null) { print("card mnger: conneceted to the pipeline component");}
	}
	
	// Update is called once per frame
    
    void PlayerHit()
    {
        PutCardOnScreen();
    }

    void CardMessageWhenClicked(TriviaCard.CardMessage cardMessage)
    {

        if(cardMessage.answer)
        {
            CorrectAnswer(cardMessage.gameObject);
        }
        else
        {
            WrongAnswer(cardMessage.gameObject);
        }  
    }


    void WrongAnswer(GameObject gameCard)
    {
        audioSource.PlayOneShot(wrongAnswerAudio);
        pipeLineToTimer.SendMessage("MessageToAlarm", false);
        //print("wrong answer");
        Image response = Instantiate(responseToAnswer);
        response.transform.SetParent(canvas.transform, false);
        response.transform.localPosition = gameCard.transform.localPosition;
        response.GetComponent<ResponseToAnswerAnimation>().setAnswer(false);
        StartCoroutine(InvokeDestroyGameObject(gameCard));
    }

    IEnumerator InvokeDestroyGameObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.5f * 5f);
        GameObject.Destroy(gameObject);
        print("card destroy after 3 seconds ish");
    }
    void CorrectAnswer(GameObject gameCard)
    {
        audioSource.PlayOneShot(correctAnswerAudio);
        pipeLineToTimer.SendMessage("MessageToAlarm", true);
        //print("correct answer");
        Image response = Instantiate(responseToAnswer);
        response.transform.SetParent(canvas.transform, false);
        response.transform.localPosition = gameCard.transform.localPosition;
        response.GetComponent<ResponseToAnswerAnimation>().setAnswer(true);
        GameObject.Destroy(gameCard);
    }

    void sendMessageToPlayerGameIsOver()
    {   
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) { return; }
        player.SendMessage("GameOver");
    }

    void PutCardOnScreen()
    {
        int indexSpace = SpaceForCardOnTheScreen();
        if(indexSpace == 4)
        {
            sendMessageToPlayerGameIsOver();
            return;
        }
        float max_array = Convert.ToSingle(cardData.cards.Length) - float.Epsilon;
        int randRange = (int)Random.Range(0, max_array);
        CardData.Card triviaCardData = cardData.cards[randRange];
        
        // initialize the card data 
        questionCards[indexSpace] = Instantiate(questionCardPrefab) as Image;
        TriviaCard triviaCard = questionCards[indexSpace].GetComponent<TriviaCard>();
        if(triviaCard == null) { print("trivia card is null");}
        triviaCard.setupTriviaCardData(triviaCardData.Question,
        triviaCardData.Answer1, triviaCardData.Answer2,
        triviaCardData.correctAnswer);

        // make it a child of the canvas object and position it correctly on the screen
        Vector2 offsetPositionCard = new Vector2
                            (0, triviaCard.offsetDistanceBetweenCards * indexSpace);
        Vector2 TransformPositionOfCard = questionCards[indexSpace].transform.position;
        TransformPositionOfCard -= offsetPositionCard;
        questionCards[indexSpace].transform.SetParent(canvas.transform, false);   
        questionCards[indexSpace].transform.localPosition = TransformPositionOfCard;
    }

    int SpaceForCardOnTheScreen()
    {
        for(int index = 0; index < questionCards.Length; index++)
        {
            if(questionCards[index] == null){ return index; }
        }
        return 4; // 4 means there is no space for another card, just end the game
    }


}
