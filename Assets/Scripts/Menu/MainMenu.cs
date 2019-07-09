using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MainMenu : HoverCursor {
	public KeyCode		settingsKey = KeyCode.Escape;
	public GameObject	SettingsBg;

	public string volumeOnText = "on", volumeOffText = "off";
	public Sprite volume_on, volume_off;
	public Image volumeImg = null;
	public Text volumeText = null;
	[HideInInspector]

	void Awake () {
		if (volume_on == null)
			Debug.LogError("You need to set volume_on to MainMenu script !");
		if (volume_off == null)
			Debug.LogError("You need to set volume_off to MainMenu script !");
		if (volumeImg == null)
			Debug.LogError("You need to set volumeImg to MainMenu script !");
		if (volumeText == null)
			Debug.LogError("You need to set volumeText to MainMenu script !");

		volumeImg.sprite = AudioManager.instance.volumeOn ? volume_on : volume_off;
		volumeText.text = AudioManager.instance.volumeOn ? volumeOnText : volumeOffText;
	}

	void Update () {
		if (Input.GetKeyDown(settingsKey))
			OnSettings();
	}

	public void OnStoryMode () {
		GameManager.instance.testMode = false;
		SceneManager.LoadScene("StoryMode", LoadSceneMode.Single);
		GameManager.instance.SetCursor(CursorType.normal);
	}

	public void OnTestMode () {
		GameManager.instance.testMode = true;
		SceneManager.LoadScene("StoryMode", LoadSceneMode.Single);
		GameManager.instance.SetCursor(CursorType.normal);
	}

	public void OnSettings () {
		SettingsBg.SetActive(!SettingsBg.activeSelf);
	}

	public void OnSettingVolume () {
		AudioManager.instance.volumeOn = !AudioManager.instance.volumeOn;
		AudioManager.instance.toggleAudio(!AudioManager.instance.volumeOn);
		volumeImg.sprite = AudioManager.instance.volumeOn ? volume_on : volume_off;
		volumeText.text = AudioManager.instance.volumeOn ? volumeOnText : volumeOffText;
	}
}
