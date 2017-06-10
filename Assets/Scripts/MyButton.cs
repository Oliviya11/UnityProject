using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class MyButton : MonoBehaviour {
	AudioSource source;
	public AudioClip sound;

	public UnityEvent signalOnClick = new UnityEvent();
	void Awake() {
		source = gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
	}
	void Strat() {
		
	}
	public void _onClick() {
		if (LevelController.getSound ()) {
			source.Play ();
		}
		signalOnClick.Invoke ();
	}

		


}
