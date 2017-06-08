using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsPanel : MonoBehaviour {
	
	public List<UI2DSprite> crystals;
	public List<Sprite> crystalsImages;
	int current_color;
	void FixedUpdate () {
		current_color = LevelController.current.getCurCrystalColor ();
		countCrystals (current_color);
	}

	void countCrystals(int num) {
		if (num != -1)
			crystals [num].sprite2D = crystalsImages [num];
	}
		
}
