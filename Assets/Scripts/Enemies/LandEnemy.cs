using UnityEngine;
using System.Collections;

public class LandEnemy : MonoBehaviour {
	
	// Life
	public int HealthPoints = 100;
	public GameObject DeathParticule;
	
	// Movement
	public float MovementSpeed = 3.0f;
	public float Force = 15.0f;
	private Transform SpawnPoints;
	private Vector2 direction;
	private GameObject lastCollidedWall;
	public float PlayerStunTime = 1.0f;
	public float ReverseTimerMin = 3.0f;
	public float ReverseTimerMax = 8.0f;
	private float reverseTimer = 5.0f;
	
	// Firing
	public float FireRate = 3.0f;
	private float fireTimer = 0.0f;
	public GameObject Projectile;
	public float FireDistance = 7.0f;
	
	// Item Drop
	public GameObject[] Items = new GameObject[2];
	public GameObject[] PowerUps = new GameObject[3];
	public float ItemDropRate = 10.0f;
	public float PowerUpDropRate = 80.0f;

	public AudioClip DeathSound;
	public float DeathVolume = 0.5f;
	
	// Use this for initialization
	void Start () {
		SpawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints").transform;
		direction = transform.forward + new Vector3(1.0f, 0.0f);
		transform.position = SpawnPoints.GetChild(Random.Range(0, SpawnPoints.childCount)).position;
		SetRandomTimer();
	}

	void SetRandomTimer(){
		reverseTimer = Random.Range(ReverseTimerMin, ReverseTimerMax);
	}
	
	// Update is called once per frame
	void Update () {
		HandleDeath();
		HandleFire();
	}

	void FixedUpdate(){
		HandleMovement();
		HandleReverseTimer();
	}
	
	void HandleMovement(){
		rigidbody2D.velocity = new Vector2(MovementSpeed * direction.x, rigidbody2D.velocity.y);
		ClampAngle();
	}

	void ClampAngle(){

	}

	void HandleReverseTimer(){
		reverseTimer -= Time.deltaTime;
		if(reverseTimer <= 0.0f){
			direction = -direction;
			Flip();
			SetRandomTimer();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){

		if(coll.gameObject.tag == "Wall" || coll.gameObject.tag == "PlatformSide" || coll.gameObject.tag == "Player"){
			if(coll.gameObject != lastCollidedWall){
				lastCollidedWall = coll.gameObject;
				direction = -direction;
				Flip();
			}
			if(coll.gameObject.tag == "Player"){
				coll.gameObject.SendMessage("Stun", PlayerStunTime);
			}
		}
	}


	void Flip(){
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	
	void HandleFire(){
		fireTimer += Time.deltaTime;
		
		if(fireTimer >= FireRate){ 

			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			float distanceToP1 = (players[0].gameObject.transform.position - transform.position).magnitude;
			float distanceToP2 = (players[1].gameObject.transform.position - transform.position).magnitude;

			Transform target = (distanceToP1 <= distanceToP2) ? players[0].gameObject.transform : players[1].gameObject.transform;

			if((target.position - transform.position).magnitude <= FireDistance){

				GameObject proj = (GameObject) Instantiate(Projectile, transform.position, Quaternion.identity);
				Vector3 relative = target.localPosition - transform.localPosition;
				float targetAngle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90;
				proj.transform.rotation = Quaternion.Euler(0, 0, targetAngle);

				fireTimer = 0.0f;
			}
		}
	}
	
	void TakeDamage(int amount){
		HealthPoints -= amount;
	}

	void HandleDeath(){
		if(HealthPoints <= 0){

			AudioSource.PlayClipAtPoint(DeathSound, transform.position, DeathVolume);

			// Maybe notify someone
			DropItem();
			Explode();
			Instantiate(DeathParticule, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	void Explode(){

	}
	
	void DropItem(){
		float chance = Random.Range(0,100.0f);
		
		if(chance < ItemDropRate){
			//Generate random number to decide the item to drop
			//Drop the item
			Instantiate(Items[Random.Range(0,Items.GetLength(0))], transform.position, Quaternion.identity);
		}
		else if(chance < PowerUpDropRate){
			//Generate random number to decide the power up to drop
			//Drop the power up
			Instantiate(PowerUps[Random.Range(0,PowerUps.GetLength(0))], transform.position, Quaternion.identity);
		}
	}
}
