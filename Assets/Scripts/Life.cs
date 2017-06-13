using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable {

	protected override void OnRabbitHit (HeroRabbit rabbit)
	{
		LevelController.current.increaseLifeNumber ();
		HeroRabbit.rabbit_copy.playMusicOnEatingLife ();
	}
}
