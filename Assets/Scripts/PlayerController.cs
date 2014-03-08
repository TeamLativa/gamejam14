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

	public bool Grounded = false;
	private bool stunned = false;
	private float stunnedTimer = 0.0f;

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
		if(!stunned){
			HandleMovement();
		}
	}

	void HandleMovement(){
		if(Grounded)
		{
			//Jump
			if(Input.GetButtonDown("Abutton_"+PNumber))
			{
				Grounded = false;
				rigidbody2D.AddForce(new Vector3(0, JumpHeight, 0));
				
			}
		}
		//Input

		float axisH = Input.GetAxisRaw ("LeftAnalogX_"+PNumber);
		if (Mathf.Abs(axisH)>0.01)
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
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Platform")) 
		{
			Grounded = true;
		}
	}

	void Stun(float stunTime){
		stunned = true;
		stunnedTimer = stunTime;
	}
}
