using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	protected virtual void OnRabbitHit(HeroRabbit rabbit) {

	}
	protected virtual bool findCondition(HeroRabbit rabbit) {
		return rabbit.getHealth()!=0;
	}
	void OnTriggerEnter2D(Collider2D collider) {
	   HeroRabbit rabbit = collider.GetComponent<HeroRabbit> ();

	   if (rabbit != null) {
		   OnRabbitHit (rabbit);
			if (findCondition(rabbit)) 
		        CollectedHide ();
		}
	}

	public void CollectedHide() {
		Destroy (gameObject);
	}


}
