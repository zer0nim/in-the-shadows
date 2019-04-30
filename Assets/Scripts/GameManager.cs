using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : CursorManager {
	public static GameManager	instance = null;
	public Material 			levelDoneMaterial, levelTodoMaterial;
	// public List
	public string				levelScenePrefix = "Levels";
	[HideInInspector]
	public List<bool>			levelProgess;
	public int					currentLevel;
	public List<string> 		levels { get; private set; }
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		// Set this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		// Get levels scenes
		levels = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			string sceneName = Path.GetFileNameWithoutExtension(scene.path);

			if (scene.enabled && scene.path.Contains(levelScenePrefix))
				levels.Add(sceneName);
		}

		LoadSave();
	}

	// if there is a save file, load it, else, create new levelProgess
	void LoadSave () {
		if (File.Exists(Application.persistentDataPath + "/gamesave.save")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize(file);
			file.Close();
			levelProgess = save.levelProgess;
		} else {
			// init levelProgess
			levelProgess = Enumerable.Repeat(false, levels.Count).ToList();
		}
	}

	public void SaveGame () {
		Save save = new Save();
		save.levelProgess = levelProgess;

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();
	}
}
