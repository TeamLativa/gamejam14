using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GridSystem : MonoBehaviour {

	public float snap = 1f;
	public float depth = 0;
	
	// Update is called once per frame
	void Update () {
		float inverseSnap = 1 / snap;
		float x, y, z;

		x = Mathf.Round (transform.localPosition.x * inverseSnap) / inverseSnap;
		y = Mathf.Round (transform.localPosition.y * inverseSnap) / inverseSnap;
		z = depth;

		transform.localPosition = new Vector3 (x, y, z);
	}
}
