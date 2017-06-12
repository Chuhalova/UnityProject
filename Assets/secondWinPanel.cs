using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class secondWinPanel : MonoBehaviour {
	public MyButton closeButton;
	public MyButton closeBackground;
	public MyButton menuButton;
	public MyButton repeatButton;

	//for music 
	public AudioClip winAudioClip = null;
	AudioSource winAudioSource = null;

	//for crystals
	public UI2DSprite crystal1;
	public UI2DSprite crystal2;
	public UI2DSprite crystal3;
	public Sprite crystalSpriteNull;
	public Sprite crystalSprite1;
	public Sprite crystalSprite2;
	public Sprite crystalSprite3;

	//for fruits
	public UILabel fruits;

	//for coins
	public UILabel coins;

	/*public MyButton background;
	public MyButton close;
	public MyButton replay;
	public MyButton next;
	public UILabel coins;
	public UILabel fruits;
	public UI2DSprite fun;*/
	// Use this for initialization
	void Start () {
		setCrystal ();
		setFruits (fruits);
		setCoins (coins);
		closeButton.signalOnClick.AddListener (this.openMenu);
		closeBackground.signalOnClick.AddListener (this.openMenu);
		menuButton.signalOnClick.AddListener (this.openNextLevel);
		repeatButton.signalOnClick.AddListener (this.repeat);

		//for music 
		this.winAudioSource = gameObject.AddComponent<AudioSource>();
		this.winAudioSource.clip = winAudioClip;
		if(soundManager.Instance.isSoundOn()) winAudioSource.Play();
	}

	public void setCrystal(){
		if (crystalPanel.crystals.firstWin == true) {
			crystal1.sprite2D = crystalSprite1;
		}
		if (crystalPanel.crystals.secondWin == true) {
			crystal2.sprite2D = crystalSprite2;
		}
		if (crystalPanel.crystals.thirdWin == true) {
			crystal3.sprite2D = crystalSprite3;
		}

	}
	public void setFruits(UILabel fruits){
		this.fruits.text = LevelInfo.current.getFruits().ToString();
	}
	public void setCoins(UILabel coins){
		this.coins.text = LevelInfo.current.getCoins().ToString();
	}
	/*background.signalOnClick.AddListener(this.onClosePlay);
		close.signalOnClick.AddListener(this.onClosePlay);
		replay.signalOnClick.AddListener(this.onReplayPlay);
		next.signalOnClick.AddListener(this.onClosePlay);
		setsFilds();*/

	/*private void setsFilds()
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
	void openMenu(){
		SceneManager.LoadScene ("ChooseLevel");
	}
	void openNextLevel(){ 
		SceneManager.LoadScene ("ChooseLevel");
	}

	void repeat()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
