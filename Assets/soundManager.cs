using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager{
	bool audioPlay = true;
	bool isSound = true;

	public bool isSoundOn(){
		return this.audioPlay;
	}

	public bool isVolumeOn(){
		return this.isSound;
	}
	public void setSoundOn(bool val){
		this.audioPlay = val;
		PlayerPrefs.SetInt("sound", this.audioPlay ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void setVolumeOn(bool val){
		this.isSound = val;
		PlayerPrefs.SetInt("volume", this.isSound ? 1 : 0);
		PlayerPrefs.Save();
	}
	soundManager(){
		audioPlay = PlayerPrefs.GetInt("sound", 1) == 1;
		isSound = PlayerPrefs.GetInt("volume", 1) == 1;
	}
	public static soundManager Instance = new soundManager();
}