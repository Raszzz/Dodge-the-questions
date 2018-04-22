using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResponseToAnswerAnimation : MonoBehaviour {

    Image image;
    [SerializeField] SpriteRenderer correctSymbol;
    [SerializeField] SpriteRenderer incorrectSymbol;
    bool correctAnswer;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
    public void setAnswer(bool answer)
    {
        if(answer)
        {
            image.sprite = correctSymbol.sprite;
            AnimationCorrectAnswer();
        }
        else
        {
            image.sprite = incorrectSymbol.sprite;
        }
    }
    
    void AnimationCorrectAnswer()
    {
        StartCoroutine(Fade());
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        Vector2 myPosition;
        for(float f = 0; f <= 50; f -= .5f)
        {
            myPosition = image.transform.position;
            myPosition.x += f;
            image.transform.position = myPosition;
            yield return null;    
        }
    }

    IEnumerator Fade()
    {
        Color imageColor;
        for(float f = 1; f >= 0; f -= .1f)
        {
            imageColor = image.color;
            imageColor.a -= .1f;
            image.color = imageColor;
            yield return null;
        }
        imageColor = image.color;
        imageColor.a = 0f;
        image.color = imageColor;
        yield return null;
        GameObject.Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
