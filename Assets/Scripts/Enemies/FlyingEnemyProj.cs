﻿using UnityEngine;
using System.Collections;

public class FlyingEnemyProj : MonoBehaviour {

	public float StunTime = 1.0f;

	public GameObject CollisionParticule;

	public float TimeToLive = 3.0f;
	private float lifeTimer = 0.0f;

	void Update(){
		lifeTimer += Time.deltaTime;
		if(lifeTimer >= TimeToLive)
			Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Player"){
			coll.gameObject.SendMessage("Stun", StunTime);
			Instantiate(CollisionParticule, coll.gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}