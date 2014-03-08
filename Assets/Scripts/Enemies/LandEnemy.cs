using UnityEngine;
using System.Collections;

public class LandEnemy : MonoBehaviour {
	
	// Life
	public int HealthPoints = 100;
	public GameObject DeathParticule;
	
	// Movement
	public float MovementSpeed = 3.0f;
	public float Force = 15.0f;
	public Transform SpawnPoints;
	private Vector2 direction;
	private GameObject lastCollidedWall;
	
	// Firing
	public float FireRate = 3.0f;
	private float fireTimer = 0.0f;
	public GameObject Projectile;
	
	// Item Drop
	public GameObject[] Items = new GameObject[2];
	public GameObject[] PowerUps = new GameObject[3];
	public float ItemDropRate = 10.0f;
	public float PowerUpDropRate = 80.0f;
	
	// Use this for initialization
	void Start () {
		direction = transform.forward + new Vector3(1.0f, 0.0f);
		transform.position = SpawnPoints.GetChild(Random.Range(0, SpawnPoints.childCount)).position;
	}
	
	// Update is called once per frame
	void Update () {
		HandleDeath();
		HandleFire();
	}

	void FixedUpdate(){
		HandleMovement();
	}
	
	void HandleMovement(){
		rigidbody2D.velocity = new Vector2(MovementSpeed * direction.x, rigidbody2D.velocity.y);
		Debug.Log(direction);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Wall" || coll.gameObject.tag == "PlatformSide"){
			if(coll.gameObject != lastCollidedWall){
				lastCollidedWall = coll.gameObject;
				Debug.Log ("Collided, changing direction");
				direction = -direction;
				Flip();
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
			// Fire towards player
			fireTimer = 0.0f;
		}
	}
	
	void TakeDamage(int amount){
		HealthPoints -= amount;
	}
	
	void HandleDeath(){
		if(HealthPoints <= 0){
			// Maybe notify someone
			DropItem();
			Instantiate(DeathParticule, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
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
