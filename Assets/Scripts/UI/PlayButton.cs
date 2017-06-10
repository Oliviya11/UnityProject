using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	public MyButton playButton;

	void Start() {

		playButton.signalOnClick.AddListener (this.onPlay);
	}

	public void onPlay() {
		LevelController.current.writeMusic ();
		LevelController.current.save ();
		SceneManager.LoadScene ("ChangeLevel");
	}

}
