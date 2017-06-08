using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevelComplete : MonoBehaviour {
	LevelInfo info;
	public int maxFruitsNumber;
	void Start() {
		info = new LevelInfo ();
	}

	void modifyLevelInfo() {
		Fruit.setCounterToZero ();
		info.coinsNumber = LevelController.current.getCoinsNumber ();
		if (LevelController.current.getCrystalsNumber () == 3) {
			Debug.Log ("allCrystals");
			info.hasAllCrystals = true;
		}
		if (LevelController.current.getFruitsNumber () == maxFruitsNumber) {
			info.hasAllFruits = true;
		}
		info.collectedFruits = LevelController.current.getFruits ();
	}

	void save() {
		string str = JsonUtility.ToJson (info);
		PlayerPrefs.SetString ("info", str);
		PlayerPrefs.Save ();
	}
	public void saveInfo() {
		modifyLevelInfo ();
		save ();
	}

}
