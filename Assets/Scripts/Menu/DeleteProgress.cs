using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class DeleteProgress : HoverCursor {
	public Dialog dialog = null;

	void Awake () {
		if (dialog == null) Debug.LogError("You need to asign \"dialog\" to the DeleteProgress script !");
	}

	public void OnDeleteProgress () {
		StopCoroutine("DelProgressCoroutine");
		StartCoroutine("DelProgressCoroutine");
	}

	IEnumerator DelProgressCoroutine () {
		dialog.setVisible();

		while (dialog.result == DialogResult.none)
			yield return null;

		if (dialog.result == DialogResult.ok) {
			GameManager.instance.SetCursor(CursorType.normal);
			GameManager.instance.initProgress();
			GameManager.instance.SaveGame();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		dialog.result = DialogResult.none;
    }
}
