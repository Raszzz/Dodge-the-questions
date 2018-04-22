using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResponseToAnswerAnimation : MonoBehaviour {

    public Sprite correctAnswerSymbol;
    public Sprite incorrectAnswerSymbol;
    Image image;
    bool correctAnswer;
	// Use this for initialization
	void Awake()
    {
        image = GetComponent<Image>();
        if(image == null) { print("image component is null"); }
        //if(image.sprite == null) { print("sprt in image is null");}
	}
	
    public void setAnswer(bool answer)
    {   
        //print("enter set answer rtan");
        
        if(answer)
        {
            Sprite correctSymbol = Instantiate(correctAnswerSymbol);
            image.sprite = correctSymbol;
            AnimationCorrectAnswer();
        }
        else
        {
            Sprite incorrectSymbol = Instantiate(incorrectAnswerSymbol);
            image.sprite = incorrectSymbol;
            AnimationIncorrectAnswer();
        }
    }
    
    void AnimationIncorrectAnswer()
    {
        StartCoroutine(Blink());
    }
    void AnimationCorrectAnswer()
    {
        StartCoroutine(Fade());
        StartCoroutine(MoveUp());
    }

    IEnumerator Blink()
    {
        Color blink;
        // starts at one so the first frame is visible
        for(int intervals = 1; intervals < 6; intervals++)
        {
            blink = image.color;
            blink.a = intervals % 2;
            image.color = blink;
            yield return new WaitForSeconds(.5f);    
        }
        blink = image.color;
        blink.a = 0;
        image.color = blink;
        GameObject.Destroy(gameObject);
    }
    IEnumerator MoveUp()
    {
        Vector2 myPosition;
        for(float f = 0; f <= 50; f += .5f)
        {
            myPosition = image.transform.position;
            myPosition.y += f;
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
