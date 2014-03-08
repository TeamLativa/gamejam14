using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float Speed = 1f;
	public int JumpHeight = 100;

	public string PNumber;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
<<<<<<< HEAD
	private bool isOnTotemSpot;

	[HideInInspector]
	public bool Grounded = false;
=======
	
	private bool stunned = false;
	private float stunnedTimer = 0.0f;
>>>>>>> 659948dcd13deb6bbb848f67d334191b2cd273c3

	// Use this for initialization
	void Start () {

	}

	void Update()
	{
		if(stunned)
			stunnedTimer -= Time.deltaTime;

		if(stunnedTimer <= 0.0f){
			stunned = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
<<<<<<< HEAD
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
=======
		if(!stunned){
			HandleMovement();
>>>>>>> 659948dcd13deb6bbb848f67d334191b2cd273c3
		}

	}

	void HandleMovement(){
		//Input

		float axisH = Input.GetAxisRaw ("LeftAnalogX_"+PNumber);
		if (Mathf.Abs(axisH)>0.05)
		{
			targetSpeed = axisH * Speed;

			//Set Amount to move
			amountToMove.x = targetSpeed; 
			//rigidbody2D.AddForce(amountToMove);
			transform.position += new Vector3(targetSpeed, 0, 0) * Time.deltaTime;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "PlatformTop")) 
<<<<<<< HEAD
		{
			Grounded = true;
		}

		if (collision.gameObject.name == "TopColliderTotem") 
		{
			isOnTotemSpot = true;
			Grounded = true;
=======
		{
			//
			if(Input.GetButtonDown("Abutton_"+PNumber))
			{
				
				rigidbody2D.AddForce(Vector3.up * JumpHeight);
				
			}
>>>>>>> 659948dcd13deb6bbb848f67d334191b2cd273c3
		}
	}



	void Stun(float stunTime){
		stunned = true;
		stunnedTimer = stunTime;
	}
}
