using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class DeleteProgress : HoverCursor {
	public void OnDeleteProgress () {
		GameManager.instance.SetCursor(CursorType.normal);
		GameManager.instance.initProgress();
		GameManager.instance.SaveGame();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
