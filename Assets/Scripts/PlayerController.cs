using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float Speed = 1f;
	public int JumpHeight = 100;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private bool isOnTotemSpot;

	[HideInInspector]
	public bool Grounded = false;

	// Use this for initialization
	void Start () {

	}

	void Update()
	{

	}

	// Update is called once per frame
	void FixedUpdate () {
		//drop component for totem
		if(Input.GetButtonDown("Fire1"))
		{
			if (isOnTotemSpot)
			{
				//add things to totem



			}
		}

		if(Grounded)
		{
			//Jump
			if(Input.GetButtonDown("Jump"))
			{

				rigidbody2D.AddForce(new Vector3(0, JumpHeight, 0));
				Grounded = false;
				isOnTotemSpot = false;
			}
		}
		//Input
		targetSpeed = Input.GetAxis ("Horizontal") * Speed;

		//Set Amount to move
		amountToMove.x = targetSpeed; 
		//rigidbody2D.AddForce(amountToMove);
		transform.position += new Vector3(targetSpeed, 0, 0) * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "PlatformTop")) 
		{
			Grounded = true;
		}

		if (collision.gameObject.name == "TopColliderTotem") 
		{
			isOnTotemSpot = true;
			Grounded = true;
		}
	}
}
