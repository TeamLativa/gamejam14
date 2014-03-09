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
	public GameObject BouclierFire;
	public GameObject BouclierComplet;

	public GameObject FireEffect;
	public GameObject SpeedEffect;
	public GameObject PowerSpeedEffect;
	public GameObject SpeedShieldEffect;

	public AudioClip PowerUpSound;
	public float PowerUpVolume = 0.5f;

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
			powerUp1 = pUp.tag;
			nbPowerUp++;

			if(pUp.tag == "ForcePowerUp")
				rendererPU1.texture = ForceSprite;
			if(pUp.tag == "BouclierPowerUp")
				rendererPU1.texture = BouclierSprite;
			if(pUp.tag == "VitessePowerUp")
				rendererPU1.texture = VitesseSprite;

			return;
		}
		if(nbPowerUp == 1){
			powerUp2 = pUp.tag;
			nbPowerUp++;

			if(pUp.tag == "ForcePowerUp")
				rendererPU2.texture = ForceSprite;
			if(pUp.tag == "BouclierPowerUp")
				rendererPU2.texture = BouclierSprite;
			if(pUp.tag == "VitessePowerUp")
				rendererPU2.texture = VitesseSprite;

			return;
		}
		if(nbPowerUp == 2){
			powerUp1 = powerUp2;
			powerUp2 = pUp.tag;

			rendererPU1.texture = rendererPU2.texture;
			if(pUp.tag == "ForcePowerUp")
				rendererPU2.texture = ForceSprite;
			if(pUp.tag == "BouclierPowerUp")
				rendererPU2.texture = BouclierSprite;
			if(pUp.tag == "VitessePowerUp")
				rendererPU2.texture = VitesseSprite;

			return;
		}
	}

	public bool ConsumePowerUps(){

		if(nbPowerUp == 0)
			return false;
		else
		{

			AudioSource.PlayClipAtPoint(PowerUpSound, transform.position, PowerUpVolume);
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
		GameObject effect = (GameObject)Instantiate(FireEffect);
		effect.transform.parent = gameObject.transform;
		effect.transform.position = gameObject.transform.position + new Vector3(0,-1.5f,0);
		effect.SendMessage("destroyEffect",4.0f);
	}
	private void PUBouclier()
	{
		int side = gameObject.GetComponent<NonPhysicsPlayerController>().facingSideInt();

		GameObject bouclier = (GameObject)Instantiate(BouclierDevantSimple);
		bouclier.transform.parent = gameObject.transform;
		bouclier.transform.position = gameObject.transform.position + new Vector3(side * 0.75f,0,0);

		Vector3 scale = bouclier.transform.localScale;
		scale.x *= side;
		bouclier.transform.localScale = scale;
	}
	private void PUVitesse()
	{
		gameObject.GetComponent<NonPhysicsPlayerController>().ApplyBonusSpeed(1.5f,6.0f);
		GameObject effect = (GameObject)Instantiate(SpeedEffect);
		effect.transform.parent = gameObject.transform;
		effect.transform.position = gameObject.transform.position + new Vector3(0,-1.5f,0);
		effect.SendMessage("destroyEffect",6.0f);
	}
	private void PUForceForce()
	{
		gameObject.GetComponentInChildren<Gun>().ChangeProjectile(2);
		GameObject effect = (GameObject)Instantiate(FireEffect);
		effect.transform.parent = gameObject.transform;
		effect.transform.position = gameObject.transform.position + new Vector3(0,-1.5f,0);
		effect.SendMessage("destroyEffect",6.0f);
	}
	private void PUForceBouclier()
	{
		int side = gameObject.GetComponent<NonPhysicsPlayerController>().facingSideInt();
		
		GameObject bouclier = (GameObject)Instantiate(BouclierFire);
		bouclier.transform.parent = gameObject.transform;
		bouclier.transform.position = gameObject.transform.position + new Vector3(side * 0.75f,0,0);
		
		Vector3 scale = bouclier.transform.localScale;
		scale.x *= side;
        bouclier.transform.localScale = scale;
	}
	private void PUForceVitesse()
	{
		gameObject.GetComponent<NonPhysicsPlayerController>().ApplyBonusSpeed(1.5f,10.0f);
		gameObject.GetComponentInChildren<Gun>().ApplyBonusFireRate(0.75f,10.0f);

		GameObject effect = (GameObject)Instantiate(PowerSpeedEffect);
		effect.transform.parent = gameObject.transform;
		effect.transform.position = gameObject.transform.position + new Vector3(0,-1.5f,0);
		effect.SendMessage("destroyEffect",10.0f);
	}
	private void PUBouclierBouclier()
	{
		int side = gameObject.GetComponent<NonPhysicsPlayerController>().facingSideInt();
		
		GameObject bouclier = (GameObject)Instantiate(BouclierComplet);
		bouclier.transform.parent = gameObject.transform;
		bouclier.transform.position = gameObject.transform.position;
		
		Vector3 scale = bouclier.transform.localScale;
		scale.x *= side;
        bouclier.transform.localScale = scale;
    }
    private void PUBouclierVitesse()
	{
		gameObject.GetComponent<NonPhysicsPlayerController>().ApplyBonusSpeed(1.5f,6.0f);
		gameObject.GetComponent<NonPhysicsPlayerController>().ApplyBonusJump(2.0f,6.0f);

		GameObject effect = (GameObject)Instantiate(SpeedShieldEffect);
		effect.transform.parent = gameObject.transform;
		effect.transform.position = gameObject.transform.position + new Vector3(0,-1.5f,0);
		effect.SendMessage("destroyEffect",6.0f);
	}
	private void PUVitesseVitesse()
	{
		gameObject.GetComponent<NonPhysicsPlayerController>().ApplyBonusSpeed(3.0f,6.0f);

		GameObject effect = (GameObject)Instantiate(SpeedEffect);
		effect.transform.parent = gameObject.transform;
		effect.transform.position = gameObject.transform.position + new Vector3(0,-1.5f,0);
		effect.SendMessage("destroyEffect",6.0f);
	}
}
