using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPanel : MonoBehaviour {
	public int maxNumber=3;
	public UILabel label;
	public int id;
	int current_number;

	public void setCoins(int coinsNum) {
		current_number = coinsNum;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (id == 0) {
			current_number = PlayerPrefs.GetInt ("coins", 0);;
		
		} else {
			current_number = LevelController.current.getCoinsNumber ();
		}
		writeText ();
	}

	int getZeroNumber(int number) {
		int count = (number == 0) ? 1 : 0;
		while (number != 0) {
			count++;
			number /= 10;
		}
		return maxNumber - number;
	}

	void writeText() {
		string text = "";
		for (int i = 0; i < getZeroNumber (current_number); ++i) {
			text += "0";
		}
		text += current_number.ToString ();
		label.text = text;
	}
}
