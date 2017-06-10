using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBtn : MonoBehaviour {
	//public MyButton settingsBtn;
	public GameObject settingsPrefab;
	public static float time;

	static AudioSource source;
	public  AudioClip sound;

	// Use this for initialization
	void Start () {
		source = gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		time = Time.timeScale;
		this.GetComponent<MyButton>().signalOnClick.AddListener (this.showSettings);
	}
	
	void OnSettingsBtn() {
		showSettings ();
	}

	void showSettings() {
		
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		obj.GetComponent<SettingsPanel>();


		Time.timeScale = 0;
		//...
	}

	public static void playSoundOnClosingSettingsPanel() {
		if (LevelController.getSound())
		    source.Play ();
	}
}
