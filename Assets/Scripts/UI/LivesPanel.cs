using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPanel : MonoBehaviour {
	public List<UI2DSprite> lifes;
	public Sprite empty_life;
	public Sprite full_life;
	int maxLifesNumber = 3;

	void FixedUpdate () {
		countLifes ();
		increaseLife ();
	}

	void countLifes() {
		for (int i = 0; i < maxLifesNumber; ++i)
			if (i == LevelController.current.getLifesNumber ())
				lifes [i].sprite2D = empty_life;
	}

	void increaseLife() {
		if (LevelController.current.getIfIncreaseLife ()) {
			lifes [LevelController.current.getLifesNumber ()-1].sprite2D = full_life;
			LevelController.current.setIncreaseLifeFaulse ();
		}
	}
}
