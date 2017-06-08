using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsInfo {
	LevelInfo info;
	int coinsNumber;
	// Use this for initialization
	public void prepeareInfo () {
		string str = PlayerPrefs.GetString ("info", null);
		info = JsonUtility.FromJson<LevelInfo> (str);
		if (this.info!=null) {
			coinsNumber = info.coinsNumber;
		}
	}

	public int getCoinsNumber() {
		return coinsNumber;
	}
	
}
