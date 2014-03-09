using UnityEngine;
using System.Collections;

public class BouclierSimple : MonoBehaviour {

	public GameObject CollisionParticule;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) // DEUX L
	{
		if (coll.gameObject.tag == "Projectile" || coll.gameObject.tag == "EnemyProjectile")
		{
			Instantiate(CollisionParticule, coll.gameObject.transform.position, Quaternion.identity);
			Destroy(coll.gameObject);
		}
	}
}