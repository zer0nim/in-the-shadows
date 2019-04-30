using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject	levelBox;
	public float		padding = .5f;
	void Awake () {
		var levels = GameManager.instance.levels;
		var levelProgess = GameManager.instance.levelProgess;

		Vector3 pos = Vector3.zero;
		for (int i = 0; i < levels.Count; i++)
		{
			GameObject levelBoxInst = Instantiate(levelBox, pos, Quaternion.identity);
			pos.Set(pos.x + 1 + padding, pos.y, pos.z);

			LevelInfo levelInfo = levelBoxInst.GetComponent<LevelInfo>();
			levelInfo.sceneName = levels[i];
			levelInfo.done = levelProgess[i];
			levelInfo.Init();
		}
	}
}
