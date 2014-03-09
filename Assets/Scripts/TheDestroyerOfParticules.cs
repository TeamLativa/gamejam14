using UnityEngine;
using System.Collections;

public class TheDestroyerOfParticules : MonoBehaviour {

	public float TimeToLive = 3.0f;
	private float lifeTimer = 0.0f;
	
	void Update(){
		lifeTimer += Time.deltaTime;
		if(lifeTimer >= TimeToLive)
			Destroy(gameObject);
	}
}
