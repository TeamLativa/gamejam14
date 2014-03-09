using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject MenuBackground;
	private SpriteRenderer MenuSpriteRenderer;
	public Sprite MainMenuSprite;
	public Sprite ControlsMenuSprite;
	public Sprite CreditsMenuSprite;

	public GameObject PressStart;
	public GameObject Controls;

	private string shownScreen;
	
	// Use this for initialization
	void Start () {
		MenuSpriteRenderer = MenuBackground.GetComponent<SpriteRenderer>();
		MenuSpriteRenderer.sprite = MainMenuSprite;
		shownScreen = "Start";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Startbutton") && shownScreen == "Start"){
			Application.LoadLevel("Level");
		}

		if(Input.GetButtonDown("Ybutton") && shownScreen == "Start"){
			shownScreen = "Controls";
			MenuSpriteRenderer.sprite = ControlsMenuSprite;
		}

		if(Input.GetButtonDown("Bbutton") && shownScreen == "Controls"){
			MenuSpriteRenderer.sprite = MainMenuSprite;
			shownScreen = "Start";
		}

		if(Input.GetButtonDown("Backbutton") && shownScreen == "Start"){
			MenuSpriteRenderer.sprite = CreditsMenuSprite;
			shownScreen = "Credits";
		}

		if(Input.GetButtonDown("Bbutton") && shownScreen == "Credits"){
			MenuSpriteRenderer.sprite = MainMenuSprite;
			shownScreen = "Start";
		}

		if(shownScreen == "Start")
			PressStart.renderer.enabled = Controls.renderer.enabled = true;
		else
			PressStart.renderer.enabled = Controls.renderer.enabled = false;
	}
}
