using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MainMenu : MonoBehaviour {

	public void OnStoryMode () {
		SceneManager.LoadScene("StoryMode", LoadSceneMode.Single);
	}

	public void OnTestMode () {
		print("OnTestMode");
	}

	public void OnHover () {
		GameManager.instance.SetCursor(CursorType.link);
	}

	public void OnHoverAway () {
		GameManager.instance.SetCursor(CursorType.normal);
	}
}
