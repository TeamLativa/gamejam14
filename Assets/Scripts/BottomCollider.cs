using UnityEngine;
using System.Collections;

public class BottomCollider : MonoBehaviour {

	private int lastLayer;

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
			Debug.Log ("BOTTOM IN: " + coll.gameObject.layer);
			if (coll.gameObject.layer == 21)
			{
				return;
			}
			lastLayer = coll.gameObject.layer;
			coll.gameObject.layer =  21;
		}
	}

	void OnTriggerExit2D(Collider2D coll) // 2 L 2 MOTHERFUCKING L
	{
		if (coll.gameObject.tag == "Player")
		{
			Debug.Log("BOTTOM OUT :" + lastLayer);
			coll.gameObject.layer = lastLayer;
		}
	}
}
