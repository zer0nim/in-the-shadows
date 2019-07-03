using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum LevelStatus { Locked, Unlocked, Done }

public class LevelInfo : HoverCursor, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
	public string		sceneName;
	public Text			textComponent;
	public Image		textFillBox;
	public LevelStatus	status = LevelStatus.Locked;
	public float		lockedAlpha = .4f;
	public Animator		animator;
	public bool			animationDone = false;
	void Awake () {
		Init();
	}

	public void Init () {
		textComponent.text = sceneName;
		SetMaterial(status == LevelStatus.Done && animationDone ? 1 : 0);
		active = status != LevelStatus.Locked;
		Tools.SetMaterialAlpha(GetComponent<Renderer>(), active ? 1 : lockedAlpha);

		if (!animationDone) {
			if (status == LevelStatus.Unlocked)
				animator.SetTrigger("Unlock");
			else if (status == LevelStatus.Done) {
				animator.SetBool("Unlocked", active);
				animator.SetTrigger("Done");
			}
		} else
			animator.SetBool("Unlocked", active);
	}

	// Used to set "Unlocked" from animation event, int used because bool is not suported...
	public void SetUnlocked(int active) {
		animator.SetBool("Unlocked", active == 1);
	}
	// Used to set material alpha from animation event
	public void SetMaterialAlpha(float alpha) {
		Tools.SetMaterialAlpha(GetComponent<Renderer>(), alpha);
	}
	// Used to set material alpha from animation event, int used because bool is not suported...
	public void SetMaterial(int done) {
		GetComponent<Renderer>().material = done == 1 ? GameManager.instance.levelDoneMaterial : GameManager.instance.levelTodoMaterial;
	}

	public void OnPointerClick (PointerEventData data) {
		if (active) {
			GameManager.instance.lastLoadedScene = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		}
	}

	public void OnTitleClick () { OnPointerClick(null); }
	public void OnPointerEnter(PointerEventData pointerEventData) {
		OnHover();
	}
	public void OnPointerExit(PointerEventData pointerEventData) {
		OnHoverAway();
	}
}
