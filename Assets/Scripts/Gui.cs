using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gui : MonoBehaviour {
	public Color coldColor = new Color(0.09f, 0.698f, 1); // #17B2FF
	public Color hotColor = Color.red;


	private RectTransform validPercRT;
	private Image validPercImg;
	void Awake () {
		Transform validPercBar = transform.Find("ValidPercBar").GetChild(0);
		validPercRT = validPercBar.GetComponent<RectTransform>();
		validPercImg = validPercBar.GetComponent<Image>();
	}

	void Update () {
		if (PuzzleManager.instance.finished)
			validPercRT.transform.parent.gameObject.SetActive(false);
		else {
			validPercRT.anchorMin = new Vector2(1 - PuzzleManager.instance.validPerc, 0);
			validPercImg.color = Color.Lerp(coldColor, hotColor, PuzzleManager.instance.validPerc);
		}
	}
}
