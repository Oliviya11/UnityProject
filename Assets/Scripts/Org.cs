using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Org : MonoBehaviour {
	AudioSource attackSource = null;
	AudioSource dieSource = null;
	public AudioClip attackSound = null;
	public AudioClip dieSound = null;
	protected Vector3 pointA, pointB;
	protected Rigidbody2D myBody = null;
	public float moveBy;
	protected float speed=1;
	protected SpriteRenderer sr = null;
	protected Animator animator = null;
	protected Mode mode, oldMode;
	public Collider2D head, body;
	protected Renderer rend = null;

	protected enum Mode {
		GoToA,
		GoToB,
		Attack,
		Stand,
		Die,
		Flip
		//...
	}
	// Use this for initialization
	protected virtual void Start () {


		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;

		dieSource = gameObject.AddComponent<AudioSource> ();
		dieSource.clip = dieSound;

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
	protected virtual void FixedUpdate () {
		if (mode == Mode.Die) {
			StartCoroutine (dieAnimation ());

		} else if (attackCondition()) {
		//	animator.SetBool ("run", false);
			prepareToAttack ();
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

		if (HeroRabbit.rabbit_copy.getHealth () == 0) {
			prepareToAttack ();
		}

	}

	public void playMusicOnAttack() {
		attackSource.Play ();

	}

	public IEnumerator playMusicOnDeth() {
		yield return new WaitForSeconds (0.5f);
		if (LevelController.getSound()) 
		dieSource.Play ();
	}
		
	protected virtual bool attackCondition() {
		return mode == Mode.Attack;
	}
	protected virtual void prepareToAttack() {
		animator.SetBool ("walk", false);
	}
	protected virtual void attackMethod() {
		animator.SetTrigger ("attack");
	}

	protected void walk(float value)
	{
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;

		}
	}
	protected void flipPicture(float value) 
	{

		if (value < 0) {
			sr.flipX = false;
		} else if (value > 0) {
			sr.flipX = true;
		}

	}


	protected float getDirection() {
		Mode oldModeIn;
		if(mode == Mode.GoToB) {
			if (transform.position.x < pointB.x && moveBy<0 || transform.position.x > pointB.x && moveBy>0) {
				oldModeIn = mode;
				mode = Mode.Stand;
				StartCoroutine (standLooking (2.0f, oldModeIn));

			}
			if (moveBy > 0)
				return 1;
			return -1; //Move left
		} else if(mode == Mode.GoToA) {
			if (transform.position.x >= pointA.x && moveBy<0 || transform.position.x <= pointA.x && moveBy>0) {
				oldModeIn = mode;
				mode = Mode.Stand;
				StartCoroutine (standLooking (2.0f, oldModeIn));
			}
			if (moveBy > 0)
				return -1;
			return 1; //Move right
		}
		return 0; //No movement
	}

	protected void walkAnimation(float value) {
		if (Mathf.Abs (value) > 0) {
			animator.SetBool ("walk", true);
		} else {
			animator.SetBool ("walk", false);
		}
	}


	protected IEnumerator standLooking(float duration, Mode oldMode) {
		animator.SetBool ("walk", false);
		yield return new WaitForSeconds (duration);
		//Continue excution in few seconds
		//Other actions...
		toggle(oldMode);
		animator.SetBool ("walk", true);
	}




	protected void toggle(Mode oldMode) {
		if (oldMode == Mode.GoToA) {
			mode = Mode.GoToB;
		}
		else {
			mode = Mode.GoToA;
		}
	}

	protected virtual void OnRabbitNoticed() {
		
	}

	public void die() {
		mode = Mode.Die;
	}


	public void attack() {
		mode = Mode.Attack;
	}
	public  virtual void setUsualBehavior() {
		animator.SetBool ("walk", true);
		mode = oldMode; 
	}

	public bool isDead() {
		return mode == Mode.Die;
	}

	protected void becomeTransparent() {
		Color c = new Color (1.0f, 1.0f, 1.0f, 0f);
		rend.material.SetColor ("_Color", c);
	}

	protected IEnumerator dieAnimation() {
		animator.SetBool ("die", true);
		yield return new WaitForSeconds (1.5f);
		Destroy (this.gameObject);
	}



}
