using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable {
	protected override void OnRabbitHit (HeroRabbit rabbit)
	{
		LevelController.current.increasCoins ();
		rabbit.playMusicOnCoin ();
	}

}
