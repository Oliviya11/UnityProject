using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class MovingPlatform : MonoBehaviour {

	public Vector3 moveBy;
	public float speed;
	public float time_to_wait;
	float changing_time;
	Vector3 pointA, pointB, my_pos, target;
	bool going_to_a;

	// Use this for initialization
	void Start () {
		pointA = this.transform.position;
		pointB = pointA + moveBy;
		my_pos = transform.position;
	    going_to_a = false;
		changing_time = time_to_wait;
		exchangeDirection ();
	}

	bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}

	void exchangeDirection() {
		if(going_to_a) {
			target = this.pointA;
			my_pos = this.pointB;
			going_to_a = false;
		} else {
			target = this.pointB;
			my_pos = this.pointA;
			going_to_a = true;
		}

	}

	void waitForAWhile() {
		changing_time -= Time.deltaTime;
	}


	void move() {
	//	if (changing_time <= 0) {
		if (isArrived (my_pos, target)) {
			changing_time = time_to_wait;
			exchangeDirection ();
		} else {
			/*
			Vector3 destination = target - my_pos;
				Debug.Log (target.y+", "+my_pos.y);
			destination.z = 0;
			float move = speed;
			float distance = Vector3.Distance (destination, my_pos);
			Vector3 moveVect = destination.normalized * Mathf.Min (move, distance);
			my_pos = transform.position += moveVect * Time.deltaTime;
            


			Vector3 destination = (target - my_pos)*speed;
			destination.z = 0;
			Vector3 finalVector = destination;
				//Vector3.Min (destination, moveVector);
		    transform.Translate (finalVector * Time.deltaTime);
			my_pos = transform.position;
            
           
		}
	//	} else {
		//	waitForAWhile ();
	//	}

	}

	
	
	// Update is called once per frame
	void FixedUpdate () {
		move ();
		//5.6
	}
}
*/




public class MovingPlatform : MonoBehaviour {

	public Vector3 moveBy;
	public float speed;
	public float time_to_wait;
	float changing_time;
	Vector3 pointA, pointB, my_pos, target;
	bool going_to_a;

	// Use this for initialization
	void Start () {
		pointA = this.transform.position;
		pointB = pointA + moveBy;
		my_pos = transform.position;
	    going_to_a = true;
		changing_time = time_to_wait;
		exchangeDirection ();
	}

	bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}

	void exchangeDirection() {
		if(going_to_a) {
			target = this.pointA;
			my_pos = this.pointB;
			going_to_a = false;
		} else {
			target = this.pointB;
			my_pos = this.pointA;
			going_to_a = true;
		}

	}

	void waitForAWhile() {
		changing_time -= Time.deltaTime;
	}


	void move() {
		if (changing_time <= 0) {
			if (isArrived (my_pos, target)) {
				changing_time = time_to_wait;
				exchangeDirection ();
			} else {
				Vector3 destination = (target - my_pos);
				destination.z = 0;
				Vector3 finalVector = destination.normalized*speed;
				transform.Translate (finalVector * Time.deltaTime);
				my_pos = transform.position;
			}
		} else {
			waitForAWhile ();
		}
	}
		
		
	// Update is called once per frame
	void FixedUpdate () {
		move();
	}
}
