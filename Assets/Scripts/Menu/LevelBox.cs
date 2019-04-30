using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBox : MonoBehaviour {
	public string	displayName;
	public string	lName;
	public Text		textComponent;
	public bool		done = false;

	void Awake () {
		textComponent.text = displayName;
		GetComponent<Renderer>().material = done ? GameManager.instance.levelDoneMaterial : GameManager.instance.levelTodoMaterial;
	}
}
