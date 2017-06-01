using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotWeapon : Collectable {
	public float speed = 5;
	Vector2 moveBy, moveLeft, moveRight;
	SpriteRenderer sr = null;
	Rigidbody2D myBody;
	Mode mode;
	Vector3 pos;

	protected enum Mode {
		GoToLeft,
		GoToRight
	}

	void Start() {
		pos = this.transform.localPosition;
		sr = GetComponent<SpriteRenderer> ();
		myBody = GetComponent<Rigidbody2D> ();
		StartCoroutine (destroyLater());
		moveLeft = new Vector2 (-speed, 0);
		moveRight = new Vector2 (speed, 0);
		moveBy = moveLeft;

	}
	public void moveToLeft() {
		mode = Mode.GoToLeft;
	}

	public void moveToRight() {
		mode = Mode.GoToRight;
	}

	public void changePosition() {
		pos.y += 18f;
		this.transform.localPosition = pos;
	}

	void changeDirection() {
		if (mode == Mode.GoToLeft) {
			moveBy = moveLeft;
		//	Debug.Log ("move left");
		} else {
		//	Debug.Log ("move right");
			moveBy = moveRight;
		}
	}
	void FixedUpdate() {
		
		changeDirection ();
		launch ();
		flipPicture ();

	}
		
	public void launch() {
			myBody.MovePosition(myBody.position + moveBy * Time.fixedDeltaTime);
	}

	void flipPicture() 
	{
		if (mode == Mode.GoToLeft) {
			sr.flipX = true;
		} else if (mode == Mode.GoToRight) {
			sr.flipX = false;
		}
	}

	IEnumerator destroyLater() {
		yield return new WaitForSeconds (3.0f);
		Destroy (this.gameObject);

	}
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
		} 

	}
}
