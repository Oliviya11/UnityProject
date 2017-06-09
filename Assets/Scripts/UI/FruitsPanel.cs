using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsPanel : MonoBehaviour {
	int current_number;
	public UILabel label;

	// Update is called once per frame
	void FixedUpdate () {
		current_number = LevelController.current.getFruitsNumber ();
		writeText ();
	}

	void writeText() {
		label.text = current_number.ToString () + "/" + LevelController.current.getMaxFruitsNumber();
	}

}
