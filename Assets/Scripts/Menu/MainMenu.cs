﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MainMenu : HoverCursor {
	public void OnStoryMode () {
		SceneManager.LoadScene("StoryMode", LoadSceneMode.Single);
		GameManager.instance.SetCursor(CursorType.normal);
	}

	public void OnTestMode () {
		print("OnTestMode");
	}
}
