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
    [SerializeField] Image responseToAnswer;
    [SerializeField] Image questionCardPrefab;
	// Use this for initialization
	void Start()
    {
		canvas = GetComponent<Canvas>();
        cardData = GetComponent<CardData>();
	}
	
	// Update is called once per frame
    
	void Update()
    {
        // test
		if(Input.GetKeyDown(KeyCode.Space))
        {
            PutCardOnScreen();
        }
	}

    void CardMessageWhenClicked(TriviaCard.CardMessage cardMessage)
    {
        //TriviaCard triviaCard = cardMessage.gameObject.GetComponent<TriviaCard>();
        //if(triviaCard != null ) { print("this object is a card");}
        /*
        if(triviaCard == null)
        {   
            print("message badly received" + cardMessage);
            return;
        }
         */
        print("received object");
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
        print("wrong answer");
        
        // overlay ResponseToAnswer to the card position
        // load the incorrect sprite
        // Start the animation coroutine wrongAnswer
        // when the coroutine ends delete the object
    }
    void CorrectAnswer(GameObject gameCard)
    {
        print("correct answer");
        Image response = Instantiate(responseToAnswer);
        response.transform.SetParent(canvas.transform, false);
        response.transform.localPosition = gameCard.transform.position;
        GameObject.Destroy(gameCard);
        // overlay ResponseToAnswer to the card position
        // load the incorrect sprite
        // delete the object
    }

    void sendMessageToPlayerGameIsOver()
    {   
        // bugged - but change game object name, maybe now its working
        /*
        GameObject player = GameObject.Find("PlayerCharacter");
        player.SendMessage("GameOver");
         */
    }

    void PutCardOnScreen()
    {
        // determine space for card
        //if(questionCards[0] == null ) {print("yes its empty");}
        int indexSpace = SpaceForCardOnTheScreen();
        if(indexSpace == 4) { sendMessageToPlayerGameIsOver(); }
        int randRange = selectProperRandom(0, cardData.cards.Length);
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
        print("array len" + questionCards.Length);
        for(int index = 0; index < questionCards.Length; index++)
        {
            if(questionCards[index] == null){ return index; }
        }
        return 4; // 4 means there is no space for another card, just end the game
    }

    int selectProperRandom(float min, float max)
    {
        float randNum = Random.Range(min, max);
        float middleRange = (max - min) / 2;
        if(randNum < middleRange)
        {
            return (int)randNum;
        }
        else
        {
            randNum -= float.Epsilon;
            return (int)randNum;
        }
    }
}
