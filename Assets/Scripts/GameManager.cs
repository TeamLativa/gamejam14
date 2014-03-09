using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject Player1;
	public GameObject Player2;

	public GameObject FlyingEnemy;
	public float FlyingEnemySpawnTimer = 5.0f;
	private float flyingEnemyTimer = 0.0f;
	public GameObject LandEnemy;
	public float LandEnemySpawnTimer = 3.0f;
	private float landEnemyTimer = 0.0f;

	public Transform[] WaypointSets = new Transform[3];
	public Transform SpawnPoints;
	
	static public GameCamera Cam;
	
	void Start () {

	}

	void Update(){
		flyingEnemyTimer += Time.deltaTime;
		landEnemyTimer += Time.deltaTime;

		if(flyingEnemyTimer >= FlyingEnemySpawnTimer){
			// Spawn FlyingEnemy
			GameObject flyEnmy = (GameObject) Instantiate(FlyingEnemy, new Vector3(-18.0f, -18.0f), Quaternion.identity);
			flyingEnemyTimer = 0.0f;
		}

		if(landEnemyTimer >= LandEnemySpawnTimer){
			// Spawn LandEnemy
			GameObject landEnmy = (GameObject) Instantiate(LandEnemy, new Vector3(-18.0f, -18.0f), Quaternion.identity);
			landEnemyTimer = 0.0f;
		}
	}
}
