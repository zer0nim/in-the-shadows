using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleManager : MonoBehaviour {
	public static PuzzleManager instance = null;
	public List<Part> parts;
	public float minValidPerc = .95f;

	[HideInInspector]
	public float validPerc;

	private List<float> validsParts;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		validsParts = Enumerable.Repeat(180f, parts.Count).ToList();
	}

	void Update ()
	{
		// Store angle betwwen rotations quaternion
		for (int i = 0; i < parts.Count; ++i)
			validsParts[i] = Quaternion.Angle(parts[i].gameObject.transform.rotation, Quaternion.Euler(parts[i].validRot));

		validPerc = 1 - validsParts.Max() / 180;

		if (validPerc > minValidPerc)
			print("Valid !");
	}
}
