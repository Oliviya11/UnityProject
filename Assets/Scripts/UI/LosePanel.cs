using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour {
	public MyButton closeButton;
	public MyButton repeatButton;
	public MyButton menuButton;
	public MyButton background;
	public List<Sprite> crystalsImages;
	public List<UI2DSprite> crystals;
	AudioSource loseSource;
	public AudioClip loseSound;

	// Use this for initialization
	void Start () {
		if (LevelController.getSound ()) {
			loseSource = gameObject.AddComponent<AudioSource> ();
			loseSource.clip = loseSound;
			loseSource.Play ();
		}
		closeButton.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		background.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		//Тут я переплутала префаби, тобто menuButton насправді - repeatButton і навпаки;
		menuButton.signalOnClick.AddListener (this.OnRepeatBtn);
		repeatButton.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		setCrystals ();
	}
	public void 	OnCloseBtnAndBackground() {
		StartCoroutine(LevelController.current.openMenu());
	}

	public void OnRepeatBtn() {
		StartCoroutine(LevelController.current.openLevel());
	}
	void setCrystals() {
		for (int i = 0; i < 3; i++) {
			if (LevelController.current.containsCrystal (i)) {
				crystals [i].sprite2D = crystalsImages [i];
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
