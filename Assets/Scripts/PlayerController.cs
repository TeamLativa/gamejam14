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
		//drop component for totem
		if(!stunned){
			HandleMovement();
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
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "PlatformTop")
		    || (collision.gameObject.tag == "PlatformTotem")) 
		{
			//
			if(Input.GetButtonDown("Xbutton_"+PNumber))
			{
				if (collision.gameObject.tag == "PlatformTotem")
				{
					//add things to totem
					//if(player got all items to add a part)
					//instantiate(blueTotem, blueTotem.transform.position, Quaternion.identity);
					Debug.Log ("IM adding parts to the totem!");
				}
			}

			if(Input.GetButtonDown("Abutton_"+PNumber))
			{
				rigidbody2D.AddForce(Vector3.up * JumpHeight);
				
			}
		}
	}



	void Stun(float stunTime){
		stunned = true;
		stunnedTimer = stunTime;
	}
}
