using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gui : HoverCursor {
	public static Gui instance = null;
	public Color coldColor = new Color(0.09f, 0.698f, 1); // #17B2FF
	public Color hotColor = Color.red;
	public Transform validPercBar;
	public GameObject pausePanel;
	public KeyCode pauseKey = KeyCode.Escape;
	public Text title;
	public Text quitNextButton;
	public string winTitle = "Victory !";
	public string normalTitle = "Pause";
	public string quitText = "Quit";
	public string nextText = "Next";
	public bool pauseOpened = false;

	private RectTransform validPercRT;
	private Image validPercImg;
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

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

		if (Input.GetKeyDown(pauseKey)) {
			setMenu(!pauseOpened);
		}
	}

	public void setMenu (bool open) {
		title.text = PuzzleManager.instance.finished ? winTitle : normalTitle;
		quitNextButton.text = PuzzleManager.instance.finished ? nextText : quitText;
		pauseOpened = open;
		pausePanel.SetActive(pauseOpened);
	}

	public void OnReplayButton () {
		print("onReloadButton");
	}
	public void onQuitNextButton () {
		print("onQuitNextButton");
		if (PuzzleManager.instance.finished)
			onNext();
		else
			onQuit();
	}
	void onNext () {
		print("onNext");
	}
	void onQuit () {
		print("onQuit");
	}
}
