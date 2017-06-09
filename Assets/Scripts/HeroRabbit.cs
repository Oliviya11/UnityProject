using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroRabbit : MonoBehaviour {
	public bool canMove;
	public AudioClip runSound = null;
	public AudioClip coinSound = null;
	public AudioClip crystalSound = null;
	public AudioClip fruitSound = null;
	public AudioClip bombSound = null;
	public AudioClip mushroomSound = null;
	public AudioClip dieSound = null;
	public AudioClip groundSound = null;
	public AudioClip attackSound = null;


	AudioSource runSource = null;
	AudioSource coinSource = null;
	AudioSource crystalSource = null;
	AudioSource fruitSource = null;
	AudioSource bombSource = null;
	AudioSource mushroomSource = null;
	AudioSource dieSource = null;
	AudioSource groundSource = null;
	AudioSource attackSource = null;
	//AudioSource backgroundSource = null;

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
	public float scaleTime;
    bool increase = false, decrease = false, 
	red = false,  shield = false, firstBomb = true, 
	side, fly = false;
	float sizeTimes = 1.5f;
	float maxX;
	public float dieTime;
	float curDieTime;
    float redTime = 4f;
	float curRedTime;
	Vector3 normalSize, myPos, myPosBeforeJump;
	bool soundJump = false;
	//public Collider2D triggerBody;

	public static HeroRabbit rabbit_copy;

	// Use this for initialization
	void Start () {
		muteOrActiveBackgroundMusic ();
		if (canMove) {
			initSoundSources ();
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
			rabbit_copy = this;
		}
	}

	void initSoundSources() {
		runSource = gameObject.AddComponent<AudioSource> ();
		coinSource = gameObject.AddComponent<AudioSource> ();
		crystalSource = gameObject.AddComponent<AudioSource> ();
		fruitSource = gameObject.AddComponent<AudioSource> ();
		bombSource = gameObject.AddComponent<AudioSource> ();
		mushroomSource = gameObject.AddComponent<AudioSource> ();
		dieSource = gameObject.AddComponent<AudioSource> ();
		groundSource = gameObject.AddComponent<AudioSource> ();
		attackSource = gameObject.AddComponent<AudioSource> ();

		   runSource.clip = runSound;
		   coinSource.clip = coinSound;
		   crystalSource.clip = crystalSound;
		   fruitSource.clip = fruitSound;
		   bombSource.clip = bombSound;
		   mushroomSource.clip = mushroomSound;
		   dieSource.clip = dieSound;
		   groundSource.clip = groundSound;
		   attackSource.clip = attackSound;
	}

	public void playMusicOnCoin() {
		if (LevelController.getSound()) 
		coinSource.Play ();
	}
	public void playMusicOnFruit() {
		if (LevelController.getSound()) 
		fruitSource.Play ();
	}

	public void playMusicOnCrystal() {
		if (LevelController.getSound()) 
		  crystalSource.Play ();
	}

	public void playMusicOnBomb() {
		if (LevelController.getSound()) 
	     	bombSource.Play ();
	}

	public void playMusicOnMushroom() {
		if (LevelController.getSound()) 
		  mushroomSource.Play ();
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
		if (canMove) {
			myPos = this.transform.position;
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
				if (fly)
					flyUp ();
			
			} else {
				muteMusicOnRun ();
				dieAnimation ();
				if ((curDieTime -= Time.deltaTime) < 0) {
					LevelController.current.onRabbitDeath (this);
				}

			}
		}	
	}

	public void muteOrActiveBackgroundMusic() {
	//	Debug.Log ("music form hero rabbit: "+LevelController.getMusic ());
		if (!LevelController.getMusic ()) {
			this.GetComponent<AudioSource> ().Stop();
		} else if (!this.GetComponent<AudioSource> ().isPlaying) {
			this.GetComponent<AudioSource> ().Play();
		}
	}

	public void playMusicOnDeath() {
		if (LevelController.getSound())
		dieSource.Play ();
	}

   public void alive() {
		whiten ();
		animator.SetBool ("die", false);
		changeInRed ();
		curDieTime = dieTime;
		transform.localScale = normalSize;
		sr.flipX = side;
		setDecrease (false);
		setIncrease (false);
		myBody.isKinematic = false;
		health = 1;
	//	fly = false;
	}
	void dieAnimation() {
		animator.SetBool ("die", true);
		animator.SetBool ("run", false);
		animator.SetBool ("jump", false);
		//myBody.velocity = new Vector2 (0, -speed);
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
			if (!runSource.isPlaying && LevelController.getSound() )
			     runSource.Play ();
		} else {
			muteMusicOnRun ();
		}
	}

	public void muteMusicOnRun() {
		if (runSource!=null) runSource.Stop ();
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
			if (LevelController.getSound () && isGrounded && soundJump) {
				groundSource.Play ();
				isGrounded = false;
				soundJump = false;
			}
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
			muteMusicOnRun ();
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
				    soundJump = true;
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
	//	Debug.Log (Time.deltaTime);
		transform.localScale = Vector3.SmoothDamp (transform.localScale, targetScale, ref scale_speed, scaleTime*Time.deltaTime);

	}
		

	public void increaseHealth() {
		if (health < 2) health++;
	}

	public void decreaseHealth() {
		if (!shield && health > 0) {
			health--;
		}
		if (health==0 && LevelController.getSound() ) playMusicOnDeath ();
	}



    void becomeHealthier() {
		
		if (transform.localScale.x < Vector3.one.x * sizeTimes) {
			changeSize (sizeTimes);

		} else {

			shield = true;
			setIncrease (false);
		}

	}

    void becomeIllOrDie() {
		
			if (transform.localScale.x > Vector3.one.x) {
				changeSize (1 / sizeTimes);
			   if (transform.localScale.x < normalSize.x * 2)
				    decreaseHealth ();
			   if (health!=0) red = true;
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

	void OnTriggerEnter2D(Collider2D collider) {
		interactWithOrgs (collider);
		interactWithDoors (collider);
		interactWithDoorLevelComplete (collider);
	}

	void interactWithOrgs(Collider2D collider) {
		Org org = collider.GetComponent<Org> ();
		if (health != 0 &&  org!= null && !org.isDead ()) {
			if (org != null && org.head == collider) {
				myPosBeforeJump = this.transform.position;
				fly = true;
				if (LevelController.getSound()) 
				   attackSource.Play ();
				org.die ();
				StartCoroutine (org.playMusicOnDeth ());

			} else if (org != null && org.body == collider) {
				if (LevelController.getSound()) 
				  org.playMusicOnAttack ();
				StartCoroutine (waitForRabbitDeath (org));

			}
		}
	}


	void interactWithDoors(Collider2D collider) {
		Door door = collider.GetComponent<Door> ();
		if (door != null) {
			StartCoroutine(openLevel (door.level));
		}
	}
		
	IEnumerator openLevel(int level) {
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("Level" + level.ToString ());
	}

	IEnumerator waitForRabbitDeath(Org org){
		org.attack ();
		if (health == 2) {
			shield = false;
			decreaseHealth ();
		}
	    decreaseHealth ();
		yield return new WaitForSeconds (dieTime);
		org.setUsualBehavior ();
	}
	public void dieEffect() {
		myBody.isKinematic = true;
		becomeTransparent ();
	}

	void becomeTransparent() {
		Color c = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		rend.material.SetColor ("_Color", c);
	}

	void flyUp() {
		Vector2 vel = myBody.velocity;
		if (myPos.y - myPosBeforeJump.y > 1f) {
			fly = false;
		}
		else {
			vel.y = jumpSpeed*1.2f;
		}
		myBody.velocity = vel;
	}

	void interactWithDoorLevelComplete(Collider2D collider) {
		DoorLevelComplete door = collider.GetComponent<DoorLevelComplete> ();
		if (door != null) {
		    StartCoroutine(openDoor(door));
		}
	}

	IEnumerator openDoor(DoorLevelComplete door) {
		LevelController.current.saveInfo (true);
	//	LevelController.current.setInfo ();
		yield return new WaitForSeconds (1f);
		door.showWinPanel ();
	}


}
