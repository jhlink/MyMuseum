using UnityEngine;
using System.Collections;

public class MuseumLogic : MonoBehaviour {
	
	public GameObject player;
	public GameObject eventSystem;

	public float height = 2;
	public float maxMoveDistance = 10;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void Move(GameObject waypoint) {
		Debug.Log ("Move to waypoint: " + waypoint.name.ToString());

		iTween.MoveTo (player, 
			iTween.Hash (
				"position", new Vector3 (waypoint.transform.position.x, waypoint.transform.position.y + height / 2, waypoint.transform.position.z), 
				"time", 3F, 
				"easetype", "linear"
			)
		);
	}
}
