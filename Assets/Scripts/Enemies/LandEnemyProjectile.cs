using UnityEngine;
using System.Collections;

public class LandEnemyProjectile : MonoBehaviour {
	
	public float StunTime = 1.0f;
	
	public GameObject CollisionParticule;
	
	public float TimeToLive = 3.0f;
	private float lifeTimer = 0.0f;
	public float MoveSpeed = 4.0f;
	
	void FixedUpdate(){
		transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Player") {
			coll.gameObject.SendMessage("Stun", StunTime);
			Instantiate(CollisionParticule, coll.gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
