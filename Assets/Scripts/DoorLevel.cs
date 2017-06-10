using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevel : MonoBehaviour {
	public int levelId;
	public Sprite filledImage;
	// Use this for initialization
	void Start () {
		checkIfLevelPassed ();
	}

	void checkIfLevelPassed() {
		string str = PlayerPrefs.GetString ("info"+levelId.ToString(), null);
		LevelInfo info = JsonUtility.FromJson<LevelInfo> (str);
		if (info.passLevel)
			changeSprite ();
	}
	void changeSprite() {
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = filledImage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
