using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winPanel : MonoBehaviour {
	public MyButton closeButton;
	public MyButton closeBackground;
	public MyButton menuButton;
	public MyButton repeatButton;

	//for music 
	public AudioClip winAudioClip = null;
	AudioSource winAudioSource = null;

	/*public MyButton background;
	public MyButton close;
	public MyButton replay;
	public MyButton next;
	public UILabel coins;
	public UILabel fruits;
	public UI2DSprite fun;*/
	// Use this for initialization
	void Start () {
		closeButton.signalOnClick.AddListener (this.openMenu);
		closeBackground.signalOnClick.AddListener (this.openMenu);
		menuButton.signalOnClick.AddListener (this.openMenu);
		repeatButton.signalOnClick.AddListener (this.repeat);

		//for music 
		this.winAudioSource = gameObject.AddComponent<AudioSource>();
		this.winAudioSource.clip = winAudioClip;
		if(soundManager.Instance.isSoundOn()) winAudioSource.Play();
	}


		/*background.signalOnClick.AddListener(this.onClosePlay);
		close.signalOnClick.AddListener(this.onClosePlay);
		replay.signalOnClick.AddListener(this.onReplayPlay);
		next.signalOnClick.AddListener(this.onClosePlay);
		setsFilds();*/
	
	/*
	private void setsFilds()
	{
		UI2DSprite[] crystals = fun.transform.GetComponentsInChildren<UI2DSprite>();

		SpriteRenderer[] crystalsFromGame = LevelInfo.current.getCrystals();
		for(int i = 0; i < crystalsFromGame.Length; ++i)
		{
			crystals[i+1].sprite2D = crystalsFromGame[i].sprite;
		}

		coins.text = "+" + LevelInfo.current.getCoinsOnLevel();
		fruits.text = LevelInfo.current.getFruitCount().ToString();
	}

	private void onReplayPlay()
	{
		SceneManager.LoadScene(LevelInfo.current.currentLevelName);
	}

	private void onClosePlay()
	{
		SceneManager.LoadScene("ChooseLevel");
	}
*/
	void openMenu()
	{
		SceneManager.LoadScene("ChooseLevel");
	}

	void repeat()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
