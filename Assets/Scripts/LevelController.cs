using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	
	public static LevelController current = null;
	int fruitsNumber=0, coinsNumber = 0, lifesNumber=3;
	int curCrystalColor = -1;
	Vector3 startingPosition;
	bool music=true, sound=true;

	void Awake() {
		current = this;
	}

	public void setStartPosition(Vector3 pos){
		this.startingPosition = pos;
	}
	public void setMusic(bool val) {
		music = val;
	}
	public void setSound(bool val) {
		sound = val;
	}

	public bool getMusic() {
		return music;
	}
	public bool getSound() {
		return sound;
	}

	public void onRabbitDeath(HeroRabbit rabbit){
		decreaseLifeNumber ();
		if (lifesNumber==0) 
			StartCoroutine(openLevel ());
		rabbit.transform.position = startingPosition;
		rabbit.alive ();
	}

	IEnumerator openLevel() {
		yield return new WaitForSeconds (1f);
			SceneManager.LoadScene ("ChangeLevel");
	}

	public int getFruitsNumber() {
		return fruitsNumber;
	}

	public int getCoinsNumber() {
		return coinsNumber;
	}

	public int getLifesNumber() {
		return lifesNumber;
	}

	void decreaseLifeNumber() {
		lifesNumber--;
	}

	public void increasCoins() {
		coinsNumber++;
	}
	public void increaseFruits() {
		fruitsNumber++;
	}

	public void setCurCrystalColor(int color) {
		curCrystalColor = color;
	}

	public int getCurCrystalColor() {
		return curCrystalColor;
	}
}
