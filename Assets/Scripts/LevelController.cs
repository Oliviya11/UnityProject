﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	public static LevelController current = null;
	int fruitsNumber=0, coinsNumber = 0, lifesNumber=3, crystalsCounter=0;
	static int staticCoinsNumber;
	int curCrystalColor = -1;
	Vector3 startingPosition;
	static bool music=true, sound=true;
	LevelInfo info;
	List<int> fruits = new List<int>();
	void Awake() {
		current = this;
	}
	void Start() {
		setInfo ();

	}
		

	public void setInfo() {
		string str = PlayerPrefs.GetString ("info", null);
		info = JsonUtility.FromJson<LevelInfo> (str);
		if (this.info!=null) {
			staticCoinsNumber += info.coinsNumber;
			fruits = info.collectedFruits;
		}
	}

	public bool containFruit(int id) {
		return fruits.Contains (id);
	}

	public void putInList(int id) {
		if (!fruits.Contains(id))
		     fruits.Add (id);
	}

	public List<int> getFruits() {
		return fruits;
	}
	public void setStartPosition(Vector3 pos){
		this.startingPosition = pos;
	}
	public static void setMusic(bool val) {
		music = val;
	}
	public static void setSound(bool val) {
		sound = val;
	}

	public static bool getMusic() {
		return music;
	}
	public static bool getSound() {
		return sound;
	}

	public void onRabbitDeath(HeroRabbit rabbit){
		decreaseLifeNumber ();
		if (lifesNumber==0) 
			StartCoroutine(openChangingScene());
		rabbit.transform.position = startingPosition;
		rabbit.alive ();
	}

	IEnumerator openChangingScene() {
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
		crystalsCounter++;
	}

	public int getCurCrystalColor() {
		return curCrystalColor;
	}

	public int getCrystalsNumber() {
		return crystalsCounter;
	}

	public int getStaticCoinsNumber() {
		return staticCoinsNumber;
	}
}
