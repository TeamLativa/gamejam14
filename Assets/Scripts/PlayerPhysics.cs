using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;
	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;
	private float skin = 0.005f;

	[HideInInspector]
	public bool grounded = false;
	[HideInInspector]
	public bool movementStopped = false;

	Ray ray;
	RaycastHit hit;



	// Use this for initialization
	void Start () {

		collider = GetComponent<BoxCollider> ();
		s = collider.size;
		c = collider.center;

	}
	
	// Update is called once per frame
	void Update () {
	}

	void Dead()
	{

		//Dostuff for when you die.. still not sure

	}

	public void Move(Vector2 moveAmount)
	{
		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		Vector2 p = transform.position;

		grounded = false;

		//Check Collision up and down first
		for (int i = 0; i < 3; i++) 
		{
			float dir = Mathf.Sign (deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float y = p.y + c.y + s.y/2 * dir; // Bottom of collider

			ray = new Ray(new Vector2(x,y), new Vector2(0,dir));

			if ( Physics.Raycast(ray, out hit, Mathf.Abs (deltaY) + skin, collisionMask))
			{
				//Get distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);

				//Stop player's downwards movement
				if(dst>skin)
					deltaY = dst * dir - skin * dir;
				else
					deltaY = 0;

				grounded = true;

				break;
			}

		}

		movementStopped = false;

		//Check Collision right and left
		for (int i = 0; i < 3; i++) 
		{
			float dir = Mathf.Sign (deltaX);
			float x = p.x + c.x + s.x/2 * dir;
			float y = p.y + c.y - s.y/2 + s.y/2 * i;
			
			ray = new Ray(new Vector2(x,y), new Vector2(dir, 0));
			
			if ( Physics.Raycast(ray, out hit, Mathf.Abs (deltaX) + skin, collisionMask))
			{
				//Get distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				//Stop player's downwards movement
				if(dst>skin)
					deltaX = dst * dir - skin * dir;
				else
					deltaX = 0;
				
				movementStopped = true;
				
				break;
			}
			
		}

		movementStopped = false;

		Vector2 finalTransformation = new Vector2 (deltaX, deltaY);

		transform.Translate (finalTransformation);

	}
}
