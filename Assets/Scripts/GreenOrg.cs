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
	Mode mode;

	public enum Mode {
		GoToA,
		GoToB,
		Attack,
		Stand
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

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float value = getDirection();
		//Debug.Log (value);
		walk (value);
		walkAnimation (value);
		OnRabbitNoticed (value);
		flipPicture (value);

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

	void OnRabbitNoticed(float value) {
		Mode oldMode = mode;
		if (HeroRabbit.rabbit_copy.transform.position.x < pointA.x &&
		    HeroRabbit.rabbit_copy.transform.position.x > pointB.x &&
		    Mathf.Abs (HeroRabbit.rabbit_copy.transform.position.y - transform.position.y) < 3f) {
			oldMode = mode;
			speed = 3f;
			fromWalkToRunAnimation ();
			findRabbitLocation ();
		} else {
			mode = oldMode;
			fromRunToWalkAnimation ();
			speed = 1;
		}
	}

	void findRabbitLocation() {
		if (HeroRabbit.rabbit_copy.transform.position.x < transform.position.x)
			mode = Mode.GoToB;
		else
			mode = Mode.GoToA;
	}


}
