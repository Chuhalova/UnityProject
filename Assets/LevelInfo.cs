using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelInfo : MonoBehaviour {
	public static LevelInfo current;
	int coins=0;
	int lifesNumber = 3;
	void Awake()
	{
		current = this;
	}

	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos)
	{
		this.startingPosition = pos;
	}
	public void onRabbitDeath(HeroRabbit rabbit)
	{
		decreaseLifeNumber ();
		uiCoinsCounter.coinsCounter.removeCoins();
		rabbit.transform.position = this.startingPosition;
	}
	public void addCoins(int number)
	{
		coins += number;
		uiCoinsCounter.coinsCounter.setCoins(coins);

		//this.coins += number;
		//string coinsNum = coins.ToString();
		//string forPast = "";
		//for (int i = 0; i < 4 - coinsNum.Length; i++) {
		//	forPast += "0";
		//}
		//forPast += coinsNum;
		//coinsLabel.text = forPast;
		//Debug.Log ("Coins collected"+number);
	}
	public void addFruits(int number)
	{
		Debug.Log("Сoins collected" + number);
	}
	void decreaseLifeNumber() {
		if (lifesNumber <= 0) {
			//lifesNumber = 3; //оновлюемо життя
			SceneManager.LoadScene("ChooseLevel");
		} else {
			lifesNumber--;
		}
	}
	public int getLifes() {
		return lifesNumber;
	}
	public void addAlmaz(int number)
	{
		Debug.Log("Сoins collected" + number);
	}

}
