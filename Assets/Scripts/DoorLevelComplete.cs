using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevelComplete : MonoBehaviour {
	public GameObject winPrefab;
	public static float time;
	// Use this for initialization
	void Start () {
		time = Time.timeScale;
	}
		

	public void showWinPanel() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//	GameObject parent = UICamera.first.transform.SetParent(gameObject);
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		obj.GetComponent<SettingsPanel>();
		//Time.timeScale = 0;
		//...
	}

}
