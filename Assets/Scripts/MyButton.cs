using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class MyButton : MonoBehaviour {
	
	public UnityEvent signalOnClick = new UnityEvent();
	void Strat() {
		
	}
	public void _onClick() {
		signalOnClick.Invoke ();
	}

		


}
