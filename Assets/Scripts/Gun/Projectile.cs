using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float BulletSpeed = 20.0f;

	public float MaxDistance = 18.0f;

	public int Damage = 1;

	Vector3 initialPosition;


	// Use this for initialization
	void Start () {
	
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += Time.deltaTime * BulletSpeed * transform.right;
	 
		if ((transform.position - initialPosition).magnitude >= MaxDistance)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) // DEUX L
	{
		if (coll.gameObject.tag == "Enemy")
		{
			Debug.Log("Do Ouch");
			coll.gameObject.SendMessage("TakeDamage", Damage);
			Destroy(gameObject);
		}
	}

}
