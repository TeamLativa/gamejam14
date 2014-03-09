﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float Speed = 1f;
	public float JumpHeight = 100.0f;

	public string PNumber;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

	public GameObject TotemPart1;
	public GameObject TotemPart2;
	public GameObject TotemPart3;
	public GameObject TotemPart4;
	public GameObject TotemPart5;
	public GameObject TotemPart6;

	private int partNumber = 6;

	private bool stunned = false;
	private float stunnedTimer = 0.0f;

	private bool jump = false;	
	private bool grounded = false;			// Whether or not the player is grounded.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.

	private float speedTimer = 0.0f;
	private float baseSpeed;

	private float jumpTimer = 0.0f;
	private float baseJumpHeight;

	public int LastLayer;

	private Animator anim;
	private bool facingRight = true;

	// Use this for initialization
	void Start () {

		groundCheck = transform.Find("GroundCheck");
		baseSpeed = Speed;
		baseJumpHeight = JumpHeight;

		anim = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		Physics2D.IgnoreLayerCollision(gameObject.layer, 9, rigidbody2D.velocity.y > 0);

		if (Input.GetAxis("LeftTrigger_"+ PNumber) > 0.5)
		{
			gameObject.GetComponent<PlayerInventoryPowerUp>().ConsumePowerUps();
		}
		if(stunned)
			stunnedTimer -= Time.deltaTime;

		if(stunnedTimer <= 0.0f){
			anim.SetTrigger("NotStunned");
			stunned = false;
		}

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Platforms")); 
		if((Input.GetButtonDown("Abutton_"+PNumber) || Input.GetButtonDown("LeftBumper_"+PNumber)) && grounded && !stunned)
			jump = true;


		VerifySpeedTimer();
		VerifyJumpTimer();
	}

	// Update is called once per frame
	void FixedUpdate () {


		//drop component for totem
		if(!stunned){
			HandleMovement();
		}

		// If the player should jump...
		if(jump)
		{
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, JumpHeight));

			anim.SetTrigger("Jump");
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}

	}

	void HandleMovement(){
		//Input

		float axisH = Input.GetAxisRaw ("LeftAnalogX_"+PNumber);

		anim.SetFloat("Speed", Mathf.Abs(axisH));

		if (Mathf.Abs(axisH)>0.05)
		{
			targetSpeed = axisH * Speed;

			//Set Amount to move
			amountToMove.x = targetSpeed; 
			//rigidbody2D.AddForce(amountToMove);
			transform.position += new Vector3(targetSpeed, 0, 0) * Time.deltaTime;
		}

		if(axisH > 0 && !facingRight){
			Flip();
		}
		else if(axisH < 0 && facingRight){
			Flip();
		}
	}

	/*void OnCollisionEnter2D(Collision2D collision)
	{
		if(!stunned) 
		{
			if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "PlatformTop")
			    || (collision.gameObject.tag == "PlatformTotem")) 
			{
				if(collision.gameObject.tag == "PlatformTotem")
				{
					if(Input.GetButtonDown("Xbutton_"+PNumber))
					{
						//add things to totem
						//if(player got all items to add a part)

						switch (partNumber)
						{
						case 0 : break; 
						case 1 : 
							Instantiate(TotemPart1, TotemPart1.transform.position, Quaternion.identity);
							partNumber = 0;

							GameObject part;
							part = (GameObject)Instantiate(TotemPart6, new Vector2(-TotemPart6.transform.position.x,TotemPart6.transform.position.y), Quaternion.identity);
							part.transform.localScale = Flip (part.transform.localScale);
							part = (GameObject)Instantiate(TotemPart5, new Vector2(-TotemPart5.transform.position.x,TotemPart5.transform.position.y), Quaternion.identity);
							part.transform.localScale = Flip (part.transform.localScale);					
							part = (GameObject)Instantiate(TotemPart4, new Vector2(-TotemPart4.transform.position.x,TotemPart4.transform.position.y), Quaternion.identity);
							part.transform.localScale = Flip (part.transform.localScale);
							part = (GameObject)Instantiate(TotemPart3, new Vector2(-TotemPart3.transform.position.x,TotemPart3.transform.position.y), Quaternion.identity);
							part.transform.localScale = Flip (part.transform.localScale);
							part = (GameObject)Instantiate(TotemPart2, new Vector2(-TotemPart2.transform.position.x,TotemPart2.transform.position.y), Quaternion.identity);
							part.transform.localScale = Flip (part.transform.localScale);
							part = (GameObject)Instantiate(TotemPart1, new Vector2(-TotemPart1.transform.position.x,TotemPart1.transform.position.y), Quaternion.identity);
							part.transform.localScale = Flip (part.transform.localScale);

							//load gameOver when its gonna be made
							//Application.LoadLevel ("level");

							break;
						case 2 : 
							Instantiate(TotemPart2, TotemPart2.transform.position, Quaternion.identity);
							partNumber = 1;
							break;
						case 3 : 
							Instantiate(TotemPart3, TotemPart3.transform.position, Quaternion.identity);
							partNumber = 2;
							break;
						case 4 : 
							Instantiate(TotemPart4, TotemPart4.transform.position, Quaternion.identity);
							partNumber = 3;
							break;
						case 5 : 
							Instantiate(TotemPart5, TotemPart5.transform.position, Quaternion.identity);
							partNumber = 4;
							break;
						case 6 : 
							Instantiate(TotemPart6, TotemPart6.transform.position, Quaternion.identity);
							partNumber = 5;
							break;
						}					
					}
				}

				if(Input.GetButtonDown("Abutton_"+PNumber))
				{
					rigidbody2D.AddForce(Vector3.up * JumpHeight);
					
				}
			}
		}
	}*/

	void Flip(){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public int facingSideInt(){
		if(facingRight)
			return 1;
		else
			return -1;
	}
	
	public bool IsStunned(){
		return stunned;
	}

	void Stun(float stunTime){
		if(!stunned) {
			anim.SetTrigger("Stun");
			stunned = true;
			stunnedTimer = stunTime;
		}
	}

	void VerifySpeedTimer()
	{
		if(speedTimer > 0.0f){
			speedTimer -= Time.deltaTime;
			if(speedTimer <= 0)
			{
				Speed = baseSpeed;
                speedTimer = 0.0f;
            }
            
        }
	}

	public void ApplyBonusSpeed(float bonus, float time)
	{
		Speed *= bonus;
		SetSpeedTimer(time);
	}

	void SetSpeedTimer(float time)
	{
		speedTimer = time;
    }

	void VerifyJumpTimer()
	{
		if(jumpTimer > 0.0f){
			jumpTimer -= Time.deltaTime;
			if(jumpTimer <= 0)
			{
				JumpHeight = baseJumpHeight;
				jumpTimer = 0.0f;
			}
			
		}
	}
	
	public void ApplyBonusJump(float bonus, float time)
	{
		JumpHeight *= bonus;
		SetJumpTimer(time);
    }
    
    void SetJumpTimer(float time)
    {
        jumpTimer = time;
    }
}
