using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryPowerUp : MonoBehaviour {

	private string powerUp1, powerUp2;
	private int nbPowerUp;

	private GUITexture rendererPU1;
	private GUITexture rendererPU2;
	public GameObject GuiPowerUP1;
	public GameObject GuiPowerUP2;
	public Texture ForceSprite;
	public Texture BouclierSprite;
	public Texture VitesseSprite;

	public GameObject BouclierDevantSimple;

	// Use this for initialization
	void Start () {
		nbPowerUp = 0;
		rendererPU1 = GuiPowerUP1.GetComponent<GUITexture>();
		rendererPU2 = GuiPowerUP2.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddPowerUp(GameObject pUp){
		if(nbPowerUp == 0){
			powerUp1 = pUp.name;
			nbPowerUp++;

			if(pUp.name == "ForcePowerUp")
				rendererPU1.texture = ForceSprite;
			if(pUp.name == "BouclierPowerUp")
				rendererPU1.texture = BouclierSprite;
			if(pUp.name == "VitessePowerUp")
				rendererPU1.texture = VitesseSprite;

			return;
		}
		if(nbPowerUp == 1){
			powerUp2 = pUp.name;
			nbPowerUp++;

			if(pUp.name == "ForcePowerUp")
				rendererPU2.texture = ForceSprite;
			if(pUp.name == "BouclierPowerUp")
				rendererPU2.texture = BouclierSprite;
			if(pUp.name == "VitessePowerUp")
				rendererPU2.texture = VitesseSprite;

			return;
		}
		if(nbPowerUp == 2){
			powerUp1 = powerUp2;
			powerUp2 = pUp.name;

			rendererPU1.texture = rendererPU2.texture;
			if(pUp.name == "ForcePowerUp")
				rendererPU2.texture = ForceSprite;
			if(pUp.name == "BouclierPowerUp")
				rendererPU2.texture = BouclierSprite;
			if(pUp.name == "VitessePowerUp")
				rendererPU2.texture = VitesseSprite;

			return;
		}
	}

	public bool ConsumePowerUps(){
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
			rendererPU1.texture = null;
			rendererPU2.texture = null;
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
		GameObject bouclier = (GameObject)Instantiate(BouclierDevantSimple);
		bouclier.transform.parent = gameObject.transform;
		bouclier.transform.position = gameObject.transform.position + new Vector3(0.95f,0,0);
	}
	private void PUVitesse()
	{
		gameObject.GetComponent<PlayerController>().ApplyBonusSpeed(1.5f,6.0f);
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
		gameObject.GetComponent<PlayerController>().ApplyBonusSpeed(1.5f,10.0f);
		gameObject.GetComponentInChildren<Gun>().ApplyBonusFireRate(0.75f,10.0f);
	}
	private void PUBouclierBouclier()
	{
	}
	private void PUBouclierVitesse()
	{
		gameObject.GetComponent<PlayerController>().ApplyBonusSpeed(1.5f,6.0f);
		gameObject.GetComponent<PlayerController>().ApplyBonusJump(2.0f,6.0f);
	}
	private void PUVitesseVitesse()
	{
		gameObject.GetComponent<PlayerController>().ApplyBonusSpeed(3.0f,6.0f);
	}
}
