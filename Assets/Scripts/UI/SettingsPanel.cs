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
		closeBtn.signalOnClick.AddListener (this.OnCloseBtn);
		background.signalOnClick.AddListener(this.OnBackground);
		soundBtn.signalOnClick.AddListener (this.OnSoundBtn);
		musicBtn.signalOnClick.AddListener (this.OnMusicBtn);

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnBackground() {
		close();
	}
	void OnCloseBtn() {
		SettingsBtn.playSoundOnClosingSettingsPanel ();
		close();
	}
	void close() {
		
		Time.timeScale = SettingsBtn.time;
		Destroy (this.gameObject);
		Destroy (background.gameObject);
	}



	void OnSoundBtn() {
		if (LevelController.getSound()) {
			OffSound ();
		} else {
			OnSound ();
		}
		//saveing not only when win or lose...
		LevelController.current.writeMusic ();
		LevelController.current.save ();
	}

	void OnMusicBtn() {
		if (LevelController.getMusic ()) {
			OffMusic ();
		} 

		else {
			OnMusic ();
		}
		//saveing not only when win or lose...
		LevelController.current.writeMusic ();
		LevelController.current.save ();
	}

	void startMusic() {
		if (LevelController.getMusic ()) {
			OnMusic ();
		}
		else
			OffMusic ();
	}

	void startSound() {
		if (LevelController.getSound ()) {
			OnSound ();

		}
		else
			OffSound ();
	}



	void OffMusic() {
		musicBtn.GetComponent<UI2DSprite> ().sprite2D = musicOffImg;
		musicBtn.GetComponent<UIButton> ().normalSprite2D = musicOffImg;
		LevelController.setMusic (false);
		if (HeroRabbit.rabbit_copy!=null) HeroRabbit.rabbit_copy.muteOrActiveBackgroundMusic ();
	}

	void OnMusic() {
		musicBtn.GetComponent<UI2DSprite> ().sprite2D = musicOnImg;
		musicBtn.GetComponent<UIButton> ().normalSprite2D = musicOnImg;
		LevelController.setMusic (true);
		if (HeroRabbit.rabbit_copy!=null) HeroRabbit.rabbit_copy.muteOrActiveBackgroundMusic ();
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
