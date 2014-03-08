using UnityEngine;
using System.Collections;

public class BottomCollider : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) // Always 2 L
	{
		if (coll.gameObject.tag == "Player")
		{
			transform.parent.collider2D.isTrigger = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll) // 2 L 2 MOTHERFUCKING L
	{
		if (coll.gameObject.tag == "Player")
		{
				transform.parent.collider2D.isTrigger = false;
		}
	}
}
