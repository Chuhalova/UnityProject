using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {


	public static LevelInfo current;

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
		rabbit.transform.position = this.startingPosition;
	}
	public void addCoins(int number)
	{
		Debug.Log ("Coins collected"+number);
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
