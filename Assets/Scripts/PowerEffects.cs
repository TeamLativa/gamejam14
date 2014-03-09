using UnityEngine;
using System.Collections;

public class PowerEffects : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void destroyEffect(float time)
	{
		Destroy(gameObject, time);
	}
}
