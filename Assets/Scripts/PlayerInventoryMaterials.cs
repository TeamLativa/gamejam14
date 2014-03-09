using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryMaterials : MonoBehaviour {

	public GameObject GuiRoche,GuiBois,GuiOs,GuiMetal,GuiPlume,GuiLiane;

	private int nbItem,nbRoche, nbBois,nbOs,nbMetal,nbPlume,nbLiane;

	private GUITexture rendererRoche,rendererBois, rendererOs, rendererMetal, rendererPlume, rendererLiane;

	private GUIText texteRoche,texteBois, texteOs, texteMetal, textePlume, texteLiane;

	private int materialRoche,materialBois,materialOs,materialMetal,materialPlume,materialLiane;

	// Use this for initialization
	void Start () {/*
		rendererRoche = GuiRoche.GetComponent<GUITexture> ();
		rendererBois = GuiBois.GetComponent<GUITexture> ();
		rendererOs = GuiOs.GetComponent<GUITexture> ();
		rendererMetal = GuiMetal.GetComponent<GUITexture> ();
		rendererPlume = GuiPlume.GetComponent<GUITexture> ();
		rendererLiane = GuiLiane.GetComponent<GUITexture> ();*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddItem(GameObject item)
	{
		switch(item.name)
		{
			case "Roche" : 
				if (nbRoche<3)
				{
					nbRoche++;
					nbItem++;
					//texteRoche.text = nbRoche.ToString ();
				}
			break;
			case "Bois" :
				if (nbBois<3)
				{
					nbBois++;
					nbItem++;
					//texteBois.text = nbBois.ToString ();
				}
				break;
			case "Os" :
				if (nbOs<3)
				{
					nbOs++;
					nbItem++;
					//texteOs.text = nbOs.ToString ();
				}
				break;
			case "Metal" : 
				if (nbMetal<3)
				{
					nbMetal++;
					nbItem++;
					//texteMetal.text = nbMetal.ToString ();
				}
				break;
			case "Plume" : 
				if (nbPlume<3)
				{
					nbPlume++;
					nbItem++;
					//textePlume.text = nbPlume.ToString ();
				}
				break;
			case "Liane" : 
			if (nbLiane<3)
				{
					nbLiane++;
					nbItem++;
					//texteLiane.text = nbLiane.ToString ();
				}
				break;
		}
	}

	public bool UseItem()
	{
		if(nbItem==0)
			return false;
		else
		{
			gameObject.GetComponent <PlayerController>().GetItems(nbRoche, nbBois, nbOs, nbMetal, nbPlume, nbLiane);
			return true;
		}
	}

	public void setRoche(int roche)
	{
		nbRoche = roche;
	}

	public void setBois(int bois)
	{
		nbBois = bois;
	}

	public void setOs(int os)
	{
		nbOs = os;
	}

	public void setMetal(int metal)
	{
		nbMetal = metal;
	}

	public void setPlume(int plume)
	{
		nbPlume = plume;
	}

	public void setLiane(int liane)
	{
		nbLiane = liane;
	}
}
