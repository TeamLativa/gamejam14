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
					gameObject.GetComponent<NonPhysicsPlayerController>().setItems("Roche", nbRoche);
				}
			break;
			case "Bois" :
				if (nbBois<3)
				{
					nbBois++;
					nbItem++;
					gameObject.GetComponent<NonPhysicsPlayerController>().setItems("Bois", nbBois);
				}
				break;
			case "Os" :
				if (nbOs<3)
				{
					nbOs++;
					nbItem++;
					gameObject.GetComponent<NonPhysicsPlayerController>().setItems("Os", nbOs);
				}
				break;
			case "Metal" : 
				if (nbMetal<3)
				{
					nbMetal++;
					nbItem++;
					gameObject.GetComponent<NonPhysicsPlayerController>().setItems("Metal", nbMetal);
				}
				break;
			case "Plume" : 
				if (nbPlume<3)
				{
					nbPlume++;
					nbItem++;
					gameObject.GetComponent<NonPhysicsPlayerController>().setItems("Plume", nbPlume);
				}
				break;
			case "Liane" : 
			if (nbLiane<3)
				{
					nbLiane++;
					nbItem++;
					gameObject.GetComponent<NonPhysicsPlayerController>().setItems("Liane", nbLiane);
				}
				break;
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
