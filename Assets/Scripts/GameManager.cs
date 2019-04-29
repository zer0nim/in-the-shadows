using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CursorManager {
	public static GameManager instance = null;
	public Material levelDoneMaterial, levelTodoMaterial;
	public List<GameObject> levels;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		// Set this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		SetCursor(CursorType.normal);
	}

}
