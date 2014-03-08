using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	public GameObject player2;
	static public int life = 3;
	
	static public GameCamera cam;
	private float groundPosition = -5;
	
	void Start () {
		cam = GetComponent<GameCamera>();
		InstantiatePlayers();
	}
	// Spawn player
	private void InstantiatePlayers() {
		Vector2 playerPosition = new Vector2 (-10f, -5f);
		Vector2 player2Position = new Vector2 (9f, -5f);

		cam.SetTarget((Instantiate(player,playerPosition,Quaternion.identity) as GameObject).transform,(Instantiate(player2,player2Position,Quaternion.identity) as GameObject).transform);
	}
}
