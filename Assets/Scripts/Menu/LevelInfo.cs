using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum LevelStatus { Locked, Unlocked, Done }

public class LevelInfo : HoverCursor {
	public string	sceneName;
	public Text		textComponent;
	public LevelStatus	status = LevelStatus.Locked;

	void Awake () {
		Init();
	}

	public void Init () {
		textComponent.text = sceneName;
		GetComponent<Renderer>().material = status == LevelStatus.Done ? GameManager.instance.levelDoneMaterial : GameManager.instance.levelTodoMaterial;
        if (status == LevelStatus.Locked)
            Tools.SetMaterialAlpha(GetComponent<Renderer>(), .4f);
	}

	public void OnClick () {
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	void OnMouseDown () { OnClick(); }
	void OnMouseOver () { OnHover(); }
	void OnMouseExit () { OnHoverAway(); }
}
