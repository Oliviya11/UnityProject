/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrg : MonoBehaviour {
	Vector3 pointA, pointB;
	Rigidbody2D myBody = null;
	public float moveBy;
	public float speed = 1;
	SpriteRenderer sr = null;
	Animator animator = null;
	Mode mode, oldMode;
	public Collider2D head, body;
	Renderer rend = null;

	public enum Mode {
		GoToA,
		GoToB,
		Attack,
		Stand,
		Die,
		Flip
		//...
	}
	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		pointA = this.transform.position;
		pointB = pointA;
		pointB.x += moveBy;
		sr = GetComponent<SpriteRenderer> ();
		mode = Mode.GoToB;
		animator = GetComponent<Animator> ();
		rend = GetComponent<Renderer> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (mode == Mode.Die) {
			StartCoroutine (dieAnimation ());
			
		} else if (mode == Mode.Attack) {
			animator.SetBool ("run", false);
			animator.SetBool ("walk", false);
			attackMethod ();
			speed = 0;
		}
		else {
			float value = getDirection ();
			walk (value);
			walkAnimation (value);
			if (HeroRabbit.rabbit_copy.getHealth()!=0) 
				OnRabbitNoticed ();

			flipPicture (value);
		}

	}

	void attackMethod() {
		animator.SetTrigger ("attack");
	}

	void walk(float value)
	{
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;

		}
	}
	void flipPicture(float value) 
	{

		if (value < 0) {
			sr.flipX = false;
		} else if (value > 0) {
			sr.flipX = true;
		}

	}


	float getDirection() {
		Mode oldMode;
		
		if(mode == Mode.GoToB) {
			if (transform.position.x < pointB.x) {
				oldMode = mode;
				mode = Mode.Stand;
				StartCoroutine (standLooking (2.0f, oldMode));
			}
			return -1; //Move left
		} else if(mode == Mode.GoToA) {
			if (transform.position.x >= pointA.x) {
				oldMode = mode;
				mode = Mode.Stand;
				StartCoroutine (standLooking (2.0f, oldMode));
			}
			return 1; //Move right
	     }
			return 0; //No movement
     }

	void walkAnimation(float value) {
		if (Mathf.Abs (value) > 0) {
			animator.SetBool ("walk", true);
		} else {
			animator.SetBool ("walk", false);
		}
	}

	void fromWalkToRunAnimation() {
		animator.SetBool ("run", true);
	
	} 
	void fromRunToWalkAnimation() {
		animator.SetBool ("run", false);
	
	} 
	IEnumerator standLooking(float duration, Mode oldMode) {
	    animator.SetBool ("walk", false);
		yield return new WaitForSeconds (duration);
		//Continue excution in few seconds
		//Other actions...
		toggle(oldMode);
		animator.SetBool ("walk", true);
	}

	void toggle(Mode oldMode) {
		if (oldMode == Mode.GoToA) {
			mode = Mode.GoToB;
		}
		else {
			mode = Mode.GoToA;
		}
	}

	bool OnRabbitNoticed() {
		float rabbitPos = HeroRabbit.rabbit_copy.transform.position.x;
		if ( rabbitPos < pointA.x && rabbitPos > pointB.x &&
		    Mathf.Abs (HeroRabbit.rabbit_copy.transform.position.y - transform.position.y) < 4f) {
			speed =2.3f;
			  fromWalkToRunAnimation ();
			  findRabbitLocation ();

			return true;
		} else {
			fromRunToWalkAnimation ();
		    speed = 1f;
		}
		return false;
	}

	void findRabbitLocation() {
		
		if (HeroRabbit.rabbit_copy.transform.position.x < transform.position.x)
			mode = Mode.GoToB;
		else
			mode = Mode.GoToA;
		oldMode = mode;
	}

	public void die() {
		mode = Mode.Die;
	}


	public void attack() {
		mode = Mode.Attack;
	}
	public void setUsualBehavior() {
		animator.SetBool ("walk", true);
		mode = oldMode; 
	}

	public bool isDead() {
		return mode == Mode.Die;
	}

	void becomeTransparent() {
		Color c = new Color (1.0f, 1.0f, 1.0f, 0f);
		rend.material.SetColor ("_Color", c);
	}

	IEnumerator dieAnimation() {
		animator.SetBool ("die", true);
     	yield return new WaitForSeconds (2f);
		//Continue excution in few seconds
		//Other actions...
		becomeTransparent();
	}



}
*/


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
		if ( rabbitPos < pointA.x && rabbitPos > pointB.x &&
			Mathf.Abs (HeroRabbit.rabbit_copy.transform.position.y - transform.position.y) < 2f) {
			speed =2.3f;
			fromWalkToRunAnimation ();
			findRabbitLocation ();

		} else {
			fromRunToWalkAnimation ();
			speed = 1f;
		}
	}

	void findRabbitLocation() {

		if (HeroRabbit.rabbit_copy.transform.position.x < transform.position.x)
			mode = Mode.GoToB;
		else
			mode = Mode.GoToA;
		oldMode = mode;
	}

	void fromWalkToRunAnimation() {
		animator.SetBool ("run", true);

	} 
	void fromRunToWalkAnimation() {
		animator.SetBool ("run", false);

	} 
}

