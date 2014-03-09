using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour {

	// Life
	public int HealthPoints = 100;
	public GameObject DeathParticule;

	// Movement
	public float MovementSpeed = 3.0f;
	private GameObject[] WaypointSets = new GameObject[3];
	private Transform waypoints;
	private int nextWaypoint = 0;
	private int currentWaypoint = 0;
	public float PlayerStunTime = 1.0f;

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
		WaypointSets = GameObject.FindGameObjectsWithTag("WaypointsSet");
		waypoints = WaypointSets[Random.Range(0, WaypointSets.GetLength(0))].transform;
		nextWaypoint = Random.Range(0,waypoints.childCount);
		transform.position = waypoints.GetChild(nextWaypoint).transform.position;
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
		Transform targetWaypointLocation = waypoints.GetChild(nextWaypoint);
		Vector3 relative = targetWaypointLocation.position - transform.position;
		Vector3 movementNormal = Vector3.Normalize(relative);
		float distanceToWaypoint = relative.magnitude;

		if(distanceToWaypoint < 0.1f){
			currentWaypoint = nextWaypoint;
			if(currentWaypoint <= 2){
				nextWaypoint = Random.Range(3,6);
			}
			else{
				nextWaypoint = Random.Range(0,3);
			}
		} else {
			transform.position += new Vector3(movementNormal.x, movementNormal.y) * MovementSpeed * Time.deltaTime;
		}
	}

	void HandleFire(){
		fireTimer += Time.deltaTime;

		if(fireTimer >= FireRate){
			Instantiate(Projectile, transform.position, Quaternion.identity);
			fireTimer = 0.0f;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Player"){
			coll.gameObject.SendMessage("Stun", PlayerStunTime);
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
