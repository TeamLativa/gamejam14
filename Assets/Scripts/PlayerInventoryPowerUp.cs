using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryPowerUp : MonoBehaviour {

	private GameObject powerUp1, powerUp2;
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
			powerUp1 = pUp;
			nbPowerUp++;
		}
		if(nbPowerUp == 1){
			powerUp2 = pUp;
			nbPowerUp++;
		}
		if(nbPowerUp == 2){
			powerUp1 = powerUp2;
			powerUp2 = pUp;
		}
	}

	public bool ConsumePowerUps(){
		if(nbPowerUp == 0)
			return false;
		else
		{
			if(nbPowerUp == 1)
			{
				if(powerUp1.name == "ForcePowerUp")
					PUForce();
				if(powerUp1.name == "BouclierPowerUp")
					PUBouclier();
				if(powerUp1.name == "VitessePowerUp")
					PUVitesse();
			}
			if(nbPowerUp == 2)
			{
				if(powerUp1.name == "ForcePowerUp" && powerUp2.name == "ForcePowerUp")
					PUForceForce();
				if((powerUp1.name == "ForcePowerUp" && powerUp2.name == "BouclierPowerUp") || (powerUp2.name == "ForcePowerUp" && powerUp1.name == "BouclierPowerUp"))
					PUForceBouclier();
				if((powerUp1.name == "ForcePowerUp" && powerUp2.name == "VitessePowerUp") || (powerUp2.name == "ForcePowerUp" && powerUp1.name == "VitessePowerUp"))
					PUForceVitesse();
				if(powerUp1.name == "BouclierPowerUp" && powerUp2.name == "BouclierPowerUp")
					PUBouclierBouclier();
				if((powerUp1.name == "BouclierPowerUp" && powerUp2.name == "VitessePowerUp") || (powerUp2.name == "BouclierPowerUp" && powerUp1.name == "VitessePowerUp"))
					PUBouclierVitesse();
				if(powerUp1.name == "VitessePowerUp" && powerUp2.name == "VitessePowerUp")
					PUVitesseVitesse();
			}
			nbPowerUp = 0;
			return true;
		}
		
	}

	public int GetPowerUpsCount(){
		return nbPowerUp;
	}

	public string GetFirstPowerUpName(){
		if(nbPowerUp >= 1)
			return powerUp1.name;
		else
			return "";
	}

	public string GetSecondPowerUpName(){
		if(nbPowerUp == 2)
			return powerUp2.name;
		else
			return "";
	}

	private void PUForce()
	{
	}
	private void PUBouclier()
	{
	}
	private void PUVitesse()
	{
	}
	private void PUForceForce()
	{
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
