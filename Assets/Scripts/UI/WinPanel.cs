using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour {
	public MyButton closeButton;
	public MyButton background;
	public MyButton repeatButton;
	public MyButton menuButton;
	public UILabel fruitLabel;
	public UILabel coinsLabel;
	public List<Sprite> crystalsImages;
	public List<UI2DSprite> crystals;
	AudioSource winSource;
	public AudioClip winSound;


	// Use this for initialization
	void Start () {
		if (LevelController.getSound ()) {
			winSource = gameObject.AddComponent<AudioSource> ();
			winSource.clip = winSound;
			winSource.Play ();
		}
		closeButton.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		background.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		menuButton.signalOnClick.AddListener (this.OnCloseBtnAndBackground);
		repeatButton.signalOnClick.AddListener (this.OnRepeatBtn);
		setCrystals ();
		setCoinsLabel ();
		setFruitsLabel ();
	}


	public void OnCloseBtnAndBackground() {
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
	void setCoinsLabel() {
		coinsLabel.text ="+"+ LevelController.current.getCoinsNumber().ToString();
	}

	void setFruitsLabel() {
		fruitLabel.text = LevelController.current.getFruitsNumber ().ToString () + "/" + LevelController.current.getMaxFruitsNumber ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
