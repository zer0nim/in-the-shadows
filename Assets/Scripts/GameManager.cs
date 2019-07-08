using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : CursorManager {
	public static GameManager	instance = null;
	public Material 			levelDoneMaterial, levelTodoMaterial;
	// public List
	public string				levelScenePrefix = "Levels";
	[HideInInspector]
	public Save					save = new Save();
	[HideInInspector]
	public string				lastLoadedScene;
	public bool					testMode = false;

	public List<string> 		levels { get; private set; }
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		// Set this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		lastLoadedScene = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(0));

		// Get levels scenes
		levels = new List<string>();
		int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
		for (int i = 0; i < sceneCount; i++)
		{
			string path = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
			string sceneName = Path.GetFileNameWithoutExtension(path);
			if (path.Contains(levelScenePrefix))
				levels.Add(sceneName);
		}

		LoadSave();
	}

	// if there is a save file, load it, else, create new levelProgess
	void LoadSave () {
		if (File.Exists(Application.persistentDataPath + "/gamesave.save")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			save = (Save)bf.Deserialize(file);
			file.Close();
		} else
			initProgress();
	}

	public void initProgress() {
		save.levelProgess = Enumerable.Repeat(LevelStatus.Locked, levels.Count).ToList();
		save.levelProgess[0] = LevelStatus.Unlocked;
		save.animationDone = Enumerable.Repeat(true, levels.Count).ToList();
	}

	public void SaveGame () {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();
	}

	// Save progess when new level is done
	// Return next level name or null if this is the last one
	public string LevelDone (string name) {
		int i = GameManager.instance.levels.FindIndex(x => x == name);
		GameManager.instance.save.animationDone[i] = false;
		GameManager.instance.save.levelProgess[i] = LevelStatus.Done;
		// if there is a next level
		if (i + 1 < GameManager.instance.levels.Count) {
			GameManager.instance.save.animationDone[i + 1] = false;
			GameManager.instance.save.levelProgess[i + 1] = LevelStatus.Unlocked;
			SaveGame();
			return (GameManager.instance.levels[i + 1]);
		}
		SaveGame();
		return null;
	}
}
