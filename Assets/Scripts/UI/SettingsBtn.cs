using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBtn : MonoBehaviour {
	public MyButton settingsBtn;
	public GameObject settingsPrefab;
	public static float time;
	// Use this for initialization
	void Start () {
		time = Time.timeScale;
		settingsBtn.signalOnClick.AddListener (this.OnSettingsBtn);
	}
	
	void OnSettingsBtn() {
		showSettings ();
	}

	void showSettings() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
	//	GameObject parent = UICamera.first.transform.SetParent(gameObject);
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		obj.GetComponent<SettingsPanel>();
		Time.timeScale = 0;
		//...
	}
}
