using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	private Transform target;
	private Transform target2;
	private float trackSpeed = 2f;
	private float minSize = 6f;
	private float minY = 0f;
	private float maxSize = 12f;
	private float maxY = 6f;

	// Set target
	public void SetTarget(Transform t, Transform t2) {
		target = t;
		target2 = t2;
	}
	
	// Track target
	void LateUpdate() {
		if ((target)&&(target2)) {
			float y;

			Vector2 dist = target2.transform.position-target.transform.position;
			dist.x = Mathf.Abs (dist.x);
			dist.y = Mathf.Abs (dist.y);

			Debug.Log (dist.y);

			if (dist.y>minSize)
			{
				if (dist.y < maxSize)
				{
					camera.orthographicSize = IncrementTowards(camera.orthographicSize, dist.y, trackSpeed);
					y = IncrementTowards(transform.position.y, camera.orthographicSize-maxY, trackSpeed);

				}
				else
				{
					y = IncrementTowards(transform.position.y, maxY, trackSpeed);
					camera.orthographicSize = IncrementTowards(camera.orthographicSize, maxSize, trackSpeed);

				}
			}
			else
			{
				camera.orthographicSize = IncrementTowards(camera.orthographicSize, minSize, trackSpeed);
				y = IncrementTowards(transform.position.y, minY, trackSpeed);
			} 

			transform.position = new Vector3(0,y, transform.position.z);

		}
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}