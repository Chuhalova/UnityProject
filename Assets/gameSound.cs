using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSound : MonoBehaviour {

	public AudioClip audioClip = null;
	AudioSource audioClipSource = null;
	void Start(){
		audioClipSource = gameObject.AddComponent<AudioSource>();
		audioClipSource.clip = audioClip;
		audioClipSource.loop = true;
		audioClipSource.volume = 0.1f;
		audioClipSource.priority = 255;

		if(soundManager.Instance.isVolumeOn()) audioClipSource.Play();
	}

	void FixedUpdate(){
		if (!soundManager.Instance.isVolumeOn()){
			audioClipSource.Stop();
		}
		else if (!audioClipSource.isPlaying) audioClipSource.Play();
	}
}
