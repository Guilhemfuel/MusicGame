using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	public Sprite Button;
	public Sprite ButtonPressed;
	private SpriteRenderer ButtonRenderer;

	// Use this for initialization
	void Start () {
		ButtonRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pressKey()
	{
		ButtonRenderer.sprite = ButtonPressed;
	}

	public void releasedKey()
	{
		ButtonRenderer.sprite = Button;
	}
}
