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

	public GameObject TotemPart1;
	public GameObject TotemPart2;
	public GameObject TotemPart3;
	public GameObject TotemPart4;
	public GameObject TotemPart5;
	public GameObject TotemPart6;

	private int partNumber = 6;

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

					switch (partNumber)
					{
					case 0 : break; 
					case 1 : 
						Instantiate(TotemPart1, TotemPart1.transform.position, Quaternion.identity);
						partNumber = 0;
						Instantiate(TotemPart1, new Vector2(-TotemPart1.transform.position.x,TotemPart1.transform.position.y), Quaternion.identity);
						Instantiate(TotemPart2, new Vector2(-TotemPart2.transform.position.x,TotemPart2.transform.position.y), Quaternion.identity);
						Instantiate(TotemPart3, new Vector2(-TotemPart3.transform.position.x,TotemPart3.transform.position.y), Quaternion.identity);
						Instantiate(TotemPart4, new Vector2(-TotemPart4.transform.position.x,TotemPart4.transform.position.y), Quaternion.identity);
						Instantiate(TotemPart5, new Vector2(-TotemPart5.transform.position.x,TotemPart5.transform.position.y), Quaternion.identity);
						Instantiate(TotemPart6, new Vector2(-TotemPart6.transform.position.x,TotemPart6.transform.position.y), Quaternion.identity);

						//load gameOver when its gonna be made
						Application.LoadLevel ("level");

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



	void Stun(float stunTime){
		stunned = true;
		stunnedTimer = stunTime;
	}
}
