using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryPowerUp : MonoBehaviour {

	private string powerUp1, powerUp2;
	private int nbPowerUp;

	private SpriteRenderer rendererPU1;
	private SpriteRenderer rendererPU2;
	public GameObject GuiPowerUP1;
	public GameObject GuiPowerUP2;
	public Sprite ForceSprite;
	public Sprite BouclierSprite;
	public Sprite VitesseSprite;

	// Use this for initialization
	void Start () {
		nbPowerUp = 0;
		rendererPU1 = GuiPowerUP1.GetComponent<SpriteRenderer>();
		rendererPU2 = GuiPowerUP2.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddPowerUp(GameObject pUp){
		if(nbPowerUp == 0){
			powerUp1 = pUp.name;
			nbPowerUp++;

			if(pUp.name == "ForcePowerUp")
				rendererPU1.sprite = ForceSprite;
			if(pUp.name == "BouclierPowerUp")
				rendererPU1.sprite = BouclierSprite;
			if(pUp.name == "VitessePowerUp")
				rendererPU1.sprite = VitesseSprite;

			return;
		}
		if(nbPowerUp == 1){
			powerUp2 = pUp.name;
			nbPowerUp++;

			if(pUp.name == "ForcePowerUp")
				rendererPU2.sprite = ForceSprite;
			if(pUp.name == "BouclierPowerUp")
				rendererPU2.sprite = BouclierSprite;
			if(pUp.name == "VitessePowerUp")
				rendererPU2.sprite = VitesseSprite;

			return;
		}
		if(nbPowerUp == 2){
			powerUp1 = powerUp2;
			powerUp2 = pUp.name;

			rendererPU1.sprite = rendererPU2.sprite;
			if(pUp.name == "ForcePowerUp")
				rendererPU2.sprite = ForceSprite;
			if(pUp.name == "BouclierPowerUp")
				rendererPU2.sprite = BouclierSprite;
			if(pUp.name == "VitessePowerUp")
				rendererPU2.sprite = VitesseSprite;

			return;
		}
	}

	public bool ConsumePowerUps(){
		Debug.Log(nbPowerUp);
		if(nbPowerUp == 0)
			return false;
		else
		{
			if(nbPowerUp == 1)
			{
				if(powerUp1 == "ForcePowerUp")
					PUForce();
				if(powerUp1 == "BouclierPowerUp")
					PUBouclier();
				if(powerUp1 == "VitessePowerUp")
					PUVitesse();
			}
			if(nbPowerUp == 2)
			{
				if(powerUp1 == "ForcePowerUp" && powerUp2 == "ForcePowerUp")
					PUForceForce();
				if((powerUp1 == "ForcePowerUp" && powerUp2 == "BouclierPowerUp") || (powerUp2 == "ForcePowerUp" && powerUp1 == "BouclierPowerUp"))
					PUForceBouclier();
				if((powerUp1 == "ForcePowerUp" && powerUp2 == "VitessePowerUp") || (powerUp2 == "ForcePowerUp" && powerUp1 == "VitessePowerUp"))
					PUForceVitesse();
				if(powerUp1 == "BouclierPowerUp" && powerUp2 == "BouclierPowerUp")
					PUBouclierBouclier();
				if((powerUp1 == "BouclierPowerUp" && powerUp2 == "VitessePowerUp") || (powerUp2 == "BouclierPowerUp" && powerUp1 == "VitessePowerUp"))
					PUBouclierVitesse();
				if(powerUp1 == "VitessePowerUp" && powerUp2 == "VitessePowerUp")
					PUVitesseVitesse();
			}
			nbPowerUp = 0;
			rendererPU1.sprite = null;
			rendererPU2.sprite = null;
			return true;
		}
		
	}

	public int GetPowerUpsCount(){
		return nbPowerUp;
	}

	public string GetFirstPowerUpName(){
		if(nbPowerUp >= 1)
			return powerUp1;
		else
			return "";
	}

	public string GetSecondPowerUpName(){
		if(nbPowerUp == 2)
			return powerUp2;
		else
			return "";
	}

	private void PUForce()
	{
		gameObject.GetComponentInChildren<Gun>().ChangeProjectile(1);
	}
	private void PUBouclier()
	{
	}
	private void PUVitesse()
	{
	}
	private void PUForceForce()
	{
		gameObject.GetComponentInChildren<Gun>().ChangeProjectile(2);
	}
	private void PUForceBouclier()
	{
	}
	private void PUForceVitesse()
	{
	}
	private void PUBouclierBouclier()
	{
	}
	private void PUBouclierVitesse()
	{
	}
	private void PUVitesseVitesse()
	{
	}
}
