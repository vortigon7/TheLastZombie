using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public Sound[] internalSounds;
	public Sound[] externalSounds;

	public static AudioManager instance;

	//public AudioMixer internalMixer;

	/* *
	 * Note: Use Awake instead of the constructor for initialization, 
	 * as the serialized state of the component is undefined at construction time. 
	 * Awake is called once, just like the constructor.
	 * -- Taken from Unity's documentation
	 * */
	void Awake () {
		/* if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject); */

		foreach (Sound s in internalSounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = s.output;
		}
	}

	void Start () {
		Play ("Ambience");
	}

	// Method for playing a sound
	public void Play (string name) {
		Sound s = Array.Find (internalSounds, sound => sound.name == name);
		if (s == null) {
			return;
		} else {
			s.source.Play ();
		}
	}
}
