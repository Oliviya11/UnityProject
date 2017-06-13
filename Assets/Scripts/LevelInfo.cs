using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo {
	public bool hasAllCrystals = false;
	public bool hasAllFruits = false;
	public List<int> collectedFruits;
	public int fruitsNumber;
	public bool passLevel;
}
