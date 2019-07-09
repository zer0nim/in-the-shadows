using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCursor : MonoBehaviour {
	[HideInInspector]
	public bool active = true;

	public void OnHover () {
		if (active) {
			GameManager.instance.SetCursor(CursorType.link);
			EventSystem.current.SetSelectedGameObject(null); // clear selection status
		}
	}

	public void OnHoverAway () {
		if (active) {
			GameManager.instance.SetCursor(CursorType.normal);
			EventSystem.current.SetSelectedGameObject(null); // clear selection status
		}
	}
}
