using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogResult {none, ok, cancel};

public class Dialog : HoverCursor {
	public GameObject dialogBg = null;
	[HideInInspector]
	public	DialogResult result = DialogResult.none;

	void Awake () {
		if (dialogBg == null) Debug.LogError("You need to asign \"DialogBg\" to the Dialog script !");
	}

	public void setVisible(bool visible = true) {
		dialogBg.SetActive(visible);
	}

	public void OnOk () {
		result = DialogResult.ok;
		setVisible(false);
	}

	public void OnCancel () {
		result = DialogResult.cancel;
		setVisible(false);
	}
}
