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

	private int nbRoche, nbBois,nbOs,nbMetal,nbPlume,nbLiane;
	private bool putRoche, putBois, putOs, putMetal, putPlume, putLiane;
	private int nbParts = 6;

	private int []partPosition = new int[6];

	private int partNumber = 6;

	private bool stunned = false;
	private float stunnedTimer = 0.0f;

	private bool jump = false;	
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool groundedOnTotemSpot = false;	

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

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground")) || Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Platforms"));
		if((Input.GetButtonDown("Abutton_"+PNumber) || Input.GetButtonDown("LeftBumper_"+PNumber)) && grounded && !stunned)
			jump = true;


	VerifySpeedTimer();
	VerifyJumpTimer();
}
	void OnTriggerEnter( Collider col )
	{
		Debug.Log( "onTriggerEnterEvent: ");
		/*if (col.gameObject.tag == "Totem")
			checkDroppingItemOnTotem ();
*/
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
	}
	void checkDroppingItemOnTotem()
	{
		if(Input.GetButtonDown("Xbutton_"+PNumber))
		{			
			Debug.Log ("HLKHJL");
			//if(player got all items to add a part)
			if((nbRoche==0)||(putRoche))
			{
				if((nbBois==0)||(putBois))
				{
					if((nbOs==0)||(putOs))
					{
						if((nbMetal==0)||(putMetal))
						{
							if((nbPlume==0)||(putPlume))
							{
								if(nbLiane!=0)
								{	
									if(!putLiane)
									{
										Instantiate(TotemPart1, SetPosition(false), Quaternion.identity);
										putLiane = true;
										removeOne("liane");
										nbParts--;
									}
								}
							}
							else
							{	
								if(!putPlume)
								{	
									Instantiate(TotemPart2, SetPosition(true), Quaternion.identity);
									putPlume = true;
									removeOne("plume");
									nbParts--;
								}
							}
						}
						else
						{	
							if(!putMetal)
							{
								Instantiate(TotemPart3, SetPosition(false), Quaternion.identity);
								putMetal = true;
								removeOne("metal");
								nbParts--;
							}
						}
					}
					else
					{	
						if(!putOs)
						{
							Instantiate(TotemPart4, SetPosition(false), Quaternion.identity);
							putOs = true;
							removeOne("os");
							nbParts--;
						}
					}
				}
				else
				{	
					if(!putBois)
					{
						Instantiate(TotemPart5, SetPosition(false), Quaternion.identity);
						putBois = true;
						removeOne("bois");
						nbParts--;
					}
				}
			}
			else
			{	
				if(!putRoche)
				{
					Instantiate(TotemPart6, SetPosition(false), Quaternion.identity);
					putRoche = true;
					removeOne("roche");
					nbParts--;
				}
			}
			
			if((putRoche)&&(putBois)&&(putOs)&&(putMetal)&&(putPlume)&&(putLiane))
			{
				
				GameObject part;
				part = (GameObject)Instantiate(TotemPart6, GetPosition ("Roche"), Quaternion.identity);
				part.gameObject.transform.localScale = FlipTotem(part.gameObject.transform.localScale);
				part = (GameObject)Instantiate(TotemPart5, GetPosition ("Bois"), Quaternion.identity);
				part.gameObject.transform.localScale = FlipTotem(part.gameObject.transform.localScale);
				part = (GameObject)Instantiate(TotemPart4, GetPosition ("Os"), Quaternion.identity);
				part.gameObject.transform.localScale = FlipTotem(part.gameObject.transform.localScale);
				part = (GameObject)Instantiate(TotemPart3, GetPosition ("Metal"), Quaternion.identity);
				part.gameObject.transform.localScale = FlipTotem(part.gameObject.transform.localScale);
				part = (GameObject)Instantiate(TotemPart2, GetPosition ("Plume"), Quaternion.identity);
				part.gameObject.transform.localScale = FlipTotem(part.gameObject.transform.localScale);
				part = (GameObject)Instantiate(TotemPart1, GetPosition ("Liane"), Quaternion.identity);
				part.gameObject.transform.localScale = FlipTotem(part.gameObject.transform.localScale);
			}
		}
	}
	
	Vector3 FlipTotem(Vector3 currentScale){
		
		Vector3 scale = currentScale;
		scale.x *= -1;
		return scale;
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

	public void GetItems(int roche, int bois, int os, int metal, int plume, int liane)
	{
		nbRoche = roche;
		nbBois = bois;
		nbOs = os;
		nbMetal = metal;
		nbPlume = plume;
		nbLiane = liane;
	}
	void removeOne(string objet)
	{
		switch(objet)
		{
			case "roche": nbRoche--; gameObject.GetComponent<PlayerInventoryMaterials>().setRoche(nbRoche);break;
			case "bois":nbBois--; gameObject.GetComponent<PlayerInventoryMaterials>().setRoche(nbBois);break;
			case "os":nbMetal--; gameObject.GetComponent<PlayerInventoryMaterials>().setOs(nbOs);break;
			case "metal":nbMetal--; gameObject.GetComponent<PlayerInventoryMaterials>().setRoche(nbMetal);break;
			case "plume":nbPlume--; gameObject.GetComponent<PlayerInventoryMaterials>().setRoche(nbPlume);break;
			case "liane":nbLiane--; gameObject.GetComponent<PlayerInventoryMaterials>().setRoche(nbLiane);break;
		}
	}

	Vector2 SetPosition(bool isItTheBird)
	{
		switch (nbParts) 
		{
			case 1 : 
				if(isItTheBird)
					return new Vector2(TotemPart2.transform.position.x,TotemPart1.transform.position.y); 
				else 
					return TotemPart1.transform.position; break;
			case 2 : 
				if(isItTheBird)
					return TotemPart2.transform.position;
				else 
					return new Vector2(TotemPart1.transform.position.x,TotemPart2.transform.position.y); break;
			case 3 : 
				if(isItTheBird)
					return new Vector2(TotemPart2.transform.position.x,TotemPart3.transform.position.y); 
				else 
					return TotemPart3.transform.position; break;
			case 4 : 
				if(isItTheBird)
					return new Vector2(TotemPart2.transform.position.x,TotemPart4.transform.position.y); 
				else 
					return TotemPart4.transform.position; break;
			case 5 :  
				if(isItTheBird)
					return new Vector2(TotemPart2.transform.position.x,TotemPart5.transform.position.y); 
				else 
					return TotemPart5.transform.position; break;
			case 6 :  
				if(isItTheBird)
					return new Vector2(TotemPart2.transform.position.x,TotemPart6.transform.position.y); 
				else 
					return TotemPart6.transform.position; break;
			default: return new Vector2(5,5); break;
		}
	}

	Vector2 GetPosition(string tag)
	{
		switch(tag)
		{
			case "Roche": return new Vector2(-GameObject.FindWithTag("Roche").transform.position.x,GameObject.FindWithTag("Roche").transform.position.y); break;
			case "Bois": return new Vector2(-GameObject.FindWithTag("Bois").transform.position.x,GameObject.FindWithTag("Bois").transform.position.y); break;
			case "Os": return new Vector2(-GameObject.FindWithTag("Os").transform.position.x,GameObject.FindWithTag("Os").transform.position.y);break;
			case "Metal": return new Vector2(-GameObject.FindWithTag("Metal").transform.position.x,GameObject.FindWithTag("Metal").transform.position.y);break;
			case "Plume": return new Vector2(-GameObject.FindWithTag("Plume").transform.position.x,GameObject.FindWithTag("Plume").transform.position.y);break;
			case "Liane": return new Vector2(-GameObject.FindWithTag("Liane").transform.position.x,GameObject.FindWithTag("Liane").transform.position.y);break;
		default : return new Vector2(5,5);break;
		}
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
