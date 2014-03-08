using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventoryMaterials : MonoBehaviour {

	private int materialBois;
	private int materialRoche;
	private int materialCorde;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetBoisCount()
	{
		return materialBois;
	}

	public int GetRocheCount()
	{
		return materialRoche;
	}

	public int GetCordeCount()
	{
		return materialCorde;	
	}
}
