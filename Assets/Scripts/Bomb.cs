using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {
	 
	protected override bool findCondition(HeroRabbit rabbit) {
		
		return !rabbit.hasShield();
	}

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		if (rabbit.getHealth () != 0) {
			if (rabbit.getFirstBomb ()) {
				CollectedHide ();
			}
			rabbit.setDecrease (true);
			rabbit.decreaseHealth ();
			rabbit.muteMusicOnRun ();
			rabbit.playMusicOnBomb ();

		}

	}
}
