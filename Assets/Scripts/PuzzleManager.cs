using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class PuzzleManager : MonoBehaviour {
	public static PuzzleManager instance = null;
	public List<Part> parts;
	public float minValidPerc = .95f;
	public float validAnimSpeed = 10;

	[HideInInspector]
	public bool finished { get ; private set ;}
	[HideInInspector]
	public float validPercRot;
	[HideInInspector]
	public float validPercPos;
	[HideInInspector]
	public bool allFinished { get ; private set ;}
	[HideInInspector]
	public string nextLvl { get ; private set ;}
	[HideInInspector]
	public bool difficulty3 = false;
	private List<float> partsDiffAngles;
	private List<float> partsPercPos;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		finished = false;
		allFinished = false;
		partsDiffAngles = Enumerable.Repeat(180f, parts.Count).ToList();
		partsPercPos = Enumerable.Repeat(1f, parts.Count).ToList();


		for (int i = 0; i < parts.Count; ++i)
			if (parts[i].difficulty >= 3)
				difficulty3 = true;
	}

	void Update () {
		if (!finished) {
			// Store angle betwwen rotations quaternion
			for (int i = 0; i < parts.Count; ++i) {
				// Calculate and save valid diff angle
				partsDiffAngles[i] = Quaternion.Angle(
				  parts[i].gameObject.transform.rotation, Quaternion.Euler(parts[i].validRot));

				if (parts[i].difficulty >= 3) {
					difficulty3 = true;

					// Calculate and save valid diff position
					Vector3 testPos = Camera.main.WorldToScreenPoint(parts[i].gameObject.transform.position);
					testPos = new Vector3(testPos.x - (Screen.width / 2), testPos.y - (Screen.height / 2), testPos.z);
					Vector3 validPos = parts[i].validPos.localPosition;

					float widthPerc = 1;
					float heightPerc = 1;

					if (parts[i].moveHori && parts[i].moveVert) {
						widthPerc = 1 - ((Math.Max(testPos.x, validPos.x) - Math.Min(testPos.x, validPos.x))
							/ Screen.width);
						heightPerc = 1 - ((Math.Max(testPos.y, validPos.y) - Math.Min(testPos.y, validPos.y))
							/ Screen.height);
					} else if (parts[i].moveHori && !parts[i].moveVert) {
						widthPerc = 1 - ((Math.Max(testPos.x, validPos.x) - Math.Min(testPos.x, validPos.x))
							/ Screen.width);
					}  else if (!parts[i].moveHori && parts[i].moveVert) {
						heightPerc = 1 - ((Math.Max(testPos.y, validPos.y) - Math.Min(testPos.y, validPos.y))
							/ Screen.height);
					}

					partsPercPos[i] = Math.Min(widthPerc, heightPerc);
				}
			}

			validPercRot = 1 - partsDiffAngles.Max() / 180;
			if (difficulty3)
				validPercPos = partsPercPos.Min();

			if (validPercRot > minValidPerc
			 && (!difficulty3 || validPercPos > minValidPerc)) {
				finished = true;
				if (!GameManager.instance.testMode) {
					// Save progess
					nextLvl = GameManager.instance.LevelDone(SceneManager.GetActiveScene().name);
				}
			}
		} else if (!allFinished) {
			allFinished = true;
			foreach (Part part in parts)
				if (!part.winAnimFinished)
					allFinished = false;
			if (allFinished) {
				Gui.instance.setMenu(true);
				AudioManager.instance.Play("success");
			}
		}
	}
}
