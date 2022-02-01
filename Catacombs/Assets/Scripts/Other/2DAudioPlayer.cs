using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioPlayer2D {

	public static void PlayOneShot(AudioClip audio, float volume, string objectName){
		GameObject audioObject = new GameObject();
		audioObject.name = objectName;
		AudioSource audioSource = audioObject.AddComponent<AudioSource> ();
		audioSource.volume = volume;
		audioSource.PlayOneShot (audio);
		audioObject.AddComponent<AudioPlayer2DObjectController> ();
	}
}

public class AudioPlayer2DObjectController : MonoBehaviour{
	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	void Update(){
		if (!audioSource.isPlaying) {
			Destroy (gameObject);
		}
	}
}
