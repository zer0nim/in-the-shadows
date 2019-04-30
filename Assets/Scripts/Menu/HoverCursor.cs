using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCursor : MonoBehaviour {
	public void OnHover () {
		GameManager.instance.SetCursor(CursorType.link);
	}

	public void OnHoverAway () {
		GameManager.instance.SetCursor(CursorType.normal);
	}
}
