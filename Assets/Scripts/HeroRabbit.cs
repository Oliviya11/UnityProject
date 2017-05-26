using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	Rigidbody2D myBody = null;
	SpriteRenderer sr = null;
	Animator animator = null;
	Renderer rend = null;
	bool isGrounded = false;
	bool jumpActive = false;
	float jumpTime = 0f;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;
	byte health;
	Transform heroParent = null;
	public float myTime;
    bool increase = false, decrease = false, 
	red = false,  shield = false, firstBomb = true, side;
	float sizeTimes = 1.5f;
	float maxX;
	float dieTime = 2f;
	float curDieTime;
    float redTime = 4f;
	float curRedTime;
	Vector3 normalSize;

	// Use this for initialization
	void Start () {
		normalSize = transform.localScale;
		curRedTime = redTime;
		curDieTime = dieTime;
		health = 1;
		rend = GetComponent<Renderer> ();
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelController.current.setStartPosition (transform.position);
		animator = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		heroParent = transform.parent;
		side = sr.flipX;

	}

	// Update is called once per frame
	void Update () {

	}
	/**
	 * It's bonus. It works AFTER rabbit grew up, NOT in
	 * the process of growing. Rabbit recieves ability to
	 * go through bombs for some time (nearly 4 seconds).
	 * */
	void reddishRabbit() {
		if ((curRedTime -= Time.deltaTime) >= 0 && red) {
			rend.material.SetColor ("_Color", Color.red);
		} else {
			whiten ();
			changeInRed ();
			decreaseHealth ();
		}
	}
	void whiten() {
		rend.material.SetColor ("_Color", Color.white);
	}
	void changeInRed() {
		red = false;
		curRedTime = redTime;
		shield = false;
		firstBomb = true;
	}

	void FixedUpdate () {
		if (health != 0) {
			float value = Input.GetAxis ("Horizontal");
			run (value);
			flipPicture (value);
			runAnimation (value);	
			checkIfIsGrounded ();
			jump ();
			jumpAnimation ();
			if (increase)
				becomeHealthier ();
			if (decrease)
				becomeIllOrDie ();
			if (red)
				reddishRabbit ();
			
		} else {
			
		    dieAnimation ();
			if ((curDieTime -= Time.deltaTime) < 0) {
				LevelController.current.onRabbitDeath (this);
			}

		}
	}

   public void alive() {
		whiten ();
		animator.SetBool ("die", false);
		health = 1;
		changeInRed ();
		curDieTime = dieTime;
		transform.localScale = normalSize;
		sr.flipX = side;
	}
	void dieAnimation() {
		animator.SetBool ("die", true);
		animator.SetBool ("run", false);
		animator.SetBool ("jump", false);
		myBody.velocity = new Vector2 (0, -speed);
	}
	void SetNewParent(Transform obj, Transform newObject){
		obj.transform.parent = newObject;
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
	  } else if (value > 0) {
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
			SetNewParent (this.transform, hit.transform);
		} else {
			isGrounded = false;
			SetNewParent (this.transform, heroParent);
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

	void changeSize(float times) {
		 Vector3 scale_speed = Vector3.zero;
		 Vector3 targetScale = new Vector3 (transform.localScale.x * times, transform.localScale.y * times, transform.localScale.z);
		 transform.localScale = Vector3.SmoothDamp (transform.localScale, targetScale, ref scale_speed, myTime);

	}
		

	public void increaseHealth() {
		if (health < 2) health++;
	}

	public void decreaseHealth() {
		if (!shield && health > 0) health--;
	}



    void becomeHealthier() {
		shield = true;
		if (transform.localScale.x < Vector3.one.x * sizeTimes) {
			changeSize (sizeTimes);

		} else {
			setIncrease (false);
		}

	}

    void becomeIllOrDie() {
		
			if (transform.localScale.x > Vector3.one.x) {
				changeSize (1 / sizeTimes);
				red = true;
			    firstBomb = false;
			} else {
				setDecrease (false);
			}

	}

	public void setIncrease(bool val){
		increase = val;
	}
	public void setDecrease(bool val) {
		decrease = val;
	}
	public bool hasShield() {
		return shield;
	}
	public bool getFirstBomb() {
		return firstBomb;
	}
	public byte getHealth() {
		return health;
	}
}
