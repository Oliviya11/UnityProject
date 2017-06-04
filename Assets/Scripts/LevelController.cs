using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;
	int fruitsNumber=0, coinsNumber = 0, lifesNumber=3;
	int curCrystalColor = -1;
	Vector3 startingPosition;

	void Awake() {
		current = this;
	}

	public void setStartPosition(Vector3 pos){
		this.startingPosition = pos;
	}

	public void onRabbitDeath(HeroRabbit rabbit){
		decreaseLifeNumber ();
		rabbit.transform.position = startingPosition;
		rabbit.alive ();
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
