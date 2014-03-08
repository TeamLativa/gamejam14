using UnityEngine;
using System.Collections;

public class SideCollider : MonoBehaviour {
	
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
			//Debug.Log (Time.time + " >> SIDE IN: " + coll.gameObject.layer);
			if (coll.gameObject.layer == 21)
			{
				return;
			}
			coll.gameObject.GetComponent<PlayerController>().LastLayer = coll.gameObject.layer;
			coll.gameObject.layer =  21;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll) // 2 L 2 MOTHERFUCKING L
	{
		if (coll.gameObject.tag == "Player")
		{
			//Debug.Log(Time.time + " >> SIDE OUT :" + coll.gameObject.GetComponent<PlayerController>().LastLayer);
			coll.gameObject.layer = coll.gameObject.GetComponent<PlayerController>().LastLayer;
		}
	}
}
