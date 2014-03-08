using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int HealthPoints = 100;
	public float MovementSpeed = 3.0f;

	bool goingForward = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TakeDamage(int amount){
		HealthPoints -= amount;
	}

	void OnCollisionEnter2D(Collision2D coll){

	}
}
