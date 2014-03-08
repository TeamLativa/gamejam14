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
			if(Input.GetButtonDown("Abutton_"+PNumber))
			{
				Grounded = false;
				rigidbody2D.AddForce(new Vector3(0, JumpHeight, 0));

			}
		}
		//Input
		targetSpeed = Input.GetAxis ("LeftAnalogX_"+PNumber) * Speed;

		//Set Amount to move
		amountToMove.x = targetSpeed; 
		//rigidbody2D.AddForce(amountToMove);
		transform.position += new Vector3(targetSpeed, 0, 0) * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Platform")) 
		{
			Grounded = true;
		}
	}
}
