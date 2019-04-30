using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	void Awake () {
		var levels = GameManager.instance.levels;
		var levelProgess = GameManager.instance.levelProgess;

		for (int i = 0; i < levels.Count; i++)
		{
			print(levels[i] + " => " + levelProgess[i]);
		}
	}
}
