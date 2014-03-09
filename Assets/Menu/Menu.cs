using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject MenuBackground;
	private SpriteRenderer MenuSpriteRenderer;
	public Sprite MainMenuSprite;
	public Sprite ControlsMenuSprite;

	public GameObject PressStart;
	public GameObject Controls;

	private bool onControlsScreen = false;
	
	// Use this for initialization
	void Start () {
		MenuSpriteRenderer = MenuBackground.GetComponent<SpriteRenderer>();
		MenuSpriteRenderer.sprite = MainMenuSprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start") && !onControlsScreen){
			Application.LoadLevel("Level");
		}

		if(Input.GetButtonDown("Y") && !onControlsScreen){
			onControlsScreen = true;
			MenuSpriteRenderer.sprite = ControlsMenuSprite;
		}

		if(Input.GetButtonDown("B") && onControlsScreen){
			MenuSpriteRenderer.sprite = MainMenuSprite;
			onControlsScreen = false;
		}


	}
}
