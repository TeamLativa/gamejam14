using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject Player;
	public GameObject Player2;
	
	static public GameCamera Cam;
	
	void Start () {
		Cam = GetComponent<GameCamera>();
		InstantiatePlayers();
	}
	// Spawn player
	private void InstantiatePlayers() {
		Transform playerTransform = Player.transform;
		Transform player2Transform = Player2.transform;

		Cam.SetTarget(playerTransform,player2Transform);
	}
}
