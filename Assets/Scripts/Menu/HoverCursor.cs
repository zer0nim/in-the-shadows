using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCursor : MonoBehaviour {
	public bool active = true;

	public void OnHover () {
		if (active)
			GameManager.instance.SetCursor(CursorType.link);
	}

	public void OnHoverAway () {
		if (active)
			GameManager.instance.SetCursor(CursorType.normal);
	}
}
