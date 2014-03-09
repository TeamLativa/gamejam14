using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryMaterials : MonoBehaviour {

	public GUIText TexteRoche,TexteBois, TexteOs, TexteMetal, TextePlume, TexteLiane;

	private int nbItem,nbRoche, nbBois,nbOs,nbMetal,nbPlume,nbLiane;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (nbRoche);
		TexteRoche.text = nbRoche+"/"+gameObject.GetComponent <NonPhysicsPlayerController>().GetNeededItems("Roche");
		TexteBois.text = nbBois+"/"+gameObject.GetComponent <NonPhysicsPlayerController>().GetNeededItems("Bois");
		TexteOs.text = nbOs+"/"+gameObject.GetComponent <NonPhysicsPlayerController>().GetNeededItems("Os");
		TexteMetal.text = nbMetal+"/"+gameObject.GetComponent <NonPhysicsPlayerController>().GetNeededItems("Metal");
		TextePlume.text = nbPlume+"/"+gameObject.GetComponent <NonPhysicsPlayerController>().GetNeededItems("Plume");
		TexteLiane.text = nbLiane+"/"+gameObject.GetComponent <NonPhysicsPlayerController>().GetNeededItems("Liane");
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
				}
			break;
			case "Bois" :
				if (nbBois<3)
				{
					nbBois++;
					nbItem++;
				}
				break;
			case "Os" :
				if (nbOs<3)
				{
					nbOs++;
					nbItem++;
				}
				break;
			case "Metal" : 
				if (nbMetal<3)
				{
					nbMetal++;
					nbItem++;
				}
				break;
			case "Plume" : 
				if (nbPlume<3)
				{
					nbPlume++;
					nbItem++;
				}
				break;
			case "Liane" : 
			if (nbLiane<3)
				{
					nbLiane++;
					nbItem++;
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
			gameObject.GetComponent <NonPhysicsPlayerController>().GetItems(nbRoche, nbBois, nbOs, nbMetal, nbPlume, nbLiane);
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
