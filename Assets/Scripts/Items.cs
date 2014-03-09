using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {

	private PlayerInventoryMaterials playerInventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			playerInventory = col.gameObject.GetComponent<PlayerInventoryMaterials>();
			playerInventory.AddItem(gameObject);
			Destroy(gameObject);
		}
	}
}
