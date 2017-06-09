using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	public GameObject losePrefab;
	public int levelId;
	public static LevelController current = null;
	public int maxFruitsNumber;
	int fruitsNumber=0, coinsNumber = 0, lifesNumber=3, crystalsCounter=0;
	int curCrystalColor = -1;
	Vector3 startingPosition;
	static bool music=true, sound=true;
	List<int> fruits = new List<int>();
	List<int> crystals = new List<int>();
	void Awake() {
		current = this;
	}
	void Start() {
		setInfo ();

	}
		
	public int getMaxFruitsNumber() {
		return maxFruitsNumber;
	}
	public void setInfo() {
		string str = PlayerPrefs.GetString ("info"+levelId.ToString(), null);
		string str2 = PlayerPrefs.GetString ("MusicAndSound", null);
		LevelInfo info = JsonUtility.FromJson<LevelInfo> (str);
		MusicAndSound musicAndSound = JsonUtility.FromJson<MusicAndSound> (str2);
		if (info!=null) {
			fruits = info.collectedFruits;
			fruitsNumber = info.fruitsNumber;
		}
		if (musicAndSound != null) {
			music = musicAndSound.music;
			sound = musicAndSound.sound;
		}
	}
	public void addCrystal(int color) {
		crystals.Add (color);
	}
	public bool containsCrystal(int id) {
		return crystals.Contains (id);
	}
	public bool containFruit(int id) {
		return fruits.Contains (id);
	}

	public void putInList(int id) {
		if (!fruits.Contains(id))
		     fruits.Add (id);
	}

	public List<int> getFruits() {
		return fruits;
	}
	public void setStartPosition(Vector3 pos){
		this.startingPosition = pos;
	}
	public static void setMusic(bool val) {
		music = val;
	}
	public static void setSound(bool val) {
		sound = val;
	}

	public static bool getMusic() {
		return music;
	}
	public static bool getSound() {
		return sound;
	}

	public void onRabbitDeath(HeroRabbit rabbit){
		decreaseLifeNumber ();
		if (lifesNumber == 0) {
			saveInfo (false);
			setInfo ();
			StartCoroutine ( openLosePanel());
		}
		rabbit.transform.position = startingPosition;
		rabbit.alive ();
	}

	public IEnumerator openLosePanel() {
		yield return new WaitForSeconds (1f);
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//	GameObject parent = UICamera.first.transform.SetParent(gameObject);
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, losePrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		obj.GetComponent<SettingsPanel>();
	}
	public IEnumerator openChangingScene() {
		yield return new WaitForSeconds (1f);
			SceneManager.LoadScene ("ChangeLevel");
	}

	public int getFruitsNumber() {
		return fruitsNumber;
	}

	public int getCoinsNumber() {
		return coinsNumber;
	}

	public int getLifesNumber() {
		return lifesNumber;
	}

	void decreaseLifeNumber() {
		lifesNumber--;
	}

	public void increasCoins() {
		coinsNumber++;
	}
	public void increaseFruits() {
		fruitsNumber++;
	}

	public void setCurCrystalColor(int color) {
		curCrystalColor = color;
		crystalsCounter++;
	}

	public int getCurCrystalColor() {
		return curCrystalColor;
	}

	public int getCrystalsNumber() {
		return crystalsCounter;
	}


		

	void modifyLevelInfo(bool val) {
		LevelInfo newinfo = new LevelInfo ();
		Fruit.setCounterToZero ();
		if (val) {
			int coins = PlayerPrefs.GetInt ("coins", 0);
			PlayerPrefs.SetInt ("coins", coinsNumber+coins);

			if (LevelController.current.getCrystalsNumber () == 3) {
				Debug.Log ("allCrystals");
				newinfo.hasAllCrystals = true;
			}
			if (LevelController.current.getFruitsNumber () == maxFruitsNumber) {
				newinfo.hasAllFruits = true;
			}
			newinfo.collectedFruits = getFruits ();
			newinfo.fruitsNumber = fruitsNumber;
		} 
		MusicAndSound newMusicAndSound = new MusicAndSound ();
		newMusicAndSound.sound = sound;
//		Debug.Log (music);
		newMusicAndSound.music = music;
		string str = JsonUtility.ToJson (newinfo);
		PlayerPrefs.SetString ("info"+levelId.ToString(), str);
		string str2 = JsonUtility.ToJson (newMusicAndSound);
		PlayerPrefs.SetString ("MusicAndSound", str2);
		PlayerPrefs.Save ();
	}
		
	public void saveInfo(bool val) {
		modifyLevelInfo (val);
	}

	public IEnumerator openLevel() {
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene ("Level" + levelId.ToString ());
	}
	public IEnumerator openMenu() {
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene ("Menu");
	}
}
