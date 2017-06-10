
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrg : Org {
	
	protected override void prepareToAttack() {
		animator.SetBool ("run", false);
		animator.SetBool ("walk", false);
	}
	protected override void OnRabbitNoticed() {
		float rabbitPos = HeroRabbit.rabbit_copy.transform.position.x;
		if ((rabbitPos < pointA.x && rabbitPos > pointB.x && moveBy < 0) || (rabbitPos > pointA.x && rabbitPos < pointB.x && moveBy > 0)
			&& Mathf.Abs (HeroRabbit.rabbit_copy.transform.position.y - transform.position.y) < 2f) {
			speed =2.3f;
			fromWalkToRunAnimation ();
			findRabbitLocation ();

		} else {
			fromRunToWalkAnimation ();
			speed = 1f;
		}
	}

	void findRabbitLocation() {

		if (HeroRabbit.rabbit_copy.transform.position.x < transform.position.x && moveBy<0 ||
			HeroRabbit.rabbit_copy.transform.position.x > transform.position.x && moveBy>0)
			mode = Mode.GoToB;
		else
			mode = Mode.GoToA;
		oldMode = mode;
	}

	void fromWalkToRunAnimation() {
		animator.SetBool ("run", true);

	} 
	void fromRunToWalkAnimation() {
		if (HeroRabbit.rabbit_copy.getHealth () != 0 && !isDead ())
		    animator.SetBool ("run", false); 
	} 

	protected override void attackMethod() {
		animator.SetTrigger ("attack");
	}
}

