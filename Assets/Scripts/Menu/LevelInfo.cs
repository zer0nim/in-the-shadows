using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour {
	public string	display_name;
	public string	l_name;
	public Text		textComponent;
	public bool		done = false;

	void Awake () {
		textComponent.text = display_name;
		GetComponent<Renderer>().material = done ? GameManager.instance.levelDoneMaterial : GameManager.instance.levelTodoMaterial;
	}
}
