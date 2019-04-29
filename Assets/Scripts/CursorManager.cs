using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType {normal, link};

public class CursorManager : MonoBehaviour {
    [System.Serializable]
	public struct CURSORS {
		public Texture2D normal;
		public Texture2D link;
	}
	public CURSORS Cursors;

    private Vector2 hotSpot = Vector2.zero;


	public void SetCursor(CursorType cursorType) {
		Texture2D newTexture;
		switch (cursorType)
        {
            case CursorType.link:
				newTexture = Cursors.link;
				break;
            case CursorType.normal:
			default:
                newTexture = Cursors.normal;
				break;
        }
		Cursor.SetCursor(newTexture, hotSpot, CursorMode.Auto);
	}
}