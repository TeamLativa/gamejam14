using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	public GameObject ground;
	static public int life = 3;
	
	static public GameCamera cam;
	private float groundPosition = -5;
	
	void Start () {
		cam = GetComponent<GameCamera>();
		InstantiatePlayers();
	}
	// Spawn player
	private void InstantiatePlayers() {
		Vector3 position = new Vector3 (2f, 5f, 0.0f);
		cam.SetTarget((Instantiate(player,position,Quaternion.identity) as GameObject).transform);
	}
}
