using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleManager : MonoBehaviour {
	public List<Part> parts;
	public int validRotAngle = 5;

	private List<float> validsParts;

	void Awake () {
		validsParts = Enumerable.Repeat(180f, parts.Count).ToList();
	}

	void Update ()
	{
		// Store angle betwwen rotations quaternion
		for (int i = 0; i < parts.Count; ++i)
			validsParts[i] = Quaternion.Angle(parts[i].gameObject.transform.rotation, Quaternion.Euler(parts[i].validRot));

		// if (validsParts.TrueForAll(x => x < validRotAngle))
		// 	print("You win !");
	}
}
