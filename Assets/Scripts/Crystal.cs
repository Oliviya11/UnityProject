using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {
	public int color;

	protected override void OnRabbitHit (HeroRabbit rabbit)
	{
		LevelController.current.setCurCrystalColor (color);
		LevelController.current.addCrystal (color);
		if (LevelController.getSound())  rabbit.playMusicOnCrystal ();
	}
}
