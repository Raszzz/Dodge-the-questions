using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour {

	public struct Card
    {
        public readonly string Question;
        public readonly string Answer1;
        public readonly string Answer2;
        public readonly int correctAnswer;

        public Card(string question, string answer1,
                    string answer2, int correctanswer)
        {
            Question = question;
            Answer1 = answer1;
            Answer2 = answer2;
            correctAnswer = correctanswer;
        }
    }

    public Card[] cards = new Card[]{
        new Card("Who are you?", "I'm the correct", "I'm the incorrect.", 1),
        new Card("This question is", "false", "true", 2),
        new Card("110000011100101010101100011001001", "0", "1", 2),
        new Card("yo mama is:", "ugly", "beautiful", 1),
        new Card("my mama is:", "ugly", "beautiful", 2),
        new Card("this isn't incorrect", "correct", "what?", 1),
        new Card("is this correct?", "yes", "no", 1),
        new Card("this card is:", "invalid", "valid", 2),
        new Card("\"\"","null","empty",2)
    };
}
