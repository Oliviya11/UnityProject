using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	Rigidbody2D myBody = null;
	SpriteRenderer sr = null;
	Animator animator = null;
	bool isGrounded = false;
	bool jumpActive = false;
	float jumpTime = 0f;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelController.current.setStartPosition (transform.position);
		animator = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();

	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");

		run (value);
		flipPicture (value);
		runAnimation (value);	
		checkIfIsGrounded ();
		jump ();
		jumpAnimation ();


	}

	void run(float value)
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
			sr.flipX = true;
		}
		else if (value > 0) {
			sr.flipX = false;
		}
	}

	void runAnimation(float value) {
		if (Mathf.Abs (value) > 0) {
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
		}
	}

	void checkIfIsGrounded(){
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");

		RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);

		if (hit) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

		Debug.DrawLine (from, to, Color.red);
	}
		

	void jump() {
		if (Input.GetButtonDown ("Jump") && isGrounded) {
			this.jumpActive = true;
		}

		if (this.jumpActive) {
			if (Input.GetButton ("Jump")) {
				this.jumpTime += Time.deltaTime;
				if (this.jumpTime < this.maxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.jumpActive = false;
				this.jumpTime = 0;
			}
		}
	}

	void jumpAnimation() {
		if (this.isGrounded) {
			animator.SetBool ("jump", false);
		} else {
			animator.SetBool ("jump", true);
		}
	}
}
