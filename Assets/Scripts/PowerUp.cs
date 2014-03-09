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
		Destroy(gameObject, 15.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			playerInventory = col.gameObject.GetComponent<PlayerInventoryPowerUp>();
			playerInventory.AddPowerUp(gameObject);
			Destroy(gameObject);
		}
	}
}
