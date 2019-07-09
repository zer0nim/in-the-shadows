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
	public bool volumeOn = true;

	void Awake () {
		if (volumeImg == null)
			Debug.LogError("You need to set volume_on to MainMenu script !");
		if (volumeImg == null)
			Debug.LogError("You need to set volume_off to MainMenu script !");
		if (volumeImg == null)
			Debug.LogError("You need to set volumeImg to MainMenu script !");
		if (volumeText == null)
			Debug.LogError("You need to set volumeText to MainMenu script !");

		volumeImg.sprite = volumeOn ? volume_on : volume_off;
		volumeText.text = volumeOn ? volumeOnText : volumeOffText;

		AudioManager.instance.toggleAudio(!volumeOn);
	}

	void Update () {
		if (Input.GetKeyDown(settingsKey)) {
			// Toggle settings
			SettingsBg.SetActive(!SettingsBg.active);
		}
	}

	public void OnStoryMode () {
		SceneManager.LoadScene("StoryMode", LoadSceneMode.Single);
		GameManager.instance.SetCursor(CursorType.normal);
	}

	public void OnTestMode () {
		print("OnTestMode");
	}

	public void OnSettingVolume () {
		volumeOn = !volumeOn;
		AudioManager.instance.toggleAudio(!volumeOn);
		volumeImg.sprite = volumeOn ? volume_on : volume_off;
		volumeText.text = volumeOn ? volumeOnText : volumeOffText;
	}
}
