using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType {normal, link};

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	[System.Serializable]
	public struct CURSORS {
		public Texture2D normal;
		public Texture2D link;
	}
	public CURSORS Cursors;

    private Vector2 hotSpot = Vector2.zero;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		// Set this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		GameManager.instance.SetCursor(CursorType.normal);
	}

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
