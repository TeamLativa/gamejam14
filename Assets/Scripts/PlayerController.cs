using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	public float Gravity = 10;
	public float Speed = 1;
	public float Acceleration = 10;
	public float JumpHeight = 12;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	
	private PlayerPhysics playerPhysics;
	
	
	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerPhysics.movementStopped)
		{
			targetSpeed = 0;
			currentSpeed = 0;
		}

		if(playerPhysics.grounded)
		{
			amountToMove.y = 0;

			//Jump
			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = JumpHeight;
			}
			//Fire Gun
			if(Input.GetButtonDown("Fire1"))
			{
				//Shoot gun
			}
		}

		//Input
		targetSpeed = Input.GetAxis ("Horizontal") * Speed;
		currentSpeed = incrementTowards (currentSpeed, targetSpeed, Acceleration);

		//Set Amount to move
		amountToMove.x = currentSpeed; 
		amountToMove.y -= Gravity * Time.deltaTime;
		playerPhysics.Move (amountToMove * Time.deltaTime);



	}

	private float incrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}

}
