using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {
	Renderer rend = null;
	static int counter;
	int id;
	void Start() {
		id = counter++;
	//	Debug.Log (id);
		rend = GetComponent<Renderer> ();
	}

	void FixedUpdate() {
		if (LevelController.current.containFruit(id))
			becomeTransparent ();
	}
	protected void becomeTransparent() {
		Color c = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		rend.material.SetColor ("_Color", c);
	}
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.current.increaseFruits ();
		if (LevelController.getSound()) rabbit.playMusicOnFruit ();
		LevelController.current.putInList (id);
	}

	public static void setCounterToZero() {
		counter = 0;
	}

}
