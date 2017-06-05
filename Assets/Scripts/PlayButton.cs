using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayButton : MonoBehaviour {

	public UnityEvent signalOnClick = new UnityEvent();


	public void _onClick() {
		this.signalOnClick.Invoke ();
	}
}
