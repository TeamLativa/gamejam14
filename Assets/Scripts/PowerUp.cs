using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	//public GameObject otherGameObject;
	private PlayerInventoryPowerUp playerInventory;

	void Awake() {
		//playerInventory = otherGameObject.GetComponent<PlayerInventoryPowerUp>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.name == "Player")
		{
			playerInventory = col.gameObject.GetComponent<PlayerInventoryPowerUp>();
			Destroy(gameObject);
			playerInventory.AddPowerUp(gameObject);
		}
	}
}
