using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float Speed = 10f;
	public float JumpHeight = 10f;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

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
		if(Grounded)
		{
			//Jump
			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = JumpHeight;
			}
			else
				amountToMove.y = 0;

			//Fire Gun
			if(Input.GetButtonDown("Fire1"))
			{
				//Shoot gun
			}
		}
		//Input
		targetSpeed = Input.GetAxis ("Horizontal") * Speed;

		//Set Amount to move
		amountToMove.x = targetSpeed; 
		rigidbody2D.AddForce(amountToMove);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Platform")) 
		{
			Grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Platform")) 
		{
			Grounded = false;
		}
	}
}
