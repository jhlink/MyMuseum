﻿using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class MuseumLogic : MonoBehaviour {
	
	public GameObject player;
	public GameObject eventSystem;

	public GameObject startUI;
	public GameObject introAudioHolder;


	public float height = 2;
	public float maxMoveDistance = 10;

	private GameObject _previousWaypoint = null;
	private GameObject _previousVideoPlayerHolder = null;
	private VideoPlayer _tempVideoPlayer = null;
	private GvrAudioSource _tempAudioSource = null;
	private bool playStartFlag = false;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		if (_previousVideoPlayerHolder && _tempVideoPlayer) {
			if (!_tempVideoPlayer.isPrepared) {
				return;
			}
			if (playStartFlag && !_tempVideoPlayer.isPlaying) {
				resetVideoPlayerController ();
			}
		}
	}

	public void startGuidedTour() {
		startUI.SetActive(false);
		introAudioHolder.GetComponent<GvrAudioSource> ().Play ();
	}

	public void playAudioExperience(GameObject audioClip) {
		
		if (_tempVideoPlayer) {
			resetVideoPlayerController ();
		}

		_tempAudioSource = audioClip.GetComponent<GvrAudioSource> ();
		_tempAudioSource.Play ();
	}

	public void setFlag(VideoPlayer source) {
		playStartFlag = true;
	}

	public void resetVideoPlayerController() {
		_tempVideoPlayer.Stop ();
		_tempVideoPlayer.targetTexture.DiscardContents ();
		_tempVideoPlayer.targetTexture.Release ();

		_previousVideoPlayerHolder.SetActive (false);
		_previousVideoPlayerHolder = null;
		_tempVideoPlayer.started += null;
		_tempVideoPlayer = null;
		playStartFlag = false;
	}



	public void playVideoExperience(GameObject videoPlayerHolder) {

		if (_tempVideoPlayer) {
			resetVideoPlayerController ();
		} else if (_tempAudioSource) {
			_tempAudioSource.Stop ();
		}
			
		_tempVideoPlayer = videoPlayerHolder.GetComponentInChildren<VideoPlayer> ();
		_previousVideoPlayerHolder = videoPlayerHolder;
		_previousVideoPlayerHolder.SetActive (true);
		_tempVideoPlayer.started += setFlag;
		_tempVideoPlayer.Play ();
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
