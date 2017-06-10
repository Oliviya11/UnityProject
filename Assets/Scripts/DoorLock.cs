using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour {

	public int levelId;
	public Sprite filledImage;
	// Use this for initialization
	void Start () {
		checkIfLock ();
	}

	void checkIfLock() {
		if (levelId - 1 > 0) {
			string str = PlayerPrefs.GetString ("info" + (levelId - 1).ToString (), null);
			LevelInfo info = JsonUtility.FromJson<LevelInfo> (str);
			if (!info.passLevel)
				changeSprite ();
		}
	}
	void changeSprite() {
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = filledImage;
	}

	// Update is called once per frame
	void Update () {

	}
}
