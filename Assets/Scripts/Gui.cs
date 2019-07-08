using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gui : HoverCursor {
	public static Gui instance = null;
	public Color coldColor = new Color(0.09f, 0.698f, 1); // #17B2FF
	public Color hotColor = Color.red;
	public Transform validRotBar;
	public Transform validPosBar;
	public GameObject pausePanel;
	public KeyCode pauseKey = KeyCode.Escape;
	public Text title;
	public Text quitNextButton;
	public string winTitle = "Victory !";
	public string normalTitle = "Pause";
	public string quitText = "Quit";
	public string nextText = "Next";
	public bool pauseOpened = false;

	private RectTransform validRotRT, validPosRT;
	private Image validRotImg, validPosImg;
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		validRotRT = validRotBar.GetComponent<RectTransform>();
		validRotImg = validRotBar.GetComponent<Image>();

		validPosRT = validPosBar.GetComponent<RectTransform>();
		validPosImg = validPosBar.GetComponent<Image>();
	}

	void Start () {
		if (!PuzzleManager.instance.difficulty3)
			validPosRT.transform.parent.gameObject.SetActive(false);
	}

	void Update () {
		if (PuzzleManager.instance.finished) {
			validRotRT.transform.parent.gameObject.SetActive(false);
			validPosRT.transform.parent.gameObject.SetActive(false);
		} else {
			// update rotation percent bar
			validRotRT.anchorMin = new Vector2(1 - PuzzleManager.instance.validPercRot, 0);
			validRotImg.color = Color.Lerp(coldColor, hotColor, PuzzleManager.instance.validPercRot);

			// update position percent bar
			if (PuzzleManager.instance.difficulty3) {
				validPosRT.anchorMin = new Vector2(1 - PuzzleManager.instance.validPercPos, 0);
				validPosImg.color = Color.Lerp(coldColor, hotColor, PuzzleManager.instance.validPercPos);
			}
		}

		if (Input.GetKeyDown(pauseKey)) {
			setMenu(!pauseOpened);
		}
	}

	public void setMenu (bool open) {
		title.text = PuzzleManager.instance.finished ? winTitle : normalTitle;
		bool isNext = PuzzleManager.instance.finished && PuzzleManager.instance.nextLvl != null;
		quitNextButton.text = isNext ? nextText : quitText;
		pauseOpened = open;
		pausePanel.SetActive(pauseOpened);
	}

	public void OnReplayButton () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void onQuitNextButton () {
		bool isNext = PuzzleManager.instance.finished && PuzzleManager.instance.nextLvl != null;
		if (isNext)
			onNext();
		else
			onQuit();
	}
	void onNext () {
		SceneManager.LoadScene(PuzzleManager.instance.nextLvl);
	}
	void onQuit () {
		SceneManager.LoadScene(GameManager.instance.lastLoadedScene);
	}
}
