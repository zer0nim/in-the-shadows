using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {
	public static AudioManager	instance = null;
	public Sound[] sounds;
	public bool volumeOn = true;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(gameObject);
			return;
		}

		// Set this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	void Start () {
		Play("piano_loop");
	}

	public void Play (string name) {
		Sound s = Array.Find(sounds, sounds => sounds.name == name);
		if (s == null) {
			Debug.LogWarning("Sound \"" + name + "\" was not found !");
			return;
		}
		s.source.Play();
	}

	public void toggleAudio (bool pause) {
		AudioListener.pause = pause;
	}
}
