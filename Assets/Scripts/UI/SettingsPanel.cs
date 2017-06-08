using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour {
	public MyButton closeBtn;
	public MyButton soundBtn;
	public MyButton musicBtn;
	public MyButton background;

	public Sprite soundOnImg, soundOffImg, musicOnImg, musicOffImg;

	// Use this for initialization
	void Start () {
		
		startMusic ();
		startSound ();
		closeBtn.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		background.signalOnClick.AddListener(this.OnCloseBtnAndBackground);
		soundBtn.signalOnClick.AddListener (this.OnSoundBtn);
		musicBtn.signalOnClick.AddListener (this.OnMusicBtn);

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCloseBtnAndBackground() {
		Time.timeScale = SettingsBtn.time;
		Destroy (this.gameObject);
	}



	void OnSoundBtn() {
		if (LevelController.getSound()) {
			OffSound ();
		} else {
			OnSound ();
		}
		
	}

	void OnMusicBtn() {
		if (LevelController.getMusic ()) {
			OffMusic ();
		} 

		else {
			OnMusic ();
		}

	}

	void startMusic() {
		if (LevelController.getMusic ())
			OnMusic ();
		else
			OffMusic ();
	}

	void startSound() {
		if (LevelController.getSound ())
			OnSound ();
		else
			OffSound ();
	}



	void OffMusic() {
		musicBtn.GetComponent<UI2DSprite> ().sprite2D = musicOffImg;
		musicBtn.GetComponent<UIButton> ().normalSprite2D = musicOffImg;
		LevelController.setMusic (false);
	}

	void OnMusic() {
		musicBtn.GetComponent<UI2DSprite> ().sprite2D = musicOnImg;
		musicBtn.GetComponent<UIButton> ().normalSprite2D = musicOnImg;
		LevelController.setMusic (true);
	}

	void OffSound() {
		soundBtn.GetComponent<UI2DSprite> ().sprite2D = soundOffImg;
		soundBtn.GetComponent<UIButton> ().normalSprite2D = soundOffImg;
		LevelController.setSound (false);
	}

	void OnSound() {
		soundBtn.GetComponent<UI2DSprite> ().sprite2D = soundOnImg;
		soundBtn.GetComponent<UIButton> ().normalSprite2D = soundOnImg;
		LevelController.setSound (true);
	}
}
