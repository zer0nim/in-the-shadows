using UnityEditor;
using UnityEngine;

public class Editor : MonoBehaviour {

	[MenuItem("Edit/Reset Playerprefs")]
	public static void DeletePlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}
