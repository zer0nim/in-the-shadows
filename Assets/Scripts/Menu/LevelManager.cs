﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public GameObject	levelBox;
	public float		padding = .5f;
	[Range(0, .5f)]
	public float		scrollWidth = .2f;
	public float		minScrollSpeed = 2;
	public float		maxScrollSpeed = 8;
	public KeyCode		mainMenuKey = KeyCode.Escape;
	public GameObject	deleteProgress = null;

	private float xVel = 0;
	void Awake () {
		if (deleteProgress == null)
			Debug.LogError("You need to set deleteProgress to LevelManager script !");

		var levels = GameManager.instance.levels;

		if (!GameManager.instance.testMode) {
			bool playSuccess = false;
			Vector3 pos = Vector3.zero;
			for (int i = 0; i < levels.Count; i++)
			{
				GameObject levelBoxInst = Instantiate(levelBox, pos, Quaternion.identity);
				pos.Set(pos.x + 1 + padding, pos.y, pos.z);

				LevelInfo levelInfo = levelBoxInst.GetComponent<LevelInfo>();
				levelInfo.id = i;
				levelInfo.sceneName = levels[i];
				levelInfo.status = GameManager.instance.save.levelProgess[i];
				levelInfo.animationDone = GameManager.instance.save.animationDone[i];
				if (!GameManager.instance.save.animationDone[i])
					playSuccess = true;

				GameManager.instance.save.animationDone[i] = true;
				levelInfo.Init();

				levelBoxInst.transform.parent = transform;
			}

			if (playSuccess)
				AudioManager.instance.Play("unlock");

			GameManager.instance.SaveGame();
		} else {
			deleteProgress.SetActive(false);

			Vector3 pos = Vector3.zero;
			for (int i = 0; i < levels.Count; i++)
			{
				GameObject levelBoxInst = Instantiate(levelBox, pos, Quaternion.identity);
				pos.Set(pos.x + 1 + padding, pos.y, pos.z);

				LevelInfo levelInfo = levelBoxInst.GetComponent<LevelInfo>();
				levelInfo.sceneName = levels[i];
				levelInfo.status = LevelStatus.Unlocked;
				levelInfo.animationDone = true;
				levelInfo.Init();

				levelBoxInst.transform.parent = transform;
			}
		}
	}

	void Update () {
		menuNavigation();
		if (Input.GetKeyDown(mainMenuKey))
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	void menuNavigation () {
		float mousePerc = Input.mousePosition.x / Screen.width;
		mousePerc = mousePerc < 0 ? 0 : (mousePerc > 1 ? 1 : mousePerc);

		xVel = 0;
		if (mousePerc < scrollWidth)
			xVel = -(1 - mousePerc / scrollWidth);
		else if (mousePerc > 1 - scrollWidth)
			xVel = 1 - (1 - mousePerc) / scrollWidth;

		if (xVel < 0 && transform.GetChild(0).transform.position.x < 0) {
			float moveX = Mathf.Lerp(minScrollSpeed, maxScrollSpeed, Mathf.Abs(xVel));
			transform.position = new Vector3(transform.position.x + moveX * Time.deltaTime, transform.position.y, transform.position.z);
		}

		if (xVel > 0 && transform.GetChild(transform.childCount - 1).transform.position.x > 0) {
			float moveX = - Mathf.Lerp(minScrollSpeed, maxScrollSpeed, Mathf.Abs(xVel));
			transform.position = new Vector3(transform.position.x + moveX * Time.deltaTime, transform.position.y, transform.position.z);
		}
	}
}
