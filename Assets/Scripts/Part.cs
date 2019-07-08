using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Part : MonoBehaviour {
	public Vector3 validRot;
	public Transform validPos;
	public KeyCode verticalKey = KeyCode.LeftControl;
	public KeyCode moveKey = KeyCode.LeftShift;
	public int	difficulty = 3;
	public bool	moveHori = false;
	public bool	moveVert = false;
	[Header("v-- left, top, right, bot => in % of windows --v")]
	// windows percentage where we are allowed to move
	public Vector4	moveZone = new Vector4(.2f, .2f, .2f, .2f);
	[HideInInspector]
	public bool	winAnimFinished = false;

	private Vector4 moveZoneCoord;
	private Vector3 screenSpace, offset;
	private Vector2 mousePerc;
	private Vector3 startScreenSpace;

	void Start () {
		moveZoneCoord = new Vector4(
			Screen.width * moveZone.x,
			Screen.height - Screen.height * moveZone.y,
			Screen.width - Screen.width * moveZone.z,
			Screen.height * moveZone.w
		);
	}

	void Update () {
		if (PuzzleManager.instance.finished && !winAnimFinished) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(validRot),
			PuzzleManager.instance.validAnimSpeed * Time.deltaTime);
			if (transform.rotation == Quaternion.Euler(validRot))
				winAnimFinished = true;
		}
	}

	// mousePerc is used to calculate rotation angle
	Vector2 CalcMousePerc () {
		Vector2 newMousePerc = new Vector2();
		newMousePerc.x = Input.mousePosition.x / Screen.width;
		newMousePerc.y = Input.mousePosition.y / Screen.height;
		return (newMousePerc);
	}

	void OnMouseDown () {
		if (!PuzzleManager.instance.finished && !EventSystem.current.IsPointerOverGameObject()) {
			screenSpace = Camera.main.WorldToScreenPoint(transform.position);
			// calculate any difference between the puzzle world position and the mouses Screen position converted to a world point
			offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenSpace.z));
			// update mousePerc, used later to calculate rotation angle
			mousePerc = CalcMousePerc();

			startScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
		}
	}

	void OnMouseDrag () {
		if (!PuzzleManager.instance.finished && !EventSystem.current.IsPointerOverGameObject()) {
			Vector2 newMousePerc = CalcMousePerc();

			// move mode
			if (Input.GetKey(moveKey) && difficulty >= 3) {
				if (moveHori || moveVert) {
					// keep track of the mouse position
					Vector3 curScreenSpace = startScreenSpace;

					if (moveHori && moveVert)
						curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
					else if (moveHori)
						curScreenSpace = new Vector3(Input.mousePosition.x, startScreenSpace.y, screenSpace.z);
					else if (moveVert)
						curScreenSpace = new Vector3(startScreenSpace.x, Input.mousePosition.y, screenSpace.z);

					// limit the moves to moveZone
					curScreenSpace.x = curScreenSpace.x < moveZoneCoord.x ? moveZoneCoord.x : curScreenSpace.x;
					curScreenSpace.x = curScreenSpace.x > moveZoneCoord.z ? moveZoneCoord.z : curScreenSpace.x;
					curScreenSpace.y = curScreenSpace.y > moveZoneCoord.y ? moveZoneCoord.y : curScreenSpace.y;
					curScreenSpace.y = curScreenSpace.y < moveZoneCoord.w ? moveZoneCoord.w : curScreenSpace.y;

					// convert the screen mouse position to world point and adjust with offset
					var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
					transform.position = curPosition;

					// continue updating mousePerc to avoid a jump in the rotation when we will stop holding the moveKey
					mousePerc = newMousePerc;
				}
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
}
