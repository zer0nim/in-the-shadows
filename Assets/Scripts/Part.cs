using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {
	public Vector3 validRot, validPos;
	public KeyCode verticalKey = KeyCode.LeftControl;
	public KeyCode moveKey = KeyCode.LeftShift;
	public int difficulty = 3;


	private Vector3 screenSpace, offset;
	private Vector2 mousePerc;

	// mousePerc is used to calculate rotation angle
	Vector2 CalcMousePerc () {
		Vector2 newMousePerc = new Vector2();
		newMousePerc.x = Input.mousePosition.x / Screen.width;
		newMousePerc.y = Input.mousePosition.y / Screen.height;
		return (newMousePerc);
	}

	void OnMouseDown () {
		screenSpace = Camera.main.WorldToScreenPoint(transform.position);
		// calculate any difference between the puzzle world position and the mouses Screen position converted to a world point
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenSpace.z));
		// update mousePerc, used later to calculate rotation angle
		mousePerc = CalcMousePerc();
	}

	void OnMouseDrag () {
		Vector2 newMousePerc = CalcMousePerc();

		// move mode
		if (Input.GetKey(moveKey) && difficulty >= 3) {
			// keep track of the mouse position
			var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
			// convert the screen mouse position to world point and adjust with offset
			var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
			transform.position = curPosition;

			// continue updating mousePerc to avoid a jump in the rotation when we will stop holding the moveKey
			mousePerc = newMousePerc;
		}
		// vertical rotation mode
		else if (Input.GetKey(verticalKey) && difficulty >= 2) {
			if (mousePerc.y != newMousePerc.y) {
				float angle = (mousePerc.y - newMousePerc.y) * 360;
				transform.RotateAround(transform.position, Camera.main.transform.right, -angle);
				mousePerc = newMousePerc;
			}
		}
		// horizontal rotation mode
		else {
			if (mousePerc.x != newMousePerc.x) {
				float angle = (mousePerc.x - newMousePerc.x) * 360;
				transform.RotateAround(transform.position, Camera.main.transform.up, angle);
				mousePerc = newMousePerc;
			}
		}
	}
}
