using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject Player1;
	public GameObject Player2;

	public GameObject OrangePlayerWon;
	public GameObject BluePlayerWon;

	public bool EnemySpawnEnabled = true;
	private bool gameIsOver = false;

	public GameObject FlyingEnemy;
	public float FlyingEnemySpawnTimer = 5.0f;
	private float flyingEnemyTimer = 0.0f;
	public GameObject LandEnemy;
	public float LandEnemySpawnTimer = 3.0f;
	private float landEnemyTimer = 0.0f;

	public Transform[] WaypointSets = new Transform[3];
	public Transform SpawnPoints;

	private int minQtyItemNeeded = 1;
	public int MaxQtyItemNeeded = 3;

	
	void Start () {
		GenerateRequirementsList();
	}

	void GenerateRequirementsList(){
		int[] r = new int[6];
		for (int i = 0; i < r.Length; i++){
			r[i] = Random.Range(minQtyItemNeeded, MaxQtyItemNeeded + 1);
		}

		// Set requirements in the player
		Player1.GetComponent<NonPhysicsPlayerController>().setParameterNeeded(r[0], r[1], r[2], r[3], r[4], r[5]);
		Player2.GetComponent<NonPhysicsPlayerController>().setParameterNeeded(r[0], r[1], r[2], r[3], r[4], r[5]);
	}

	public void SetEnemySpawnEnabled(bool newState){
		EnemySpawnEnabled = newState;
	}

	void Update(){
		if (!gameIsOver) {
			if(EnemySpawnEnabled){
				HandleEnemySpawn();
			}
		}
		else{
			if(Input.GetButtonDown("Startbutton"))
				Application.LoadLevel("Menu");
		}

	}

	public void SetGameOver(int winnerPlayer){
		gameIsOver = true;

		if(winnerPlayer == 1)
			Instantiate(OrangePlayerWon, new Vector3(0,0,0), Quaternion.identity);
		else
			Instantiate(BluePlayerWon, new Vector3(0,0,0), Quaternion.identity);

		Debug.Log ("Player: " + winnerPlayer + " won!");
	}

	void HandleEnemySpawn(){
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
