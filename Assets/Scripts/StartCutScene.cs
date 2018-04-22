using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StartCutScene : MonoBehaviour {

	public event EventHandler endCutScene;
    SpriteRenderer image;
    public Sprite ready;
    public Sprite go;

	void Start () {
        image = GetComponent<SpriteRenderer>();
        if(image != null) { print("spr render loaded"); }
		StartCoroutine(animateCutScene());
	}
	
    
    IEnumerator animateCutScene()
    {
        image.sprite = ready;
        yield return new WaitForSeconds(2f);
        image.sprite = go;
        yield return new WaitForSeconds(.5f);
        OnEndCutScene();
    }

    void OnEndCutScene()
    {
        if(endCutScene != null)
        {
            endCutScene(this, EventArgs.Empty);
            DestroyObject(gameObject);
        }
    }
}
