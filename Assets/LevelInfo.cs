using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
	public static LevelInfo current;
	int coins=0;
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

	public void addAlmaz(int number)
	{
		Debug.Log("Сoins collected" + number);
	}

}
