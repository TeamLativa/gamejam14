using UnityEngine;
using System.Collections;


public class NonPhysicsPlayerController : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

	public string PNumber;

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

	private float speedTimer = 0.0f;
	private float baseSpeed;
	
	private float jumpTimer = 0.0f;
	private float baseJumpHeight;

	private bool facingRight = true;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;

		baseSpeed = runSpeed;
		baseJumpHeight = jumpHeight;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		if( hit.normal.y == 1f )
			return;


	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion



	void Update()
	{
		if (Input.GetAxis("LeftTrigger_"+ PNumber) > 0.5)
		{
			gameObject.GetComponent<PlayerInventoryPowerUp>().ConsumePowerUps();
		}

		if(stunned)
			stunnedTimer -= Time.deltaTime;
		
		if(stunnedTimer <= 0.0f){
			_animator.SetTrigger("NotStunned");
			stunned = false;
		}

		if(!stunned)
			HandleMovement();

		VerifySpeedTimer();
		VerifyJumpTimer();
	}

	void VerifyJumpTimer()
	{
		if(jumpTimer > 0.0f){
			jumpTimer -= Time.deltaTime;
			if(jumpTimer <= 0)
			{
				jumpHeight = baseJumpHeight;
				jumpTimer = 0.0f;
			}
			
		}
	}

	void HandleMovement()
	{
	
		if(stunned)
			stunnedTimer -= Time.deltaTime;
		
		if(stunnedTimer <= 0.0f){
			_animator.SetTrigger("NotStunned");
			stunned = false;
		}
		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;
		
		if( Input.GetAxisRaw ("LeftAnalogX_"+PNumber)  > 0.5 )
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			facingRight = true;
		}
		else if( Input.GetAxisRaw ("LeftAnalogX_"+PNumber) < -0.05 )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			facingRight = false;
		}
		else
		{
			normalizedHorizontalSpeed = 0;

		}
		
		
		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetButtonDown("Abutton_"+PNumber) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			_animator.SetTrigger("Jump");
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_animator.SetFloat("Speed", Mathf.Abs (_velocity.x));

		_controller.move( _velocity * Time.deltaTime );

	}

	public int facingSideInt(){
		return (facingRight) ? 1 : -1;
	}

	public bool IsStunned(){
		return stunned;
	}
	
	void Stun(float stunTime){
		if(!stunned) {
			_animator.SetTrigger("Stun");
			stunned = true;
			stunnedTimer = stunTime;
		}
	}
	
	public void ApplyBonusJump(float bonus, float time)
	{
		jumpHeight *= bonus;
		SetJumpTimer(time);
	}
	
	void SetJumpTimer(float time)
	{
		jumpTimer = time;
	}

	void VerifySpeedTimer()
	{
		if(speedTimer > 0.0f){
			speedTimer -= Time.deltaTime;
			if(speedTimer <= 0)
			{
				runSpeed = baseSpeed;
				speedTimer = 0.0f;
			}
			
		}
	}
	
	public void ApplyBonusSpeed(float bonus, float time)
	{
		runSpeed *= bonus;
		SetSpeedTimer(time);
	}
	
	void SetSpeedTimer(float time)
	{
		speedTimer = time;
	}

}
