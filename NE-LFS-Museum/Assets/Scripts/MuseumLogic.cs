using UnityEngine;
using System.Collections;

public class MuseumLogic : MonoBehaviour {
	
	public GameObject player;
	public GameObject eventSystem;

	public GameObject startUI;
	public GameObject introAudioHolder;


	public float height = 2;
	public float maxMoveDistance = 10;

	private GameObject _previousWaypoint = null;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void startGuidedTour() {
		startUI.SetActive(false);
		introAudioHolder.GetComponent<GvrAudioSource> ().Play ();
	}

	public void playAudioExperience(GameObject audioClip) {
		audioClip.GetComponent<GvrAudioSource> ().Play ();
	}
		

	public void Move(GameObject waypoint) {
		Debug.Log ("Move to waypoint: " + waypoint.name.ToString());

		iTween.MoveTo (player, 
			iTween.Hash (
				"position", new Vector3 (waypoint.transform.position.x, waypoint.transform.position.y, waypoint.transform.position.z), 
				"time", 3F, 
				"easetype", "linear",
				"onstart", "hideWaypoint", 
				"onstarttarget", this.gameObject, 
				"onstartparams", waypoint
			)
		);
	}

	private void hideWaypoint(GameObject waypoint) {
		Debug.Log ("Executing Hiding function");
		if (_previousWaypoint != null) {
			unhideWaypoint (_previousWaypoint);
		}
		waypoint.SetActive (false);
		_previousWaypoint = waypoint;
	}

	private void unhideWaypoint(GameObject waypoint) {
		waypoint.SetActive (true);
	}
}
