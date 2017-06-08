using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeOrg : Org {

	public float radius = 5f, carrotTime = 2f;
	public GameObject prefabCarrot;
	bool rabbitIsNoticed = false, carrotDirectionLeft = false;
	float last_carrot = 0;
	Vector3 orgPos;

	protected override void FixedUpdate ()
	{
		base.FixedUpdate ();
		orgPos = this.transform.position;
	}


	protected override void prepareToAttack() {
		animator.SetBool ("run", false);
		animator.SetBool ("walk", false);
		OnRabbitNoticed ();
	}
	protected override void OnRabbitNoticed() {
		speed = 1;
		float rabbitPos = HeroRabbit.rabbit_copy.transform.position.x;
		if (Mathf.Abs (rabbitPos - transform.position.x) <= radius &&
			Mathf.Abs(HeroRabbit.rabbit_copy.transform.position.y - transform.position.y) < 4f) {
			playMusicOnAttack ();
			speed = 0;
			findRabbitLocation ();
			attack ();
			rabbitIsNoticed = true;
		} else {
			setUsualBehavior ();
		}
	}

	public  override void setUsualBehavior() {
		if (rabbitIsNoticed) {
			animator.SetBool ("walk", true);
			mode = oldMode;
			rabbitIsNoticed = false;
		}
	}

	void findRabbitLocation() {
		mode = oldMode;
		if (HeroRabbit.rabbit_copy.transform.position.x < transform.position.x) {
			sr.flipX = false;
			carrotDirectionLeft = true;
		} else {
			sr.flipX = true;
			carrotDirectionLeft = false;
		}
	}

	void findCarrotDirection() {

	}

	protected override void attackMethod() {
			animator.SetTrigger ("attack");
			launchCarrot ();
	}
		
	void launchCarrot() {
		if (Time.time - last_carrot > 2.0f && HeroRabbit.rabbit_copy.getHealth () != 0 && !isDead()) {
			//Створюємо копію Prefab
			GameObject obj = GameObject.Instantiate (this.prefabCarrot);
			CarrotWeapon car = obj.GetComponent<CarrotWeapon> ();
			//	car.changePosition ();
			orgPos.y += 0.8f;
			obj.transform.position = orgPos;
			if (carrotDirectionLeft)
				car.moveToLeft ();
			else
				car.moveToRight ();	
				
			last_carrot = Time.time;
		} else if (HeroRabbit.rabbit_copy.getHealth () == 0) {
			StartCoroutine (dieEffectLater());
		}
	}

	IEnumerator dieEffectLater() {
		mode = Mode.Stand;
		yield return new WaitForSeconds (1.0f);

	}
	protected override bool attackCondition() {
		return mode == Mode.Attack && HeroRabbit.rabbit_copy.getHealth() != 0;
	}


}