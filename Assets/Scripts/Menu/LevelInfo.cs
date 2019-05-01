using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelInfo : HoverCursor {
	public string	sceneName;
	public Text		textComponent;
	public bool		done = false;

	void Awake () {
		Init();
	}

	public void Init () {
		textComponent.text = sceneName;
		GetComponent<Renderer>().material = done ? GameManager.instance.levelDoneMaterial : GameManager.instance.levelTodoMaterial;
	}

	public void OnClick () {
		// SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		if (done)
			Tools.SetMaterialAlpha(GetComponent<Renderer>(), 1);
		else
			Tools.SetMaterialAlpha(GetComponent<Renderer>(), .4f);
		done = !done;
	}

	void OnMouseDown () { OnClick(); }
	void OnMouseOver () { OnHover(); }
	void OnMouseExit () { OnHoverAway(); }
}
